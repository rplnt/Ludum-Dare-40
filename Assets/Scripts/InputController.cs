using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {


    // keep in sync!
    Dictionary<string, int> keys = new Dictionary<string, int>() {
        {"Q", 0}, {"W", 1}, {"E", 2}, {"R", 3},
        {"A", 4}, {"S", 5}, {"D", 6}, {"F", 7},
        };
    public GameObject[] UIObjects;
    public Transform[] SlotObjects;

    int activeIndex = -1;


    private void Start() {
        
    }


    private void Update() {
        if (GameManager.Instance.over) return;

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) {
            GameManager.Instance.paused = !GameManager.Instance.paused;
            UIController.Instance.PauseMenu();
        }

        if (GameManager.Instance.paused) return;

        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Keypad7)) {
            Activate("Q");
        } else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Keypad8)) {
            Activate("W");
        } else if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Keypad9)) {
            Activate("E");
        } else if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Keypad6)) {
            Activate("R");

        } else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Keypad4)) {
            Activate("A");
        } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Keypad1)) {
            Activate("S");
        } else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Keypad2)) {
            Activate("D");
        } else if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Keypad3)) {
            Activate("F");
        }

        if (Input.GetAxis("Horizontal") != 0) {
            MoveTentacle(Input.GetAxis("Horizontal"));
        }
    }


    void Activate(string key) {
        if (!keys.ContainsKey(key)) {
            Debug.LogError("Key not mapped");
            return;
        }

        if (activeIndex >= 0) {
            UIController.Instance.HighLightButton(UIObjects[activeIndex], true);
        }
        activeIndex = keys[key];
        UIController.Instance.HighLightButton(UIObjects[activeIndex]);
    }


    void MoveTentacle(float direction) {
        if (activeIndex < 0) return;

        WobblePlatters wp = SlotObjects[activeIndex].GetComponentInChildren<WobblePlatters>();
        if (wp == null) {
            return;
        }

        wp.Correct(direction);
    }

}
