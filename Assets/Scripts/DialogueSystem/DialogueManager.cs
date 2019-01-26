using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
    private static DialogueManager sInstance;
    public static DialogueManager Instance {
        get {
            if(sInstance == null) sInstance = GameObject.Find("TextController").GetComponent<DialogueManager>();
            return sInstance;
        }
    }

    [SerializeField] private Text textComponent;    //text field for the story
    [SerializeField] private Canvas canvas;         //canvas

    [HideInInspector]
    public string[] storyText;

    

    // Use this for initialization
    void Start() {
        canvas.gameObject.SetActive(true);
        State.Instance.Awake();
        canvas.gameObject.SetActive(false);
    }

    public void StartDialogue(string[] text) {
        StartCoroutine(State.Instance.GetStateText(new Dialogue(text), textComponent, canvas));
    }

}

public class Dialogue {
    
    public string[] storyText;

    public Dialogue(string[] dialogue) {
        storyText = new string[dialogue.Length];
        for(int i = 0; i < dialogue.Length; i++) {
            storyText[i] = dialogue[i];
        }
    }
}
