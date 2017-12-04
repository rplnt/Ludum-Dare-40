using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    public bool paused;
    public bool over = false;
    public float speed;

    int score = 0;
    float startTime;

    public int level;

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            Instance = this;
        }

        //DontDestroyOnLoad(this.gameObject);

    }

    private void Start() {
        UIController.Instance.SetScore(score);
        startTime = Time.time;
    }

    public void ScorePoints(int inc) {
        score += inc;
        UIController.Instance.SetScore(score);
    }

    public void StartGame() {
        paused = false;
    }

    public void RestartLevel() {
        SceneManager.LoadScene(0);
    }

    public void GameOver() {
        paused = true;
        over = true;
        UIController.Instance.Invoke("GameOver", 1.5f);
    }

}
