using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialMenu : MonoBehaviour
{

    [SerializeField] private Image optionsImage;
    [SerializeField] private Camera cam;

    Ray ray;
    RaycastHit hit;

    CanvasRenderer canvasRenderer;  
        // Start is called before the first frame update
    void Start()
    {
        canvasRenderer = optionsImage.gameObject.GetComponent<CanvasRenderer>();
        canvasRenderer.SetAlpha(0);
    }

    // Update is called once per frame
    void Update()
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);

        ShowMenuOnMouseClick();

        if  (canvasRenderer.GetAlpha() <= 0.05)
        {
            canvasRenderer.SetAlpha(0);
            optionsImage.gameObject.SetActive(false);
        }

        print(canvasRenderer.GetAlpha());
    }
    void MoveOptionsMenu()
    {
        optionsImage.gameObject.transform.position = Input.mousePosition;
    }

    void ShowMenuOnMouseClick()
    {
        if (Physics.Raycast(ray, out hit) )
        {
            if(hit.collider.tag == "Interactable")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    optionsImage.gameObject.SetActive(true);
                    optionsImage.CrossFadeAlpha(1, 0.25f, false);
                    MoveOptionsMenu();
                }
            }
        }
        else
        {
            optionsImage.CrossFadeAlpha(0,0.25f,false);
        }
    }
}