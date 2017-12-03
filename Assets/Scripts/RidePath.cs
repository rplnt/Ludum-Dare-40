using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidePath : MonoBehaviour {
    public Transform path;

    List<Transform> nodes;
    int targetIndex = 0;

    public float baseMoveSpeed;
    public float rotateSpeed;

    private void Start() {
        if (path == null) {
            path = GameObject.Find("Level/Path").transform;
        }

        nodes = new List<Transform>();
        foreach (Transform child in path) {
            nodes.Add(child);
        }
    }


    private void Update() {
        if (GameManager.Instance.paused) return;

        Transform target = nodes[targetIndex];

        if (transform.position == target.position) {
            targetIndex = (targetIndex + 1) % nodes.Count;
        }

        
        Vector3 direction = target.position - transform.position;
        Debug.DrawRay(transform.position, direction, Color.red);

        /* Rotate */
        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, direction, rotateSpeed * Time.deltaTime, 0.0f));

        /* Move */
        transform.position = Vector3.MoveTowards(transform.position, target.position, baseMoveSpeed * GameManager.Instance.speed * Time.deltaTime);


    }


}
