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
    public bool Saveable;

    public Renderer Ghost;
    public Material Happy;
    public ParticleSystem[] Tears;
}
