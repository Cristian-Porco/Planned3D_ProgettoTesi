using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeEditorController : MonoBehaviour
{
    public int LunghezzaVettoreUI;
    public GameObject[] UIEditor;

    public GameObject controlliEdit;
    public GameObject controlliView;
    public bool visualizzazione_edit = false;
    public bool visualizzazione_view = false;

    public void enableSectionEditor(GameObject selected) {
        controlliEdit.SetActive(false);
        controlliView.SetActive(false);
        visualizzazione_edit = false;
        visualizzazione_view = false;

        for (int i = 0; i < LunghezzaVettoreUI; i++) {
            if (selected == UIEditor[i])
                UIEditor[i].SetActive(true);
            else
                UIEditor[i].SetActive(false);
        }
    }

    public void show_hide_edit() {
        if(visualizzazione_edit) {
            controlliEdit.SetActive(false);
            visualizzazione_edit = false;
        } else {
            controlliEdit.SetActive(true);
            visualizzazione_edit = true;
        }
    }

    public void show_hide_view() {
        if (visualizzazione_view) {
            controlliView.SetActive(false);
            visualizzazione_view = false;
        } else {
            controlliView.SetActive(true);
            visualizzazione_view = true;
        }
    }
}
