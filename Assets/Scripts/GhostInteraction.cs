using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostInteraction : MonoBehaviour
{
    public string Name;
    public string[] Eyes;
    public string[] Mouth;
    public string[] MouthSaveable;
    public string[] Hand;
    public int[] VoiceOversEyes;
    public int[] VoiceOversMouth;
    public int[] VoiceOversHand;
    public int[] VoiceOversSaveable;
    public bool Saveable;

    public Renderer Ghost;
    public Material Happy;
    public ParticleSystem[] Tears;
}
