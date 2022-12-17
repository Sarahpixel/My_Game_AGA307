using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine.InputSystem;
using UnityEngine;
using Cinemachine;


public class ThirdPersonShooterController : MonoBehaviour
{
    //makes the aim virtual camera private but arranged in a public class using [Serializefield] to be asscesed in the inspector
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask(); // type is defined in the inspector using the layer mask 
    [SerializeField] private Transform debugTransform; // this is used to test if the object is actually working and that it follows where the player is aiming at
    // bullet reference
    [SerializeField] private Transform bulletProjectile;
    [SerializeField] private Transform bulletPoint;

    // Script Reference
    private StarterAssets.ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;
    private Animator animator;

    private void Awake()
    {
        //gets the comonent of the starter assets
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        //Get the component of the Third person controller
        thirdPersonController = GetComponent<StarterAssets.ThirdPersonController>();
        //gets the componet of the animator
        animator = GetComponent<Animator>();
       
    }

    private void Update()
    {
        //raycast based on mouse position
        Vector3 mouseWorldPosition = Vector3.zero;
        //makes the screenCentrePoint while grabbing the screenWidth and divide by 2 and the height divide by 2
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
       

        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }
        else
        {
            //when the mouse is out of the range of the layer mask it will still follow the mouse that is set in the centre of the screen
            mouseWorldPosition = ray.GetPoint(10);
            debugTransform.position = ray.GetPoint(10);// temp for us to see the postion of our sphere in the inspector
           

        }
        // if the aim function is true, the aim camera is set active if its not true then deactivate the gameobject which goes back to the default player camera
        if (starterAssetsInputs.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            //sets the Sensitivity to aim mode
            thirdPersonController.SetSensitivity(aimSensitivity);
            thirdPersonController.SetRotateOnMove(false);
            animator.SetLayerWeight(1,Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
            // be able to see the player rotate to face the worldAimTarget based on the time multiply the value it takes to rotate
            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            // sets the Sensitivity to the normal palyer camera
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));

        }
        if (starterAssetsInputs.shoot)
        {
            Vector3 aimDir = (mouseWorldPosition - bulletPoint.position).normalized;
            Instantiate(bulletProjectile, bulletPoint.position, Quaternion.LookRotation(aimDir,Vector3.up));
            starterAssetsInputs.shoot = false;
        }

        if (Pause.GameIsPaused == true)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Pause.GameIsPaused == false) 
            Cursor.lockState = CursorLockMode.Locked;




    }


}
