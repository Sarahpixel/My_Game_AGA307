using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Rendering;

public class LedgeGrabbing : MonoBehaviour
{
    [Header("References")]
    public ThirdPersonController pm;
    public LedgeGrabbing lg;
    public Transform orientation;
    public Transform cam;
    public Rigidbody rb;

    [Header("Ledge Grabbing")]
    public float moveToLedgeSpeed;
    public float maxLedgeGrabDistance;
    //minimim time on ledge and current time on ledge
    public float minTimeOnLedge;
    public float timeOnLedge;
    //bool to check if the player is currently holding on the ledge
    public bool holding;

    [Header("Ledge Detection")]
    public float ledgeDetectionLength;
    public float ledgeSphereCastRadius;
    public LayerMask IsLedge;

    private Transform lastLedge;
    private Transform currentLedge;

    [Header("Ledge Jumping")]
    public KeyCode jumpKey = KeyCode.Space;
    public float ledgejumpForwardForce;
    public float ledgeUpWardForce;

    [Header("Exiting")]
    public bool exitingLedge;
    public float exitingLedgeTime;
    private float exitingLedgeTimer;


    private RaycastHit ledgeHit;

    private void Start()
    {
        lg = GetComponent<LedgeGrabbing>();
    }
    private void Update()
    {
        LedgeDetection();
        StateMachine();
    }

    private void StateMachine()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal"); //gets the horizontal input keys 
        float verticalInput = Input.GetAxisRaw("Vertical"); // gets the vertical input keys 
        bool anyInputKeyPressed = horizontalInput != 0 || verticalInput != 0; //define whenever thre input keys are pressed

        // SubState 1- Holding onto Ledge
        if (holding)
        {
            FreezeRigidBodyOnLedge(); //calls the function if holding onto a ledge

            // counts the time on ledge
            timeOnLedge += Time.deltaTime;

            if (timeOnLedge > maxLedgeGrabDistance && anyInputKeyPressed) ExitLedgeHold();// if the time on the ledge is reached and any input keys are pressed that means the player wants to exit holding onto the ledge

            if (Input.GetKeyDown(jumpKey)) Ledgejump();
        }

        //SubState - Exiting ledge
        else if (exitingLedge)
        {
            if (exitingLedgeTimer > 0) exitingLedgeTimer -= Time.deltaTime; //counts down the exiting ledge timer if it's below zero, turn the exiting ledge to false
            
            else exitingLedge = false;
        }
    }

    private void Ledgejump()
    {
        ExitLedgeHold();

        Invoke(nameof(DelayedJumpForce), 0.05f);
    }

    private void DelayedJumpForce()
    {
        Vector3 forceToAdd = cam.forward * ledgejumpForwardForce + orientation.up * ledgeUpWardForce;
        // reset the rigid body velocity
        rb.velocity = Vector3.zero;
        rb.AddForce(forceToAdd, ForceMode.Impulse);
    }
    //using shpere cast to the ledge detection
    private void LedgeDetection()
    {
        //this starts at the players position while applying the shere cast to whwere the palyer's cam is looking and store information oin the ledgeHit varible passing the length of the sphere cast and to also find which layer mask is ledge
        bool ledgeDetected = Physics.SphereCast(transform.position, ledgeSphereCastRadius, cam.forward, out ledgeHit, ledgeDetectionLength, IsLedge);

        //if no ledge is detected return to player
        if (!ledgeDetected) return; 

        float distanceToLedge = Vector3.Distance(transform.position, ledgeHit.transform.position);

        //this makes sure that it only works whith new ledges
        if (ledgeHit.transform == lastLedge) return;

        //whenever the player enters the ledge hold that isn't over the maxledge Distance  that was definded in the inspector and is currently not holding any ledge  
        if (distanceToLedge < maxLedgeGrabDistance && !holding) EnterLedgeHold();
    }
    private void EnterLedgeHold()
    {
        holding= true;

        pm.unlimited = true;
        pm.restricted= true;

        currentLedge = ledgeHit.transform;
        lastLedge = ledgeHit.transform;

        //deactivate the gravity and the rigid body
        rb.useGravity= false;
        rb.velocity = Vector3.zero;
    }
    private void FreezeRigidBodyOnLedge()
    {
        rb.useGravity = false;
        Vector3 directionToLedge = currentLedge.position - transform.position;
        float distanceToledge = Vector3.Distance(transform.position, currentLedge.position);

        //moves the player towards the ledge
        if(distanceToledge > 1f)
        {
            //only if rigid body is not fast enough
            if(rb.velocity.magnitude < moveToLedgeSpeed)
            rb.AddForce(directionToLedge.normalized * moveToLedgeSpeed * 1000f *Time.deltaTime);

        }
        // Hold onto Ledge
        else
        {
            if (!pm.freeze) pm.freeze = true;
            if (pm.unlimited) pm.unlimited = false; // makes sure the player movement scrpit isn't limiting the speed of the rigid body
        }

        //Exiting if something goes wrong
        if(distanceToledge > maxLedgeGrabDistance) ExitLedgeHold();

    }
    private void ExitLedgeHold()
    {
        exitingLedge = true; // sets the exiting ledge to true and start the timer

        exitingLedgeTimer = exitingLedgeTime; // this function won't do anythin g for now but later will fix things 

        holding = false;

        timeOnLedge = 0f;

        pm.freeze= false;
        pm.restricted = false;

        rb.useGravity = true;

        StopAllCoroutines();
        Invoke(nameof(ResetLastledge), 1f); // cant grab onto the same ledge within the same sec 
    }

    private void ResetLastledge()
    {

        lastLedge= null;
    }
}
