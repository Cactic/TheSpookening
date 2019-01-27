using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public Camera cam;
    public GameObject[] cameraPoints;
    public GameObject leftButtonObject, rightButtonObject;
    [SerializeField] GameObject currentGameObject;
    Vector3 currentPosition;

    public GameObject block;
    public GameObject startButton;

    private float tweenValue;
    private float tweenTimeKey;

    public AnimationCurve MoveCameraCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    bool MoveCameraTweenBusy;
    public float duration;
    bool buttonPressed;


    // Start is called before the first frame update
    void Start()
    {
        buttonPressed = true;

        currentGameObject = cameraPoints[0];
        currentPosition = currentGameObject.transform.position;
        cam.transform.position = currentPosition;

        block.SetActive(false);
        startButton.SetActive(true);
    }


    void MoveCameraAnimation()
    {
        tweenTimeKey = 0;
        MoveCameraTweenBusy = true;
    }

    private void MoveCameraTween()
    {
        startButton.SetActive(false);
        block.SetActive(true);
        tweenTimeKey += Time.smoothDeltaTime / duration;
        tweenValue = MoveCameraCurve.Evaluate(tweenTimeKey);
        transform.position = Vector3.Lerp(currentPosition, currentGameObject.transform.position, tweenValue);
        Debug.Log(transform.position);
        //Debug.Log(tweenTimeKey);

        if (tweenTimeKey > 1f)
        {
            tweenTimeKey = 0;
            currentPosition = transform.position;
            MoveCameraTweenBusy = false;
            tweenValue = MoveCameraCurve.Evaluate(tweenTimeKey);
            cam.transform.position = currentGameObject.transform.position;
            block.SetActive(false);
            startButton.SetActive(true);

            //Debug.Log(currentGameObject.transform.position);
            //Debug.Log(currentGameObject.transform.position);
        }
    }

    public void MoveCameraPositionRight()
    {
        if (currentGameObject == cameraPoints[0] && buttonPressed)
        {
            currentGameObject = cameraPoints[1];
            buttonPressed = false;
        }
        else if (currentGameObject == cameraPoints[1] && buttonPressed)
        {
            currentGameObject = cameraPoints[2];
            buttonPressed = false;
        }
        else if (currentGameObject == cameraPoints[2] && buttonPressed)
        {
            currentGameObject = cameraPoints[0];
            buttonPressed = false;
        }
        buttonPressed = true;
        MoveCameraAnimation();
    }

    public void MoveCameraPositionLeft()
    {

        if (currentGameObject == cameraPoints[2] && buttonPressed)
        {
            currentGameObject = cameraPoints[1];
            buttonPressed = false;

        }
        else if (currentGameObject == cameraPoints[1] && buttonPressed)
        {
            currentGameObject = cameraPoints[0];
            buttonPressed = false;
        }
        else if (currentGameObject == cameraPoints[0] && buttonPressed)
        {
            currentGameObject = cameraPoints[2];
            buttonPressed = false;
        }
        buttonPressed = true;
        MoveCameraAnimation();

    }

    public void StartLevel()
    {
        if(currentGameObject == cameraPoints[0])
        {
            SceneManager.LoadScene("LevelOne");
        }
        else if(currentGameObject == cameraPoints[1])
        {
            //goto LevelTwo
        }
        else if(currentGameObject == cameraPoints[2])
        {
            //goto LevelThree
        }
    }

    private void Update()
    {
        if (MoveCameraTweenBusy)
        {
            MoveCameraTween();
        }
    }
}
