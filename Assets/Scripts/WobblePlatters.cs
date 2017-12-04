using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WobblePlatters : MonoBehaviour {
    [Header("Basic")]
    public float speed;
    public float baseLimit;

    float baseAngle = 45.0f;
    int direction;

    [Header("Game")]
    Order order;
    public float dropAngle;
    public bool falling;
    public float correctionSpeed;
    public bool corrected;
    public bool dropped;

    public System.Action DroppedPlate;

    private void Start() {
        direction = Random.Range(0.0f, 1.0f) > 0.5f ? 1 : -1;
        falling = false;
        corrected = false;
        dropped = false;
    }

    private void Update() {
        if (GameManager.Instance.paused) return;
        if (dropped) return;

        float rotate = Wobble();
        transform.Rotate(rotate, 0, 0);


        float deviance = Mathf.Abs(baseAngle - transform.localEulerAngles.x);

        if (falling) {
            Debug.DrawRay(transform.position, Vector3.up, Color.red);
        }

        if (corrected && deviance <= baseLimit) {
            falling = false;
        }

        if (deviance > baseLimit * 1.3f) {
            falling = true;
            direction = transform.localEulerAngles.x < (baseAngle - baseLimit) ? -1 : 1;
        }

        if (deviance > dropAngle) {
            Drop();
        }
    }

    float Wobble() {
        if (!falling && transform.localEulerAngles.x < (baseAngle - baseLimit)) {
            direction = 1;
        } else if (!falling && transform.localEulerAngles.x > (baseAngle + baseLimit)) {
            direction = -1;
        }

        return speed * Time.deltaTime * direction;
    }

    public void Nudge() {
        falling = true;
        corrected = false;
    }

    public void Stabilize() {
        falling = false;
        corrected = true;
    }

    public void Drop() {
        dropped = true;

        if (DroppedPlate != null) {
            DroppedPlate();
        } else {
            Debug.LogError("Who's taking care of me?");
        }
    }


    public void Correct(float direction) {
        corrected = true;
        transform.Rotate(direction * correctionSpeed * Time.deltaTime, 0, 0);
    }
}
