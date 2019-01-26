using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.AI;

public class RadialMenu : MonoBehaviour {
    [SerializeField] private GameObject middleImage;
    [SerializeField] private Image interactImage;
    [SerializeField] private Image examineImage;
    [SerializeField] private Image mouthImage;
    [SerializeField] private Camera cam;

    public GameObject TextController;

    Ray ray;
    RaycastHit hit;
    Image[] optionsImages;
    CanvasRenderer[] canvasRenderers;

    ItemInteraction currentItem;
    public NavMeshAgent agent;

    [SerializeField] Inventory inventory;

    public bool showingMenu;
    bool playerMoving;


    float Extents;

    // Start is called before the first frame update
    void Start() {
        gameObject.SetActive(true);
        optionsImages = new Image[] { interactImage, examineImage, mouthImage };
        canvasRenderers = new CanvasRenderer[] { interactImage.gameObject.GetComponent<CanvasRenderer>(), examineImage.gameObject.GetComponent<CanvasRenderer>(), mouthImage.gameObject.GetComponent<CanvasRenderer>() };
        foreach(CanvasRenderer canvas in canvasRenderers) {
            canvas.SetAlpha(0);
        }

        showingMenu = false;
        Extents = Screen.height / 8;
        playerMoving = false;
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
        if(playerMoving) {
            if(!agent.pathPending) {
                if(agent.remainingDistance <= agent.stoppingDistance) {
                    if(!agent.hasPath || agent.velocity.sqrMagnitude == 0f) {
                        showingMenu = true;
                        // Done
                        showingMenu = true;
                        middleImage.gameObject.SetActive(true);

                        for(int i = 0; i < optionsImages.Length; i++) {
                            canvasRenderers[i].SetAlpha(0);
                            optionsImages[i].CrossFadeAlpha(1, 0.25f, false);
                            MoveOptionsMenu();
                        }
                        playerMoving = false;
                    }
                }
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
                    playerMoving = true;


                }
            }
        }
        if(overrule) {
            currentItem = item;
            print(item.Hand);
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
        if(currentItem.Hand.Length >= 1) {
            TextController.GetComponent<DialogueManager>().StartDialogue(currentItem.Hand);
            return;
        }
        if(!currentItem.Name.Contains("in inventory")) inventory.Add(new Item(currentItem.Name, currentItem.Eyes, currentItem.Mouth,
            currentItem.Hand, currentItem.HandAfterPickup, currentItem.Image, currentItem));
        currentItem.gameObject.tag = "Untagged";
        currentItem.gameObject.GetComponent<GlowObject>().GlowColor = Color.black;
        currentItem = null;
    }

    public void ExamineButton() {
        for(int i = 0; i < optionsImages.Length; i++) {
            optionsImages[i].CrossFadeAlpha(0, 0.25f, false);
            showingMenu = false;
        }
        TextController.GetComponent<DialogueManager>().StartDialogue(currentItem.Eyes);
        currentItem = null;

    }

    public void MouthButton() {
        for(int i = 0; i < optionsImages.Length; i++) {
            optionsImages[i].CrossFadeAlpha(0, 0.25f, false);
            showingMenu = false;
        }
        TextController.GetComponent<DialogueManager>().StartDialogue(currentItem.Mouth);
        currentItem = null;
    }
}