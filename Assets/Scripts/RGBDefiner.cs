using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RGBDefiner : MonoBehaviour {

    [SerializeField]
    Slider Red;
    [SerializeField]
    Slider Green;
    [SerializeField]
    Slider Blue;
    [SerializeField]
    RGBChanger RGB;

    private void Start() {
        Red.value = GlobalSettings.Red;
        ChangeRed();
        Green.value = GlobalSettings.Green;
        ChangeGreen();
        Blue.value = GlobalSettings.Blue;
        ChangeBlue();
    }

    public void ChangeRed() {
        GlobalSettings.Red = Red.value;
        RGB.UpdateColor();
    }

    public void ChangeGreen() {
        GlobalSettings.Green = Green.value;
        RGB.UpdateColor();
    }

    public void ChangeBlue() {
        GlobalSettings.Blue = Blue.value;
        RGB.UpdateColor();
    }


}
