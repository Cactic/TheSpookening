using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {

    public string Name { get; internal set; }
    public string Eyes { get; internal set; }
    public string Mouth { get; internal set; }
    public Sprite Image { get; internal set; }
    public ItemInteraction ItemInteraction { get; internal set; }

    public Item(string name, string eyes, string mouth, Sprite image, ItemInteraction itemInteraction) {
        Name = name;
        Eyes = eyes;
        Mouth = mouth;
        Image = image;
        ItemInteraction = itemInteraction;
    }
}
