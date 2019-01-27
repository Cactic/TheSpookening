using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class RadialMenu : MonoBehaviour {
    [SerializeField] public GameObject middleImage;
    [SerializeField] private Image interactImage;
    [SerializeField] private Image examineImage;
    [SerializeField] private Image mouthImage;
    [SerializeField] private Image mouthIcon;
    [SerializeField] private Image examineIcon;
    [SerializeField] private Image interactionIcon;
    [SerializeField] private Camera cam;


    public GameObject TextController;

    Ray ray;
    RaycastHit hit;
    Image[] optionsImages;
    CanvasRenderer[] canvasRenderers;

    public GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    public EventSystem m_EventSystem;

    ItemInteraction currentItem;
    GhostInteraction currentGhost;
    DoorInteraction currentDoor;
    public NavMeshAgent agent;

    [SerializeField] Inventory inventory;
    [SerializeField] OpenDoor door;
    [SerializeField] GameObject openDoor;

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
        if(currentGhost != null) print(currentGhost.Saveable);
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
        if(!playerMoving) middleImage.gameObject.transform.position = Input.mousePosition;
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
            if(hit.collider.tag == "Ghost" && !showingMenu) {
                if(Input.GetMouseButtonDown(0)) {
                    currentGhost = hit.collider.gameObject.GetComponent<GhostInteraction>();
                    playerMoving = true;
                }
            }
            if(hit.collider.tag == "Open Door" && !showingMenu) {
                if(Input.GetMouseButtonDown(0)) {
                    currentDoor = hit.collider.gameObject.GetComponent<DoorInteraction>();
                    playerMoving = true;
                }
            }
        }
        if(overrule) {
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

        m_PointerEventData = new PointerEventData(m_EventSystem);
        m_PointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        List<string> nameResults = new List<string>();
        m_Raycaster.Raycast(m_PointerEventData, results);
        foreach(RaycastResult r in results) {
            nameResults.Add(r.gameObject.name);
        }
        if(!nameResults.Contains("MiddleImage")) {
            for(int i = 0; i < optionsImages.Length; i++) {
                optionsImages[i].CrossFadeAlpha(0, 0.25f, false);
                showingMenu = false;
            }
            //StartCoroutine(RemoveItem());
        }
        //if(Input.GetMouseButtonDown(1)) {
        //    currentItem = null;
        //    for(int i = 0; i < optionsImages.Length; i++) {
        //        optionsImages[i].CrossFadeAlpha(0, 0.25f, false);
        //        showingMenu = false;
        //    }
        //}
    }

    //IEnumerator RemoveItem() {
    //    yield return new WaitForSeconds(1);
    //    currentItem = null;
    //    currentGhost = null;
    //    currentDoor = null;
    //    yield break;
    //}


    public void InteractButton() {
        for(int i = 0; i < optionsImages.Length; i++) {
            optionsImages[i].CrossFadeAlpha(0, 0.25f, false);
            showingMenu = false;
        }
        if(currentItem != null && currentItem.Hand.Length >= 1) {
            DialogueManager.Instance.StartDialogue(currentItem.Hand, currentItem.VoiceOversHand);
            currentItem = null;
            return;
        }
        if(currentGhost != null && currentGhost.Hand.Length >= 1) {
            DialogueManager.Instance.StartDialogue(currentGhost.Hand, currentGhost.VoiceOversHand);
            currentGhost = null;
            return;
        }
        if(currentDoor != null && currentDoor.Hand.Length <= 0) {
            DialogueManager.Instance.StartDialogue(currentDoor.Hand, currentDoor.VoiceOversHand);
            currentDoor = null;
            SceneManager.LoadScene("LevelSelect");
            return;
        }
        if(!currentItem.Name.Contains("in inventory")) inventory.Add(new Item(currentItem.Name, currentItem.Eyes, currentItem.Mouth,
            currentItem.Hand, currentItem.HandAfterPickup, currentItem.Image, currentItem));
        currentItem.gameObject.tag = "Untagged";
        currentItem.gameObject.GetComponent<GlowObject>().GlowColor = Color.black;
        Globals.FirstStoryProgress++;
        currentItem = null;
    }

    public void ExamineButton() {
        for(int i = 0; i < optionsImages.Length; i++) {
            optionsImages[i].CrossFadeAlpha(0, 0.25f, false);
            showingMenu = false;
        }
        if(currentItem != null) DialogueManager.Instance.StartDialogue(currentItem.Eyes, currentItem.VoiceOversEyes);
        if(currentGhost != null) DialogueManager.Instance.StartDialogue(currentGhost.Eyes, currentGhost.VoiceOversEyes);
        if(currentDoor != null) DialogueManager.Instance.StartDialogue(currentDoor.Eyes, currentDoor.VoiceOversEyes);
        currentItem = null;
        currentGhost = null;
        currentDoor = null;

    }

    public void MouthButton() {
        for(int i = 0; i < optionsImages.Length; i++) {
            optionsImages[i].CrossFadeAlpha(0, 0.25f, false);
            showingMenu = false;
        }
        if(currentItem != null) DialogueManager.Instance.StartDialogue(currentItem.Mouth, currentItem.VoiceOversMouth);
        if(currentDoor != null) DialogueManager.Instance.StartDialogue(currentDoor.Mouth, currentDoor.VoiceOversMouth);
        if(currentGhost != null && currentGhost.Saveable) {
            DialogueManager.Instance.StartDialogue(currentGhost.MouthSaveable, currentGhost.VoiceOversSaveable);
            currentGhost.Ghost.material = currentGhost.Happy;
            foreach(ParticleSystem p in currentGhost.Tears) {
                p.Stop();
            }
            StartCoroutine(door.Open(openDoor.transform.rotation));
        } else if(currentGhost != null) DialogueManager.Instance.StartDialogue(currentGhost.Mouth, currentGhost.VoiceOversMouth);
        currentItem = null;
        currentGhost = null;
        currentDoor = null;
    }
}