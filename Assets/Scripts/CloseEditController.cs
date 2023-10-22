using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseEditController : MonoBehaviour
{
    public GameObject editController;

    public void closeEdit() { editController.SetActive(false); }
}
