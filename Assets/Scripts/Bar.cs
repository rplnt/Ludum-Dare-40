using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour {
    public GameObject PlatterPrefab;
    public GameObject[] StuffOnPlatesPrefabs;

    private void Start() {
        //PlayerController player = GameObject.FindObjectOfType<PlayerController>();
        //Debug.Log("Registering Bar to " + player.name);
    }


    public GameObject GiveOutStuff(Transform slot) {
        GameObject stuffPrefab = StuffOnPlatesPrefabs[Random.Range(0, StuffOnPlatesPrefabs.Length - 1)];
        GameObject stuff = Instantiate(stuffPrefab, slot);
        stuff.name = "Order [" + stuffPrefab.name + "@" + slot.name + "]";
        stuff.transform.localPosition = Vector3.zero;

        return stuff;
    }


    public GameObject GiveOutPlatter(Transform tentacle) {
        GameObject platter = Instantiate(PlatterPrefab, tentacle);
        platter.name = "Platter (" + tentacle.name + ")";
        platter.transform.localPosition = Vector3.zero;
        platter.transform.localScale = Vector3.one;

        return platter;
    }
}
