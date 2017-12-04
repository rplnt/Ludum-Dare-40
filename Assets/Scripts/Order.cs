using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order {
    public Transform Slot { get; private set; }
    public int TentacleId { get; private set; }

    WobblePlatters wobbler;

    Transform _target = null;
    Transform _platter = null;
    Transform _stuff = null;

    public Transform Target { get { return _target; } }
    public bool HasPlatter { get { return _platter != null; } }
    public bool PlatterFull { get { return _stuff != null; } }

    public Order(Transform slot, int tentacleId) {
        Slot = slot;
        TentacleId = tentacleId;
        _target = null;
    }

    public void AddPlatter(Transform platter) {
        Debug.Assert(!HasPlatter);
        wobbler = platter.gameObject.GetComponent<WobblePlatters>();
        wobbler.DroppedPlate += Dropped;
        _platter = platter;
    }

    public void LoadOrder(Transform target, Transform stuff) {
        Debug.Assert(!PlatterFull);
        Debug.Assert(_target == null);
        _stuff = stuff;
        _target = target;
    }

    public GameObject UnloadOrder() {
        if (!PlatterFull) {
            Debug.Log("Already lost the order?");
            return null;
        }
        GameObject stuff = _stuff.gameObject;

        wobbler.Stabilize();

        _target = null;
        _stuff = null;

        return stuff;
    }

    public Transform GetPlatterSlot() {
        if (!HasPlatter) {
            Debug.LogError("Requesting slot for nonexisting platter");
            return null;
        }

        return _platter.Find("Slot");
    }

    public bool Nudge() {
        if (!HasPlatter) return false;
        if (!PlatterFull) return false;
        if (wobbler.falling) return false;

        Debug.Log("Lost balance!");
        wobbler.Nudge();
        return true;
    }


    private void Dropped() {
        if (PlatterFull) {
            Rigidbody rb = _stuff.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            _stuff.SetParent(null);
            _stuff = null;

            // GAME OVER
            GameManager.Instance.GameOver();
        }

        if (HasPlatter) {
            Rigidbody rb = _platter.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            _platter.SetParent(null);
            _platter = null;


            GameManager.Instance.ScorePoints(-1);

        } else {
            Debug.Log("No platter to drop?");
        }

        _target = null;

        wobbler.DroppedPlate -= Dropped;
        wobbler.enabled = false;
        wobbler = null;
    }

}
