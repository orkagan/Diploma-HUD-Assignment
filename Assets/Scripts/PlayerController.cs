using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpSpeed = 5f;
    public float gravity = 20f;

    public float dashSpeed = 25f;
    public bool _isDashing;

    Vector3 moveDir;

    public Camera fpsCamera;

    private CharacterController _charCtrl;
    private PlayerHud playerHud; 

    // Start is called before the first frame update
    void Start()
    {
        fpsCamera= Camera.main;
        playerHud = GetComponent<PlayerHud>();
        _charCtrl = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DashMechanic();

        }

    }


    private void DashMechanic()
    {
        if (_charCtrl.isGrounded == false && playerHud.staminaBarFull.fillAmount > 0)
        {
            _isDashing = true;
            StartCoroutine(playerHud.PlayerIconChanger());
            
            fpsCamera.transform.forward = transform.forward * dashSpeed * Time.deltaTime; // Unsure on this as it doesnt work currently.
            //moveDir.z += dashSpeed; (previous currently working dash mechanic, though may have to put a very small wait for seconds to stop spamming.
            Debug.Log("DASH");
        }


    }



}
