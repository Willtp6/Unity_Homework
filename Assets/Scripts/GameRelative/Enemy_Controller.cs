using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Enemy_Controller : MonoBehaviour
{
    float distance = 15f;
    Vector3 offset;
    bool run = true, attack = false;
    private Animator animator;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Viking_Controller.mistake)
            distance = 10f;
        else
            distance = 15f;
        if (Viking_Controller.gaming)
        {
            if (Viking_Controller.isDead && Viking_Controller.killed)
            {
                distance = 5f;
                attack = true;
            }
            offset = player.position - player.forward * distance;
            transform.position += offset - transform.position;
            transform.LookAt(player);
        }
        
        animator.SetBool("Run", run);
        animator.SetBool("Attack", attack);
    }
}
