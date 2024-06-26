using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Material mat;
    private float offset;
    private PlayerController player;


    public float speed;




    private void Awake()
    {
        mat = GetComponent<Renderer>().material;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        offset += Input.GetAxisRaw("Horizontal") * speed;

        if (player.leftClicked)
        {
            offset += -speed;
        }
        if (player.rightClicked)
        {
            offset += speed;

        }
        mat.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        
    }
}
