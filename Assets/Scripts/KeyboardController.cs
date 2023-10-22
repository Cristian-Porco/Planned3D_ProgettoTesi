using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    public ChangePositionCameraController cameraController;
    public SaveText pauseController;

    void Update() {
        if (Input.GetKeyDown(KeyCode.V))
            if (cameraController.telecameraNew.name == "CameraVistaPrimaPersona" && pauseController.notPause)
                if (Cursor.lockState != CursorLockMode.Locked) {
                    Cursor.lockState = CursorLockMode.Locked;
                    cameraController.occhiCamera.GetComponent<FirstPersonCameraController>().enabled = true;
                    cameraController.strutturaPrimaPersona.GetComponent<FirstPersonMovementController>().enabled = true;
                } else {
                    Cursor.lockState = CursorLockMode.None;
                    cameraController.occhiCamera.GetComponent<FirstPersonCameraController>().enabled = false;
                    cameraController.strutturaPrimaPersona.GetComponent<FirstPersonMovementController>().enabled = false;
                }
    }
}
