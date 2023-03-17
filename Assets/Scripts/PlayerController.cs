using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float jumpSpeed = 2f;
    public float gravity = 10f;

    /*public float dashSpeed = 25f;
    public bool _isDashing;*/

    public bool grappling;
    public float grappleRange = 50f;
    public float grappleSpeed = 50f;
    public float staminaRegenRate = 60f;
    public float staminaUsageRate = 40f;
    Vector3 _grapplePoint;

    Vector3 moveDir;

    public Camera fpsCamera;

    public PlayerStats playerStats; //scriptable object
    private CharacterController _charCtrl;
    private PlayerHud playerHud;
    public LineRenderer lr;

    // Start is called before the first frame update
    void Start()
    {
        fpsCamera= Camera.main;
        playerHud = GetComponent<PlayerHud>();
        _charCtrl = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerStats.health = playerStats.maxHealth;
        playerStats.stamina = playerStats.maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        //normal movement stuff
        moveDir = new Vector3(Input.GetAxis("Horizontal"), moveDir.y, Input.GetAxis("Vertical"));
        if (_charCtrl.isGrounded)
        {
            moveDir.y = 0;
            if (Input.GetButtonDown("Jump")) // Cant seem to jump when not moving immediately after a grapple.
            {
                moveDir.y += jumpSpeed;
            }

            //regen stamina
            if (playerStats.stamina < playerStats.maxStamina & !grappling)
            {
                playerStats.stamina += staminaRegenRate * Time.deltaTime; //this is inconsistent idk why (BUG REPORT) - It seems if you're standing still its slow regen, if you're moving it regens quickly because
                //The movement is in update, and this then procs this line of code multiple times which increases its regen?
            }
        }

        //placeholder dash mechanic
        /*if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DashMechanic();

        }*/

        //grappling (kinda jank using Unity's character controller)
        //left click
        if (Input.GetMouseButton(0) & playerStats.stamina>0)
        {
            //not grappling yet but starting grapple
            if (!grappling)
            {
                //if grapple target is found, GrappleHook() returns true, making grappling true
                grappling = GrappleHook();
            }
            //stuff that happens when grappling
            else
            {
                moveDir = Vector3.zero; //just resets moveDir so when you stop grappling momentem isn't weird (gravity in this script is strange)
                playerStats.stamina -= staminaUsageRate * Time.deltaTime;

                Vector3 offset;
                //figuring out the vector from player to target in world space
                offset = _grapplePoint - transform.position;
                //if further than small distance
                if (offset.magnitude > 0.1f)
                {
                    //TODO draw grapple line
                    lr.gameObject.SetActive(true);
                    lr.SetPosition(0, transform.position);
                    lr.SetPosition(1, _grapplePoint);
                    //move towards target
                    _charCtrl.Move(offset.normalized * grappleSpeed * Time.deltaTime);
                }
            }
        }
        else
        {
            lr.gameObject.SetActive(false);
            grappling = false;
        }

        //applying normal movement if not grappling
        if (!grappling)
        {
            moveDir.y -= gravity * Time.deltaTime;
            _charCtrl.Move(transform.TransformDirection(moveDir) * speed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Performs a raycast to set the grapple target to point of raycast hit
    /// </summary>
    /// <returns>Boolean based on whether or not the raycast hits anything</returns>
    private bool GrappleHook()
    {
        RaycastHit hit;
        Ray ray = fpsCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, grappleRange))
        {
            _grapplePoint = hit.point;
            Debug.Log($"grapple point: {_grapplePoint}");
            return true;
        }
        return false;
    }

    /*private void DashMechanic()
    {
        if (_charCtrl.isGrounded == false && playerHud.staminaBarFull.fillAmount > 0)
        {
            _isDashing = true;
            StartCoroutine(playerHud.PlayerIconChanger());
            
            fpsCamera.transform.forward = transform.forward * dashSpeed * Time.deltaTime; // Unsure on this as it doesnt work currently.
            //moveDir.z += dashSpeed; (previous currently working dash mechanic, though may have to put a very small wait for seconds to stop spamming.
            Debug.Log("DASH");
        }
    }*/
}
