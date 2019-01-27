using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitLevel : MonoBehaviour {

    public GameObject warningCanvas;

    public void LeaveLevel() {
        SceneManager.LoadScene("LevelSelect");
    }

    public void DoNotLeaveLevel() {
        warningCanvas.SetActive(false);
    }

    public void OpenWarning() {
        warningCanvas.SetActive(true);
        Time.timeScale = 1;
    }

}
