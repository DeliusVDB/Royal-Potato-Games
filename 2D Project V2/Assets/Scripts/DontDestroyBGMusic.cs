using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyBGMusic : MonoBehaviour
{
    private GameObject[] music;

    void Start()
    {
        music = GameObject.FindGameObjectsWithTag("BG Music");
        Destroy(music[1]);
    }

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
