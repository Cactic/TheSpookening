using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RadialMenu : MonoBehaviour {
    [SerializeField] private GameObject middleImage;
    [SerializeField] private Image interactImage;
    [SerializeField] private Image examineImage;
    [SerializeField] private Image mouthImage;
    [SerializeField] private Camera cam;

    Ray ray;
    RaycastHit hit;
    Image[] optionsImages;
    CanvasRenderer[] canvasRenderers;

    ItemInteraction currentItem;

    [SerializeField] Inventory inventory;

    bool showingMenu;

    float Extents;

    // Start is called before the first frame update
    void Start() {
        optionsImages = new Image[] { interactImage, examineImage, mouthImage };
        canvasRenderers = new CanvasRenderer[] { interactImage.gameObject.GetComponent<CanvasRenderer>(), examineImage.gameObject.GetComponent<CanvasRenderer>(), mouthImage.gameObject.GetComponent<CanvasRenderer>() };
        foreach(CanvasRenderer canvas in canvasRenderers) {
            canvas.SetAlpha(0);
        }

        showingMenu = false;
        Extents = Screen.height / 8;
    }

    void Update() {
        ray = cam.ScreenPointToRay(Input.mousePosition);

        ShowMenuOnMouseClick();

        for(int i = 0; i < canvasRenderers.Length; i++) {
            if(canvasRenderers[i].GetAlpha() <= 0.05f) {
                canvasRenderers[i].SetAlpha(0);
                middleImage.gameObject.SetActive(false);
            }
        }

    }
    void MoveOptionsMenu() {
        middleImage.gameObject.transform.position = Input.mousePosition;
        if(middleImage.gameObject.transform.position.y + Extents > Screen.height) {
            middleImage.gameObject.transform.position = new Vector3(middleImage.gameObject.transform.position.x,
                Screen.height - Extents, middleImage.gameObject.transform.position.z);
        }
    }

    public void ShowMenuOnMouseClick(bool overrule = false, ItemInteraction item = null) {
        if(Physics.Raycast(ray, out hit) && !overrule) {
            if(hit.collider.tag == "Interactable" && !showingMenu) {
                if(Input.GetMouseButtonDown(0)) {
                    currentItem = hit.collider.gameObject.GetComponent<ItemInteraction>();
                    showingMenu = true;

                    middleImage.gameObject.SetActive(true);
                    for(int i = 0; i < optionsImages.Length; i++) {
                        canvasRenderers[i].SetAlpha(0);
                        optionsImages[i].CrossFadeAlpha(1, 0.25f, false);
                        MoveOptionsMenu();
                    }
                }
            }
        }
        if(overrule) {
            print("made it");
            print(item.name);
            currentItem = item;
            showingMenu = true;

            middleImage.gameObject.SetActive(true);
            for(int i = 0; i < optionsImages.Length; i++) {
                canvasRenderers[i].SetAlpha(0);
                optionsImages[i].CrossFadeAlpha(1, 0.25f, false);
                MoveOptionsMenu();
            }
        }
        /*if (hit.collider.tag == "NotInteractable")
        {
            for (int i = 0; i < optionsImages.Length; i++)
            {
                optionsImages[i].CrossFadeAlpha(0, 0.25f, false);
            }
        }*/

        if(Input.GetMouseButtonDown(1)) {
            currentItem = null;
            for(int i = 0; i < optionsImages.Length; i++) {
                optionsImages[i].CrossFadeAlpha(0, 0.25f, false);
                showingMenu = false;
            }
        }
    }


    public void InteractButton() {
        for(int i = 0; i < optionsImages.Length; i++) {
            optionsImages[i].CrossFadeAlpha(0, 0.25f, false);
            showingMenu = false;
        }
        if(!currentItem.Name.Contains("in inventory"))inventory.Add(new Item(currentItem.Name, currentItem.Eyes, currentItem.Mouth, currentItem.Image, currentItem));
        currentItem.gameObject.tag = "Untagged";
        currentItem.gameObject.GetComponent<GlowObject>().GlowColor = Color.black;
        currentItem = null;
    }

    public void ExamineButton() {
        for(int i = 0; i < optionsImages.Length; i++) {
            optionsImages[i].CrossFadeAlpha(0, 0.25f, false);
            showingMenu = false;
        }
        Debug.Log(currentItem.Eyes);
        currentItem = null;

    }

    public void MouthButton() {
        for(int i = 0; i < optionsImages.Length; i++) {
            optionsImages[i].CrossFadeAlpha(0, 0.25f, false);
            showingMenu = false;
        }
        Debug.Log(currentItem.Mouth);
        currentItem = null;
    }
}