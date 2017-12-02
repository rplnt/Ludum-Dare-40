using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wobble : MonoBehaviour {

    public float verticalWobbleSpeed;
    public Vector3 wobbleLimits;


    private void Update() {
        transform.localPosition = new Vector3(transform.localPosition.x, Mathf.PingPong(Time.time, 0.1f / verticalWobbleSpeed) * verticalWobbleSpeed, transform.localPosition.y);
        transform.localEulerAngles = new Vector3(
            Mathf.PingPong(Time.time, 2.0f * wobbleLimits.x) - wobbleLimits.x,
            Mathf.PingPong(Time.time, 2.0f * wobbleLimits.y) - wobbleLimits.y,
            Mathf.PingPong(Time.time, 2.0f * wobbleLimits.z) - wobbleLimits.z
            );
    }
}
