using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order {
    public Transform Slot { get; private set; }
    public int TentacleId { get; private set; }

    Transform _target;
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

        _platter = platter;
    }

    public void LoadOrder(Transform target, Transform stuff) {
        Debug.Assert(!PlatterFull);
        Debug.Assert(_target == null);
        _stuff = stuff;
        _target = target;
    }

    public GameObject UnloadOrder() {
        Debug.Assert(PlatterFull);
        GameObject stuff = _stuff.gameObject;


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



}
