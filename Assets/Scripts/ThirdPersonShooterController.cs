using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using Cinemachine;


public class ThirdPersonShooterController : MonoBehaviour
{
    //makes the aim virtual camera private but arranged in a public class using [Serializefield] to be asscesed in the inspector
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;

    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;

    private void Awake()
    {
        //gets the comonent of the starter assets
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
    }

    private void Update()
    {
        // if the aim function is true, the aim camera is set active if its not true then deactivate the gameobject which goes back to the default player camera
        if (starterAssetsInputs.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            //sets the Sensitivity to aim mode
            thirdPersonController.SetSensitivity(aimSensitivity);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            // sets the Sensitivity to the normal palyer camera
            thirdPersonController.SetSensitivity(normalSensitivity);
        }
    }

}
