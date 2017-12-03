using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WobblePlatters : MonoBehaviour {
    public float speed;
    public float limit;

    public float targetX, targetZ;
    public float resetX, resetZ;

    private void Update() {
        // TODO

        //if (Mathf.DeltaAngle(transform.localEulerAngles.x, targetX) < 0.1) {
        //    targetX = Random.Range(-limit, limit);
        //    resetX = 0.0f;
        //} else {
        //    resetX += Time.deltaTime;
        //}

        //if (Mathf.DeltaAngle(transform.localEulerAngles.z, targetZ) < 0.1) {
        //    targetZ = Random.Range(-limit, limit);
        //    resetZ = 0.0f;
        //} else {
        //    resetZ += Time.deltaTime;
        //}

        //transform.localEulerAngles = new Vector3(
        //    Mathf.LerpAngle(transform.localRotation.x, targetX, (1.0f / speed) * (resetX / 1.0f)),
        //    0.0f,
        //    Mathf.LerpAngle(transform.localRotation.z, targetZ, (1.0f / speed) * (resetZ / 1.0f))
        //);
    }

}
