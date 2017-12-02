using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSlots : MonoBehaviour {

    private void Start() {
        foreach(Transform child in this.transform) {
            GameObject bone = GameObject.Find("Bone_" + child.name);
            if (bone == null) {
                Debug.Log("Missing bone");
            }

            child.position = bone.transform.position;
        }
    }
}


