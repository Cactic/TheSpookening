using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    private static AudioManager sInstance;
    public static AudioManager Instance {
        get {
            if(sInstance == null) sInstance = GameObject.Find("TextController").GetComponent<AudioManager>();
            return sInstance;
        }
    }


    public AudioSource[] voiceOvers;
    public AudioSource currentVoiceOverSource;
    public bool[] hasPlayed = new bool[128];
    private int currentVoiceOver = -1;
    private int prevVoiceOver;

    /// <summary>
    /// Initialize class variables on startup
    /// </summary>
    void Start() {
        for(int i = 0; i < 128; i++) {
            hasPlayed[i] = false;
        }
    }

    /// <summary>
    /// Play a new voice-over if it hasn't been played before and stop the previously playing voice-over
    /// </summary>
    public void PlayVoiceOver(int voiceOverID) {
        if(hasPlayed[voiceOverID]) return;
        if(currentVoiceOverSource != null) currentVoiceOverSource.Stop();
        currentVoiceOver = voiceOverID;
        //hasPlayed[voiceOverID] = true;
    }

    // Update is called once per frame

    /// <summary>
    /// Check if the currentVoiceOverID has changed. If it has, play it and set the currentVoiceOverID to -1
    /// Also lowers the volume of any background music if a voice-over is playing
    /// </summary>
    void Update() {
        if(currentVoiceOver != -1 && !voiceOvers[currentVoiceOver].isPlaying) {
            voiceOvers[currentVoiceOver].Play();
            currentVoiceOverSource = voiceOvers[currentVoiceOver];
            currentVoiceOver = -1;
        }
    }
}
