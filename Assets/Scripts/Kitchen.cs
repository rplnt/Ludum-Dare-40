using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen : MonoBehaviour {
    public GameObject PlatterPrefab;
    public GameObject[] StuffOnPlatesPrefabs;

    void GiveOutPlatter() {
        GameObject platter = Instantiate(PlatterPrefab);
    }
}
