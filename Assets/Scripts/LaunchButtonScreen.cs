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

public class LaunchButtonScreen : MonoBehaviour
{
    public void newPlanned() {
        for (int i = 0; i < 18; i++)
            PlannedManager.getInstance().setValori(i, 0);
        SceneManager.LoadScene("Scenes/Editor");
    }
}
