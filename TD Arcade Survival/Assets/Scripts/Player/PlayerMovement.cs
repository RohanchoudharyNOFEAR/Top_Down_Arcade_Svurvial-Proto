using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 5f;
    public float RotationSpeed = 6;
    private PlayerManager playerManager;
    private Vector3 moveInput;

    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        playerManager.rb.velocity = moveInput * Speed;
        if (playerManager.rb.velocity != Vector3.zero)
        {
            Rotate(moveInput);
            playerManager.animator.SetBool("run", true);
        }
        else
        {
            playerManager.animator.SetBool("run", false);
        }
    }

    void Rotate(Vector3 dir)
    {
        float singleStep = RotationSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, dir, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

}
