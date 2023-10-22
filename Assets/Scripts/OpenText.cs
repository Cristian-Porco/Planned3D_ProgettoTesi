using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SFB;
using System;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class OpenText : MonoBehaviour, IPointerDownHandler {
    public GameObject hud_open_error;

    #if UNITY_WEBGL && !UNITY_EDITOR
            [DllImport("__Internal")]
            private static extern void UploadFile(string gameObjectName, string methodName, string filter, bool multiple);

            public void OnPointerDown(PointerEventData eventData) {
                UploadFile(gameObject.name, "OnFileUpload", ".txt", false);
            }

            public void OnFileUpload(string url) {
                StartCoroutine(OutputRoutine(url));
            }
    #else
        public void OnPointerDown(PointerEventData eventData) { }

            void Start() {
                var button = GetComponent<Button>();
                button.onClick.AddListener(OnClick);
            }

            private void OnClick() {
                var paths = StandaloneFileBrowser.OpenFilePanel("Carica Planned...", "", "txt", false);
                if (paths.Length > 0) {
                    StartCoroutine(OutputRoutine(new System.Uri(paths[0]).AbsoluteUri));
                }
            }
    #endif

    private IEnumerator OutputRoutine(string url) {
        var loader = new WWW(url);
        yield return loader;
        string[] test = loader.text.Split('\n');
        if (test[0].Contains("PlannedCheck")) {
            for(int i = 0; i < 18; i++)
                PlannedManager.getInstance().setValori(i, Convert.ToInt32(test[i+1]));
            SceneManager.LoadScene("Scenes/Editor");
        } else {
            hud_open_error.SetActive(true);
        }
    }

    public void closeOpenError() {
        hud_open_error.SetActive(false);
    }
}