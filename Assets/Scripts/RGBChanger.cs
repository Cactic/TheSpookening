using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RGBChanger : MonoBehaviour {

    [SerializeField]
    Image[] HUDImages;

    void Start() {
        GameObject[] tempObjects = GameObject.FindGameObjectsWithTag("RGBable");
        HUDImages = new Image[tempObjects.Length];
        for(int i = 0; i < tempObjects.Length; i++) {
            HUDImages[i] = tempObjects[i].GetComponent<Image>();
        }
    }
    
    void Update() {
        foreach(Image img in HUDImages) {
            img.color = new Color(GlobalSettings.Red, GlobalSettings.Green, GlobalSettings.Blue);
        }
    }
}
