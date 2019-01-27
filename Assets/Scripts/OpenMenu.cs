using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenu : MonoBehaviour {

    [SerializeField]
    GameObject MenuPanel;
    [SerializeField]
    GameObject warningMenu;

    float BaseTime;

    private void Start() {
        BaseTime = Time.timeScale;
    }

    public void Toggle() {

        if(!MenuPanel.activeSelf) {
            MenuPanel.SetActive(true);
            Time.timeScale = 0;
        } else {
            MenuPanel.SetActive(false);
            warningMenu.SetActive(false);
            Time.timeScale = BaseTime;
        }
    }
}
