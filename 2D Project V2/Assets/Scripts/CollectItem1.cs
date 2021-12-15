using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectItem1 : MonoBehaviour
{
    private int collected = 0;

    [SerializeField] private Text collectedText;
    [SerializeField] private AudioSource collectItemEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectable"))
        {
            collectItemEffect.Play();
            Destroy(collision.gameObject);
            collected++;
            collectedText.text = $"Apples Collected: {collected}/21";
        }
    } 
}
