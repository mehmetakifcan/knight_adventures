using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player;
    public float minX, maxX,miny,maxy;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(player.position.x,minX,maxX), Mathf.Clamp(player.position.y,miny,maxy), transform.position.z);

    }
}
