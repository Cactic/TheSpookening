using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public List<Slot> InventorySlots;
    public List<Image> Images;

    public Sprite testImage;

    void Start() {
        InventorySlots = new List<Slot>();
        Images = new List<Image>();
        GameObject[] tempImages = GameObject.FindGameObjectsWithTag("Slot Images");
        for(int i = 0; i < tempImages.Length; i++) {
            Images.Add(tempImages[i].GetComponent<Image>());
            InventorySlots.Add(new Slot(i, Images[i], null));
        }
        Draw();
    }

    private void Draw() {
        foreach(Slot s in InventorySlots) {
            s.slot.enabled = true;
            if(s.Item == null) s.slot.enabled = false;
            else s.slot.sprite = s.Item.Image;
        }
    }

    public void Add(Item item) {
        foreach(Slot s in InventorySlots) {
            if(!s.Empty) continue;
            else {
                s.Item = item;
                s.Empty = false;
                break;
            }
        }
        Draw();
    }

    public void Remove(Slot slot) {
        slot.Item = null;
        Draw();
    }
}

public class Slot {

    public int SlotNumber { get; internal set; }
    public Image slot { get; internal set; }
    public bool Empty { get; set; }
    public Item Item { get; set; }

    public Slot(int slotNumber, Image slot, Item item) {
        SlotNumber = slotNumber;
        this.slot = slot;
        Empty = true;
        Item = item;
    }
}
