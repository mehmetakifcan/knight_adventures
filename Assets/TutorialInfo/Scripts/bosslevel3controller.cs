using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bosslevel3controller : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            GameController.instance.EnableSpawner();
            
        }
    }
}
