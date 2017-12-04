using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public Transform path;
    public Transform tentacles;

    List<Order> orders;

    Bar bar;
    List<Transform> nodes;
    List<Transform> targets;
    int targetIndex = 0;

    public float baseMoveSpeed;
    public float rotateSpeed;

    [Header("Dropping")]
    public int nudgeRetries;

    public System.Action<Transform> TargetReached;


    private void Start() {
        Debug.Log("Player init");
        if (path == null) {
            path = GameObject.Find("Level/Path").transform;
        }

        nodes = new List<Transform>();
        targets = new List<Transform>();
        foreach (Transform child in path) {
            nodes.Add(child);
            if (child.CompareTag("Target")) {
                targets.Add(child);
            }
        }
        Debug.Log("Loaded " + nodes.Count + " nodes (" + targets.Count + " targets)");

        // first should be bar
        bar = nodes[0].GetComponent<Bar>();
        if (bar == null) {
            Debug.LogError("Octo couldn't find bar!");
        }

        orders = new List<Order>();
        foreach (Transform tentacle in tentacles) {
            orders.Add(new Order(tentacle, orders.Count));
        }

        // shuffle orders
        //for (int i=0; i < orders.Count; i++) {
        //    int j = Random.Range(i, orders.Count);
        //    Order swp = orders[i];
        //    orders[i] = orders[j];
        //    orders[j] = swp;
        //}

        Debug.Log("Loaded " + orders.Count + " tentacle slots");
    }


    private void Update() {
        if (GameManager.Instance.paused) return;

        Transform target = nodes[targetIndex];

        RidePath(target);

        /* Reached Target */
        if (Vector3.Distance(transform.position, target.position) < 0.125f) {
            /* Load/Unload Platters */
            if (target.CompareTag("Respawn")) {
                if (GameManager.Instance.level < orders.Count) {
                    GameManager.Instance.level++;
                }
                LoadPlatters();
            } else if (target.CompareTag("Target")) {
                GameManager.Instance.ScorePoints(UnloadPlatters(target));
            } else {
                int retries = 0;

                while (retries < nudgeRetries) {
                    retries++;
                    if (orders[Random.Range(0, orders.Count)].Nudge()) break;
                }

            }

            /* Move to the next target */
            targetIndex++;
            targetIndex %= nodes.Count;
            Debug.Log(targetIndex + ": " + nodes[targetIndex].name);
        }
    }


    private void RidePath(Transform target) {

        Vector3 direction = target.position - transform.position;
        Debug.DrawRay(transform.position, direction, Color.yellow);

        /* Rotate */
        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, direction, rotateSpeed * Time.deltaTime, 0.0f));
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotateSpeed * Time.deltaTime);

        /* Move */
        //transform.position = Vector3.MoveTowards(transform.position, target.position, baseMoveSpeed * GameManager.Instance.speed * Time.deltaTime);
        Vector3 moveBy = transform.forward * baseMoveSpeed * GameManager.Instance.speed * Time.deltaTime;
        transform.position += new Vector3(moveBy.x, 0.0f, moveBy.z);
        Debug.DrawRay(transform.position, transform.forward, Color.green);
    }


    private void LoadPlatters() {
        //Debug.Log("Level " + GameManager.Instance.level);

        for (int i = 0; i < GameManager.Instance.level; i++) {
            Debug.Log("Loading tray " + i);
            Order current = orders[i % orders.Count];
            //Order current = orders[Random.Range(0, orders.Count) - 1];
            Debug.Assert(current.PlatterFull == false);

            /* Add platter if we don't have any */
            if (!current.HasPlatter) {
                GameObject platter = bar.GiveOutPlatter(current.Slot);
                current.AddPlatter(platter.transform);
            }

            /* Load platter */
            GameObject stuff = bar.GiveOutStuff(current.GetPlatterSlot());
            current.LoadOrder(targets[Random.Range(0, targets.Count)], stuff.transform);
        }
    }


    private int UnloadPlatters(Transform currentStop) {
        int unloaded = 0;
        foreach (Order order in orders) {
            if (order.Target == currentStop) {
                // Reached target, unload platter
                Debug.Log("Reached target " + currentStop.name + " with " + order.TentacleId);
                GameObject currentOrder = order.UnloadOrder();

                if (currentOrder == null) {
                    continue;
                }
                unloaded++;
                Debug.Log(currentOrder.name);
                Destroy(currentOrder);
            }
        }

        return unloaded;
    }

}
