using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {

    public string Name { get; internal set; }
    public string Eyes { get; internal set; }
    public string Mouth { get; internal set; }
    public Sprite Image { get; internal set; }

    public Item(string name, string eyes, string mouth, Sprite image) {
        Name = name;
        Eyes = eyes;
        Mouth = mouth;
        Image = image;
    }
}
