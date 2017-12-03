using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DrawPath : MonoBehaviour {

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        foreach (Transform child in transform) {
            Gizmos.DrawSphere(child.position, 0.25f);
        }
    }
}
