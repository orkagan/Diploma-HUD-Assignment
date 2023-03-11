using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHookTEST : MonoBehaviour
{
    public Camera camera;
    public bool grappled;
    public Transform _playerTransform;
    private Vector3 _grappleLocation;
    CharacterController _charC;
    private void Start()
    {
        _charC = GetComponent<CharacterController>();
    }

    //This is jank, but calling in lateupdate meant that the character controller script was not ovveriding the Hookshot function.
    public void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HookShot();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //Dash
        }

    }

    public void HookShot()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            //Character controller was firing first, which was overriding the position / teleport.
            //Transform objectHit = hit.transform;
            _grappleLocation = hit.transform.position;
            transform.position = _grappleLocation;
            Debug.Log($"grapple location: {_grappleLocation}");

            // Do something with the object that was hit by the raycast.

            grappled = true;
            Debug.Log("I hit the target");

            

            


        }
        else
        {
            //Need to refine this.
            grappled= false;
        }
    }



}

