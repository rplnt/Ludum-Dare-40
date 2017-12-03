using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    public bool paused;
    public float speed;

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            Instance = this;
        }

        DontDestroyOnLoad(this.gameObject);

        paused = true;
    }

    private void Start() {
        paused = false;
    }
}
