using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RGBChanger : MonoBehaviour {

    [SerializeField]
    Image[] HUDImages;


    public float Red;
    public float Green;
    public float Blue;

    void Start() {
        Red = GlobalSettings.Red;
        Green = GlobalSettings.Green;
        Blue = GlobalSettings.Blue;
        HUDImages = new Image[10];
        GameObject[] tempObjects = GameObject.FindGameObjectsWithTag("RGBable");
        for(int i = 0; i < tempObjects.Length; i++) {
            HUDImages[i] = tempObjects[i].GetComponent<Image>();
        }
    }
    
    void Update() {
        GlobalSettings.Red = Red;
        GlobalSettings.Green = Green;
        GlobalSettings.Blue = Blue;
        foreach(Image img in HUDImages) {
            img.color = new Color(GlobalSettings.Red/255, GlobalSettings.Green/255, GlobalSettings.Blue/255);
        }
    }
}
