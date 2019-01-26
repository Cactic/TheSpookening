using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenu : MonoBehaviour {

    [SerializeField]
    GameObject MenuPanel;

    public void Toggle() {
        if(!MenuPanel.activeSelf) MenuPanel.SetActive(true);
        else MenuPanel.SetActive(false);
    }
}
