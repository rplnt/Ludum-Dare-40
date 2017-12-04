using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public static UIController Instance { get; private set; }

    public Transform canvas;
    public Color defaultColor;
    public Color hlColor;

    [Header("UI Objects")]
    public Text score;
    public GameObject intro;
    public GameObject over;
    public Text overscore;
    public GameObject pause;

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
        foreach(RectTransform go in canvas) {
            Image image = go.GetComponent<Image>();
            image.color = defaultColor;
        }
    }
    

    public void HighLightButton(GameObject go, bool remove=false) {
        Image image = go.GetComponent<Image>();
        image.color = remove? defaultColor : hlColor;
    }

    public void SetScore(int value) {
        score.text = value.ToString();
    }

    public void HideStartMenu() {
        intro.SetActive(false);
    }

    public void GameOver() {
        overscore.text = score.text;
        over.SetActive(true);
    }

    public void PauseMenu() {
        pause.SetActive(!pause.activeSelf);
    }


}
