using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeParetiPavimentiController : MonoBehaviour
{
    [SerializeField] public GameObject[] targets;
    [SerializeField] private GameObject[] pareti;
    [SerializeField] private GameObject[] pavimenti;

    public int posStanzaTrigger = 0;

    public GameObject actualPareti;
    public Transform containerPareti;

    public GameObject actualPavimenti;
    public Transform containerPavimenti;

    public GameObject itemPrefab;

    Material[] materialiPareti;
    Material[] materialiPavimenti;

    int[] sceltaMaterialePareti = new int[6];
    int[] sceltaMaterialePavimenti = new int[6];

    public Text labelIndicazioneStanza1;

    public GameObject editPareti;
    public GameObject editPavimenti;

    private void Start() {
        for(int i = 0; i < 6; i++)
        {
            sceltaMaterialePareti[i] = 0;
            sceltaMaterialePavimenti[i] = 0;
        }

        int passi = 0;
        while(passi < 2) {
            int dimensione = 0;
            string percorsoResources = "";

            if(passi == 0) {
                dimensione = 185;
                percorsoResources = "Materials/Pareti/wall";
            } else if(passi == 1) {
                dimensione = 43;
                percorsoResources = "Materials/Pavimento/floor";
            }

            if(passi == 0)
                materialiPareti = new Material[dimensione];
            else if (passi == 1)
                materialiPavimenti = new Material[dimensione];

            for (int i = 0; i <= dimensione - 1; i++) {
                var item_go = Instantiate(itemPrefab);
                if (i == 0)
                    item_go.GetComponentInChildren<Text>().text = "NESSUNA SEL.";
                else
                    item_go.GetComponentInChildren<Text>().text = "Tipologia " + i;
                GameObject material = item_go.transform.GetChild(0).gameObject;
                Material materialiFile = Resources.Load(percorsoResources + i) as Material;
                item_go.transform.GetChild(2).gameObject.GetComponent<Text>().text = i.ToString();
                if (passi == 0) {
                    material.GetComponent<Image>().color = materialiFile.color;
                    item_go.GetComponent<Button>().onClick.AddListener(delegate { OnChangeValuePareti(item_go); });
                    item_go.transform.SetParent(containerPareti);
                    materialiPareti[i] = materialiFile;
                } else if (passi == 1) {
                    Material test = new Material(materialiFile);
                    material.GetComponent<Image>().material = test;
                    material.GetComponent<Image>().material.shader = Shader.Find("UI/Default");
                    item_go.GetComponent<Button>().onClick.AddListener(delegate { OnChangeValuePavimenti(item_go); });
                    item_go.transform.SetParent(containerPavimenti);
                    materialiPavimenti[i] = materialiFile;
                }
                item_go.transform.localScale = Vector2.one;                    
            }

            passi++;
        }
        
        int j = 0;
        for(int i = 0; i < 6; i++)
        {
            sceltaMaterialePareti[i] = PlannedManager.getInstance().getValori(j++);
            sceltaMaterialePavimenti[i] = PlannedManager.getInstance().getValori(j++);
            renderParetiChild();
            renderPavimentiChild();
            posStanzaTrigger++;
        }
        posStanzaTrigger = 0;
    }

    private void Update() {
        renderParetiChild();
        renderPavimentiChild();

        labelIndicazioneStanza1.text = "Stanza " + (posStanzaTrigger + 1);
    }


    private void renderParetiChild() {
        for (int i = 0; i < pareti[posStanzaTrigger].transform.childCount; i++) {
            GameObject child = pareti[posStanzaTrigger].transform.GetChild(i).gameObject;
            Renderer rends = child.GetComponent<Renderer>();
            rends.enabled = true;
            rends.sharedMaterial = materialiPareti[sceltaMaterialePareti[posStanzaTrigger]];
        }
    }

    private void renderPavimentiChild() {
        Renderer rends = pavimenti[posStanzaTrigger].GetComponent<Renderer>();
        rends.enabled = true;
        rends.sharedMaterial = materialiPavimenti[sceltaMaterialePavimenti[posStanzaTrigger]];
    }


    void checkTrigger(Collider other) {
        for (int i = 0; i < 6; i++)
            if (other.name == targets[i].name) {
                posStanzaTrigger = i;
                break;
            }
    }

    void OnTriggerEnter(Collider other) { checkTrigger(other); }

    private void OnTriggerStay(Collider other) { checkTrigger(other); }

    void OnTriggerExit(Collider other) {
        editPareti.SetActive(false);
        editPavimenti.SetActive(false);

        changeActualPareti();
        changeActualPavimenti();
    }


    public void changeActualPareti() {
        GameObject material = actualPareti.transform.GetChild(0).gameObject;

        Material materialiFile = Resources.Load("Materials/Pareti/wall" + sceltaMaterialePareti[posStanzaTrigger]) as Material;
        material.GetComponent<Image>().color = materialiFile.color;
        
        if(sceltaMaterialePareti[posStanzaTrigger] != 0)
            actualPareti.GetComponentInChildren<Text>().text = "Tipologia " + sceltaMaterialePareti[posStanzaTrigger];
        else
            actualPareti.GetComponentInChildren<Text>().text = "NESSUNA SEL.";
    }

    public void OnChangeValuePareti(GameObject src) {
        sceltaMaterialePareti[posStanzaTrigger] = Convert.ToInt32(src.transform.GetChild(2).gameObject.GetComponent<Text>().text);
        changeActualPareti();
        PlannedManager.getInstance().setValori(posStanzaTrigger * 2, sceltaMaterialePareti[posStanzaTrigger]);
    }


    public void changeActualPavimenti() {
        GameObject material = actualPavimenti.transform.GetChild(0).gameObject;
        Material materialiFile = Resources.Load("Materials/Pavimento/floor" + sceltaMaterialePavimenti[posStanzaTrigger]) as Material;
        
        Material test = new Material(materialiFile);
        material.GetComponent<Image>().material = test;
        material.GetComponent<Image>().material.shader = Shader.Find("UI/Default");
        material.GetComponent<Image>().color = materialiFile.color;

        if (sceltaMaterialePavimenti[posStanzaTrigger] != 0)
            actualPavimenti.GetComponentInChildren<Text>().text = "Tipologia " + sceltaMaterialePavimenti[posStanzaTrigger];
        else
            actualPavimenti.GetComponentInChildren<Text>().text = "NESSUNA SEL.";
    }

    public void OnChangeValuePavimenti(GameObject src) {
        sceltaMaterialePavimenti[posStanzaTrigger] = Convert.ToInt32(src.transform.GetChild(2).gameObject.GetComponent<Text>().text);
        changeActualPavimenti();
        PlannedManager.getInstance().setValori(1 + posStanzaTrigger * 2, sceltaMaterialePavimenti[posStanzaTrigger]);
    }
}
