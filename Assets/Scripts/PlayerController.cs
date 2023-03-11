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
    private CharacterController _charCtrl;
    private PlayerHud playerHud; 

    // Start is called before the first frame update
    void Start()
    {
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
            moveDir.z += dashSpeed;
            Debug.Log("DASH");
        }


    }



}
