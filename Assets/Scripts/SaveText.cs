using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SFB;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class SaveText : MonoBehaviour, IPointerDownHandler {
    public GameObject hud_close;
    public GameObject hud_close_error;

    public bool notPause = true;
    public bool chiusura;

    private string _data = "";

    #if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void DownloadFile(string gameObjectName, string methodName, string filename, byte[] byteArray, int byteArraySize);

        public void OnPointerDown(PointerEventData eventData) {
            _data = "";
            _data += "PlannedCheck\n";
            for (int k = 0; k < 18; k++)
                _data += PlannedManager.getInstance().getValori(k).ToString() + "\n";
            var bytes = Encoding.UTF8.GetBytes(_data);
            DownloadFile(gameObject.name, "OnFileDownload", "Planned.txt", bytes, bytes.Length);
            if (chiusura)
                SceneManager.LoadScene("Scenes/Launch");  
        }

        public void OnFileDownload() {
            //output.text = "File Successfully Downloaded";
        }
    #else
        public void OnPointerDown(PointerEventData eventData) { }

        void Start() {
            var button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
        }

        public void OnClick() {
            hud_close.SetActive(false);
            hud_close_error.SetActive(false);
            var path = StandaloneFileBrowser.SaveFilePanel("Salva Planned con nome...", "", "Planned", "txt");
            _data = "";
            _data += "PlannedCheck\n";

            for (int k = 0; k < 18; k++)
                _data += PlannedManager.getInstance().getValori(k).ToString() + "\n";

            if (!string.IsNullOrEmpty(path)) {
                File.WriteAllText(path, _data);
                notPause = true;
                if (chiusura)
                    SceneManager.LoadScene("Scenes/Launch");
            } else {
                hud_close_error.SetActive(true);
                notPause = true;
            }
        }
    #endif


    public void notSave() {
        chiusura = true;
        notPause = false;
        hud_close.SetActive(true);
    }


    public void valueNotSave(int selection) {
        if (selection == 1)
            hud_close.SetActive(false);
        else if (selection == 2)
            SceneManager.LoadScene("Scenes/Launch");
        else if (selection == 3) {
            hud_close_error.SetActive(false);
            notPause = true;
        }
    }
}