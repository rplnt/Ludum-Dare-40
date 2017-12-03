using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowards : MonoBehaviour {
    GameObject player;
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update() {
        //transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z));

        float angle = Vector3.Angle((player.transform.position - transform.position), -transform.up);
        angle = angle > 180 ? angle - 360 : angle;
        if (Mathf.Abs(angle) > 90.0f) return;

        transform.localEulerAngles = new Vector3(
            0, 0, Mathf.Clamp(angle, -45.0f, 45.0f)
            );
    }
}
