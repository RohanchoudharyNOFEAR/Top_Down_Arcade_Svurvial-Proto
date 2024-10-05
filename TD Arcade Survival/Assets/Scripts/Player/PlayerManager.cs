using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Acts As Player Service Locator / Interface
public class PlayerManager : MonoBehaviour
{
    public Animator animator;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

   
}
