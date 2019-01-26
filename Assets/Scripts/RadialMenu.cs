using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.AI;

public class RadialMenu : MonoBehaviour
{
    [SerializeField] private GameObject middleImage;
    [SerializeField] private Image interactImage;
    [SerializeField] private Image examineImage;
    [SerializeField] private Image mouthImage;
    [SerializeField] private Camera cam;

    Ray ray;
    RaycastHit hit;
    Image[] optionsImages;
    CanvasRenderer[] canvasRenderers;

    public NavMeshAgent agent;

    public bool showingMenu;
    bool playerMoving;


    // Start is called before the first frame update
    void Start()
    {
        optionsImages = new Image[] { interactImage, examineImage, mouthImage };
        canvasRenderers = new CanvasRenderer[] { interactImage.gameObject.GetComponent<CanvasRenderer>(), examineImage.gameObject.GetComponent<CanvasRenderer>(), mouthImage.gameObject.GetComponent<CanvasRenderer>() };
        foreach (CanvasRenderer canvas in canvasRenderers)
        {
            canvas.SetAlpha(0);
        }

        showingMenu = false;
        playerMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);

        ShowMenuOnMouseClick();

        for (int i = 0; i < canvasRenderers.Length; i++)
        {
            if (canvasRenderers[i].GetAlpha() <= 0.05f)
            {
                canvasRenderers[i].SetAlpha(0);
                middleImage.gameObject.SetActive(false);
            }
        }
        if (playerMoving)
        {
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        showingMenu = true;
                        // Done
                        showingMenu = true;
                        middleImage.gameObject.SetActive(true);

                        for (int i = 0; i < optionsImages.Length; i++)
                        {
                            canvasRenderers[i].SetAlpha(0);
                            optionsImages[i].CrossFadeAlpha(1, 0.25f, false);
                            MoveOptionsMenu();
                        }
                        playerMoving = false;
                    }
                }
            }
        }

        void MoveOptionsMenu()
        {
            middleImage.gameObject.transform.position = Input.mousePosition;
        }

        void ShowMenuOnMouseClick()
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Interactable" && !showingMenu)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        playerMoving = true;


                    }
                }
            }
            /*if (hit.collider.tag == "NotInteractable")
            {
                for (int i = 0; i < optionsImages.Length; i++)
                {
                    optionsImages[i].CrossFadeAlpha(0, 0.25f, false);
                }
            }*/
        }
        if (Input.GetMouseButtonDown(1))
        {
            for (int i = 0; i < optionsImages.Length; i++)
            {
                optionsImages[i].CrossFadeAlpha(0, 0.25f, false);
                showingMenu = false;
            }
        }
    }


    public void InteractButton()
    {
        for (int i = 0; i < optionsImages.Length; i++)
        {
            optionsImages[i].CrossFadeAlpha(0, 0.25f, false);
            showingMenu = false;
        }
        Debug.Log("InteractButton Pressed");

    }

    public void ExamineButton()
    {
        for (int i = 0; i < optionsImages.Length; i++)
        {
            optionsImages[i].CrossFadeAlpha(0, 0.25f, false);
            showingMenu = false;
        }
        Debug.Log("Examine Pressed");

    }

    public void MouthButton()
    {
        for (int i = 0; i < optionsImages.Length; i++)
        {
            optionsImages[i].CrossFadeAlpha(0, 0.25f, false);
            showingMenu = false;
        }
        Debug.Log("MouthButton Pressed");
    }
}