using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class keyController : MonoBehaviour
{
    public int keynumber;
    public GameObject keyUI;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameController.instance.KeyCount(keynumber);
            keyUI.SetActive(true);
            Destroy(gameObject);
        }
        if (gameObject.CompareTag("BossKey") && collision.gameObject.CompareTag("Player"))
        {
            GameController.instance.DisableWall();
            GameController.instance.LevelComplete();
        }
    }
}
