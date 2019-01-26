using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {

    public string Name { get; internal set; }
    public string[] Eyes { get; internal set; }
    public string[] Mouth { get; internal set; }
    public string[] Hand { get; internal set; }
    public string[] HandAfterPickup { get; internal set; }
    public Sprite Image { get; internal set; }
    public ItemInteraction ItemInteraction { get; internal set; }

    public Item(string name, string[] eyes, string[] mouth, string[] hand, string[] handAfterPickup, Sprite image, ItemInteraction itemInteraction) {
        Name = name;
        Eyes = eyes;
        Mouth = mouth;
        Hand = hand;
        HandAfterPickup = handAfterPickup;
        Image = image;
        ItemInteraction = itemInteraction;
    }
}
