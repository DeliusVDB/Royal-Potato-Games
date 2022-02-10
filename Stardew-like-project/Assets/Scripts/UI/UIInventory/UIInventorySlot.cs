using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class UIInventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Camera mainCamera;
    private Canvas parentCanvas;
    private Transform parentItem;
    private GameObject draggedItem;

    public Image inventorySlotHighlight; 
    public Image inventorySlotImage;
    public TextMeshProUGUI textMeshProUGUI;

    [SerializeField] private UIInventoryBar inventoryBar = null;
    [SerializeField] private GameObject inventoryTextBoxPrefab = null;
    [HideInInspector] public bool isSelected = false;
    [HideInInspector] public ItemDetails itemDetails;
    [SerializeField] private GameObject itemPrefab = null;
    [HideInInspector] public int itemQuantity;
    [SerializeField] private int slotNumber = 0;

    private void Awake()
    {
        parentCanvas = GetComponentInParent<Canvas>();
    }

    private void OnDisable()
    {
        EventHandler.AfterSceneLoadEvent -= SceneLoaded;
    }

    private void OnEnable()
    {
        EventHandler.AfterSceneLoadEvent += SceneLoaded;
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void SetSelectedItem()
    {
        inventoryBar.ClearHighlightOnInventorySlots();

        isSelected = true;

        inventoryBar.SetHighlightedInventorySlots();

        InventoryManager.Instance.SetSelectedInventoryItem(InventoryLocation.player, itemDetails.itemCode);

        if (itemDetails.canBeCarried == true)
        {
            // Show player carrying item
            Player.Instance.ShowCarriedItem(itemDetails.itemCode);
        }
        else // show player carrying nothing
        {
            Player.Instance.ClearCarriedItem();
        }
    }

    private void ClearSelectedItem()
    {
        // clear currently highlighted items
        inventoryBar.ClearHighlightOnInventorySlots();

        isSelected = false;

        // set no item selected in inventory
        InventoryManager.Instance.ClearSelectedInventoryItem(InventoryLocation.player);

        // Clear player carrying item
        Player.Instance.ClearCarriedItem();
    }

    private void DropSelectedItemAtMousePosition()
    {
        if (itemDetails != null && isSelected)
        {
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -mainCamera.transform.position.z));

            //Create item from prefab at mouse position
            GameObject itemGameObject = Instantiate(itemPrefab, worldPosition, Quaternion.identity, parentItem);
            Item item = itemGameObject.GetComponent<Item>();
            item.ItemCode = itemDetails.itemCode;

            // Remove item from players inventory
            InventoryManager.Instance.RemoveItem(InventoryLocation.player, item.ItemCode);

            if (InventoryManager.Instance.FindItemInInventory(InventoryLocation.player, item.ItemCode) == -1)
            {
                ClearSelectedItem();
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (itemDetails != null)
        {
            // Disable keyword input 
            Player.Instance.DisablePlayerInputAndResetMovement();

            // Instantiate gameObject as dragged item
            draggedItem = Instantiate(inventoryBar.inventoryBarDraggedItem, inventoryBar.transform);

            // Get image for dragged item
            Image draggedItemImage = draggedItem.GetComponentInChildren<Image>();
            draggedItemImage.sprite = inventorySlotImage.sprite;

            SetSelectedItem();

        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // move gameobject as dragged item
        if (draggedItem != null)
        {
            draggedItem.transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Destroy gameobject as dragged item
        if (draggedItem != null)
        {
            Destroy(draggedItem);

            //If drag end over inventory bar, get item drag is over and swap them
            if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.GetComponent<UIInventorySlot>() != null)
            {
                // get the slot number where the drag ended
                int toSlotNumber = eventData.pointerCurrentRaycast.gameObject.GetComponent<UIInventorySlot>().slotNumber;

                // swap inventory items in inventory list
                InventoryManager.Instance.SwapInventoryItems(InventoryLocation.player, slotNumber, toSlotNumber);

                // Destroy inventory Text Box
                DestroyInventoryTextBox();  

                ClearSelectedItem(); 
            }
            // else attempt to drop the item if it can be dropped
            else
            {
                if (itemDetails.canBeDropped)
                {
                    DropSelectedItemAtMousePosition();
                }
            }

            // enable player input
            Player.Instance.EnablePlayerInput();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // if left click
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // If Inventory slot is currently selected then deselect
            if (isSelected == true)
            {
                ClearSelectedItem();
            }
            else
            {
                if (itemQuantity > 0)
                {
                    SetSelectedItem();
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Populate textbox with item details
        if (itemQuantity != 0)
        {
            // Instantiate inventory text box
            inventoryBar.inventoryTextBoxGameObject = Instantiate(inventoryTextBoxPrefab, transform.position, Quaternion.identity);
            inventoryBar.inventoryTextBoxGameObject.transform.SetParent(parentCanvas.transform, false);

            UIInventoryTextBox inventoryTextBox = inventoryBar.inventoryTextBoxGameObject.GetComponent<UIInventoryTextBox>();

            // Set item type description
            string itemTypeDescription = InventoryManager.Instance.GetItemTypeDescription(itemDetails.itemType);

            // Populate textbox
            inventoryTextBox.SetTextboxText(itemDetails.itemDescription, itemTypeDescription, "", itemDetails.itemLongDescription, "", "");

            // Set textbox position according to inventory bar position
            if (inventoryBar.IsInventoryBarPositionBottom)
            {
                inventoryBar.inventoryTextBoxGameObject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0f);
                inventoryBar.inventoryTextBoxGameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 50f, transform.position.z);
            }
            else
            {
                inventoryBar.inventoryTextBoxGameObject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1f);
                inventoryBar.inventoryTextBoxGameObject.transform.position = new Vector3(transform.position.x, transform.position.y - 50f, transform.position.z);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DestroyInventoryTextBox();
    }

    public void DestroyInventoryTextBox()
    {
        if (inventoryBar.inventoryTextBoxGameObject != null)
        {
            Destroy(inventoryBar.inventoryTextBoxGameObject);
        }
    }

    public void SceneLoaded()
    {
        parentItem = GameObject.FindGameObjectWithTag(Tags.ItemsParentTransform).transform;
    }
}
