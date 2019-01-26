using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RGBChanger : MonoBehaviour {

    [SerializeField]
    Image[] HUDImages;
    [SerializeField]
    GameObject MenuPanel;

    void Start() {
        MenuPanel.SetActive(true);
        GameObject[] tempObjects = GameObject.FindGameObjectsWithTag("RGBable");
        HUDImages = new Image[tempObjects.Length];
        MenuPanel.SetActive(false);
        for(int i = 0; i < tempObjects.Length; i++) {
            HUDImages[i] = tempObjects[i].GetComponent<Image>();
        }
        UpdateColor();
    }
    
    public void UpdateColor() {
        foreach(Image img in HUDImages) {
            img.color = new Color(GlobalSettings.Red, GlobalSettings.Green, GlobalSettings.Blue, 0.6f);
        }
    }
}
