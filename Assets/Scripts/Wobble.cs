using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wobble : MonoBehaviour {

    public float verticalWobbleSpeed;
    public float wobbleSpeed;
    public Vector3 wobbleLimits;
    float basePositionY;

    float offset;

    private void Start() {
        basePositionY = transform.localPosition.y;
    }


    private void Update() {
        if (verticalWobbleSpeed > 0.0f) {
            transform.localPosition = new Vector3(transform.localPosition.x, basePositionY + Mathf.PingPong(Time.time, 0.1f / verticalWobbleSpeed) * verticalWobbleSpeed, transform.localPosition.z);
        }

        if (wobbleLimits.magnitude > 0.0f) {
            transform.localEulerAngles = new Vector3(
                Mathf.PingPong(Time.time * wobbleSpeed, 2.0f * wobbleLimits.x) - wobbleLimits.x,
                Mathf.PingPong(Time.time * wobbleSpeed, 2.0f * wobbleLimits.y) - wobbleLimits.y,
                Mathf.PingPong(Time.time * wobbleSpeed, 2.0f * wobbleLimits.z) - wobbleLimits.z
                );
        }
    }
}
