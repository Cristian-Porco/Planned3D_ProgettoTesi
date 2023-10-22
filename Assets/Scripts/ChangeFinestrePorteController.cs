using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeFinestrePorteController : MonoBehaviour
{
    public bool porta;
    public bool frame;
    public bool davanzale;
    public bool cornice;
    public bool portaPortone;
    public bool framePortone;

    public GameObject actual;
    public Transform dropdown;
    public GameObject itemPrefab;

    Material[] materiali;
    int pos = 0;

    void Start() {
        int dimensione = 0;
        string percorsoResources = "";

        if (frame || framePortone) {
            dimensione = 3;
            percorsoResources = "Materials/Frame/frame";
        } else if (porta || portaPortone) {
            dimensione = 12;
            percorsoResources = "Materials/Door/door";
        } else if (cornice) {
            dimensione = 13;
            percorsoResources = "Materials/Cornice/cornice";
        } else if (davanzale) {
            dimensione = 9;
            percorsoResources = "Materials/Davanzale/davanzale";
        }

        materiali = new Material[dimensione];
        for (int i = 0; i <= dimensione - 1; i++) {
            var item_go = Instantiate(itemPrefab);
            if (i == 0)
                item_go.GetComponentInChildren<Text>().text = "NESSUNA SEL.";
            else
                item_go.GetComponentInChildren<Text>().text = "Tipologia " + i;
            GameObject material = item_go.transform.GetChild(0).gameObject;
            Material materialiFile = Resources.Load(percorsoResources + i) as Material;
            Material test = new Material(materialiFile);
            material.GetComponent<Image>().material = test;
            material.GetComponent<Image>().material.shader = Shader.Find("UI/Default");
            item_go.transform.GetChild(2).gameObject.GetComponent<Text>().text = i.ToString();
            item_go.GetComponent<Button>().onClick.AddListener(delegate { OnChangeValue(item_go); });
            item_go.transform.SetParent(dropdown);
            item_go.transform.localScale = Vector2.one;
            materiali[i] = materialiFile;
        }
    }

    void Update() {
        renderGetAll();
    }


    private void renderColorChild() {
        for (int i = 0; i < transform.childCount; i++) {
            GameObject child = transform.GetChild(i).gameObject;
            Renderer rends = child.GetComponent<Renderer>();
            rends.enabled = true;
            rends.sharedMaterial = materiali[pos];
        }
    }

    private void renderColorUni() {
        Renderer rends = GetComponent<Renderer>();
        rends.enabled = true;
        rends.sharedMaterial = materiali[pos];
    }

    public void renderGetAll() { 
        if (frame) {
            renderColorChild();
            pos = PlannedManager.getInstance().getValori(12);
        } else if (porta) {
            renderColorChild();
            pos = PlannedManager.getInstance().getValori(13);
        } else if (framePortone) {
            renderColorUni();
            pos = PlannedManager.getInstance().getValori(16);
        } else if (portaPortone) {
            renderColorUni();
            pos = PlannedManager.getInstance().getValori(17);
        } else if (davanzale) {
            renderColorChild();
            pos = PlannedManager.getInstance().getValori(14);
        } else if (cornice) {
            renderColorChild();
            pos = PlannedManager.getInstance().getValori(15);
        }  
    }


    public void changeActual() {
        GameObject material = actual.transform.GetChild(0).gameObject;

        Material materialiFile = Resources.Load("Materials/Frame/frame" + pos) as Material;
        if (porta)
            materialiFile = Resources.Load("Materials/Door/door" + pos) as Material;
        else if (framePortone)
            materialiFile = Resources.Load("Materials/Frame/frame" + pos) as Material;
        else if (portaPortone)
            materialiFile = Resources.Load("Materials/Door/door" + pos) as Material;
        else if (cornice)
            materialiFile = Resources.Load("Materials/Cornice/cornice" + pos) as Material;
        else if (davanzale)
            materialiFile = Resources.Load("Materials/Davanzale/davanzale" + pos) as Material;

        Material test = new Material(materialiFile);
        material.GetComponent<Image>().material = test;
        material.GetComponent<Image>().material.shader = Shader.Find("UI/Default");
        material.GetComponent<Image>().color = materialiFile.color;

        if (pos != 0)
            actual.GetComponentInChildren<Text>().text = "Tipologia " + pos;
        else
            actual.GetComponentInChildren<Text>().text = "NESSUNA SEL.";
    }

    public void OnChangeValue(GameObject src) {
        pos = Convert.ToInt32(src.transform.GetChild(2).gameObject.GetComponent<Text>().text);

        if (frame)
            PlannedManager.getInstance().setValori(12, pos);
        else if (porta)
            PlannedManager.getInstance().setValori(13, pos);
        else if (framePortone)
            PlannedManager.getInstance().setValori(16, pos);
        else if (portaPortone)
            PlannedManager.getInstance().setValori(17, pos);
        else if (davanzale)
            PlannedManager.getInstance().setValori(14, pos);
        else if (cornice)
            PlannedManager.getInstance().setValori(15, pos);

        changeActual();
    }
}
