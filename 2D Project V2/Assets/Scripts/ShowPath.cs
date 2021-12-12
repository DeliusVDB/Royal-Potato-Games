using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ShowPath : MonoBehaviour
{
    [SerializeField] private TilemapRenderer tileMapRender;
    [SerializeField] private TilemapCollider2D tileMapCollide;

    private int collected;

    // Update is called once per frame
    void Update()
    {
        if (collected >= 18)
        {
            tileMapRender.enabled = true;
            tileMapCollide.enabled = true;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectable"))
        {
            collected++;
        }
    }


}
