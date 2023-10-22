using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchCameraScreen : MonoBehaviour
{
    public GameObject[] camere;
    int posInizio = 0;
    int posFine = 1;

    float lerp = 0, passo = 10;
    bool entrare = true;

    void Update() {
        entrare = true;

        lerp += Time.deltaTime / passo;
        transform.position = Vector3.Lerp(
            camere[posInizio].transform.position,
            camere[posFine].transform.position,
            lerp);
        transform.rotation = Quaternion.Lerp(
            camere[posInizio].transform.rotation,
            camere[posFine].transform.rotation,
            lerp);

        if(entrare && transform.position == camere[posFine].transform.position) {
            posInizio += 2;
            posFine += 2;

            if (posInizio == 8 && posFine == 9) {
                posInizio = 0;
                posFine = 1;
            }

            transform.position = camere[posInizio].transform.position;
            transform.rotation = camere[posInizio].transform.rotation;

            lerp = 0;
            entrare = false;
        }
    }
}
