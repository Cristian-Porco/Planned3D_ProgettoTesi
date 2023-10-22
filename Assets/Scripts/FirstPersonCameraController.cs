using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCameraController : MonoBehaviour
{
    public ChangePositionCameraController controllerCamera;

    public float sensitivitaMouse = 100f;
    public Transform playerBody;
    float rotazioneX = 0f;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        if(!controllerCamera.move && controllerCamera.telecameraNew.name == "CameraVistaPrimaPersona") {
            float mouseX = Input.GetAxis("Mouse X") * sensitivitaMouse * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivitaMouse * Time.deltaTime;

            rotazioneX -= mouseY;
            if (rotazioneX > 53f) rotazioneX = 53f;
            rotazioneX = Mathf.Clamp(rotazioneX, -90f, 90f);

            transform.localRotation = Quaternion.Euler(rotazioneX, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
