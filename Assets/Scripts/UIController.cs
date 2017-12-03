using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public static UIController Instance { get; private set; }

    public Transform canvas;
    public Color defaultColor;
    public Color hlColor;

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            Instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
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
}
