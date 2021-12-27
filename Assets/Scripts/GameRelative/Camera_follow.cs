using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_follow : MonoBehaviour
{

    public Transform player;
    public float distance = 15f, heightOffset = 2.5f;
    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        offset = player.position - player.forward * distance;
        offset.y += heightOffset;
        if (Viking_Controller.isDead && Viking_Controller.killed)
            offset += new Vector3(10, 0, 10);
        transform.position += offset - transform.position;
        transform.LookAt(player);
    }
}
