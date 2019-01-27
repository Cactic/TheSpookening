﻿using System;
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

    private void Update() {
        if(Globals.FirstStoryProgress >= 3 && !Globals.PlayingEndingFirstStory) {
            StartDialogue(new string[] { "I think I have collected everything around here.",
            "Knowing that ghosts are stuck in our world because they are missing something and this little seems sad",
            "I wonder what the clues I have found are telling me...Let's recount",
            "I found a portrait of a couple sharing a hug and a kiss, I found a valentines coffee cup and I found a comfortable chair",
            "Somehow I get the feeling the chair might not have been a clue, but if I look at the other items, I think I know what this ghost is missing",
            "This ghost is missing the love of the living, I think all he needs is a good old hug."});
            Globals.PlayingEndingFirstStory = true;
        }
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
