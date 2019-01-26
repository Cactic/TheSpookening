using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class graphicRaycast : MonoBehaviour
{
    GraphicRaycaster gr;
    PointerEventData ped;
    EventSystem es;
    public List<GameObject> go = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        gr = GetComponent<GraphicRaycaster>();
        es = GetComponent<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ped = new PointerEventData(es);
            ped.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();

            gr.Raycast(ped, results);

            foreach(RaycastResult result in results)
            {
                Debug.Log(result.gameObject);
            }
        }
    }
}
