using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePositionCameraController : MonoBehaviour
{
    public bool move = false;

    public GameObject visualeIniziale;

    public GameObject occhiCamera;
    public GameObject strutturaPrimaPersona;

    float lerp = 0, passo = 1;
    public GameObject[] viste;

    GameObject telecameraOld;
    public GameObject telecameraNew;

    public ChangeEditorController EditorController;

    public Button[] pulsantiDaFermareSuCambiamento;

    private void Start() {
        telecameraOld = visualeIniziale;
        telecameraNew = visualeIniziale;
    }

    public void Update() {
        if (move) {
            for (int i = 0; i < 3; i++)
                pulsantiDaFermareSuCambiamento[i].interactable = false;

            EditorController.controlliEdit.SetActive(false);
            EditorController.controlliView.SetActive(false);

            EditorController.visualizzazione_edit = false;
            EditorController.visualizzazione_view = false;

            if(telecameraNew.name == "CameraStanza1" || telecameraNew.name == "CameraStanza2" || telecameraNew.name == "CameraStanza3" ||
            telecameraNew.name == "CameraStanza4" || telecameraNew.name == "CameraStanza5" || telecameraNew.name == "CameraStanza6")
                for (int i = 0; i < 3; i++) viste[i].SetActive(false);

            lerp += Time.deltaTime / passo;
            transform.position = Vector3.Lerp(
                telecameraOld.transform.position,
                telecameraNew.transform.position,
                lerp);
            transform.rotation = Quaternion.Lerp(
                telecameraOld.transform.rotation,
                telecameraNew.transform.rotation,
                lerp);

            if (lerp >= 1) {
                move = false;
                lerp = 0;
            }
        } else {
            for (int i = 0; i < 3; i++)
                pulsantiDaFermareSuCambiamento[i].interactable = true;

            if (telecameraNew.name == "CameraVistaPrimaPersona" || telecameraNew.name == "CameraVistaPorte" ||
            telecameraNew.name == "CameraVistaFinestra" || telecameraNew.name == "CameraVistaPortone")
                for (int i = 0; i < 3; i++) viste[i].SetActive(true);
            else
                pulsantiDaFermareSuCambiamento[0].interactable = false;
        }
    }


    public void changePosition(GameObject telecamera)
    {
        if(telecameraNew.name == "CameraVistaPrimaPersona") {
            telecameraNew.transform.position = occhiCamera.transform.position;
            telecameraNew.transform.rotation = occhiCamera.transform.rotation;
        }

        telecameraOld = telecameraNew;
        telecameraNew = telecamera;

        move = true;
    }
}
