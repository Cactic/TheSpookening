using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private Text textComponent;    //text field for the story
    [SerializeField] private State startingState;   //first state
    [SerializeField] private Canvas canvas;         //canvas

    [HideInInspector]
    public State currentState;

    [HideInInspector]
    public string[] storyText;

    public Dialogue(string[] dialogue)
    {
        storyText = new string[dialogue.Length];
        for (int i = 0; i < dialogue.Length; i++)
        {
            storyText[i] = dialogue[i];
        }
    }

    // Use this for initialization
    void Start()
    {
        canvas.gameObject.SetActive(false);
        currentState = startingState;
    }

    public void StartDialogue(string[] text)
    {
        StartCoroutine(State.Instance.GetStateText(new Dialogue(text), textComponent, canvas));
    }

}
