using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpSpeed = 5f;
    public float gravity = 20f;
        
    Vector3 moveDir;
    private CharacterController _charCtrl;
    
    // Start is called before the first frame update
    void Start()
    {
        _charCtrl = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_charCtrl.isGrounded)
        {
            moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            if (Input.GetButtonDown("Jump"))
            {
                moveDir.y += jumpSpeed;
            }
        }
        moveDir.y -= gravity * Time.deltaTime;
        _charCtrl.Move(transform.TransformDirection(moveDir) * speed * Time.deltaTime);
    }

}
