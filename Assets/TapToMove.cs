using UnityEngine;
using System.Collections;

public class TapToMove : MonoBehaviour
{
    private bool flag = false;
    private Vector3 endPoint;
    public float duration = 10f;
    private float yAxis;

    void Start()
    {
        yAxis = gameObject.transform.position.y;
    }

    void Update()
    {
        //check if the screen is clicked   
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray;

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //Check if the ray hits any collider
            if (Physics.Raycast(ray, out hit))
            {
                //set a flag to indicate to move the gameobject
                flag = true;
                //save the click / tap position
                endPoint = hit.point;
                //not change y axis, but hold original
                endPoint.y = yAxis;
            }

        }
        //check if the flag for movement is true and the current gameobject position is not same as the clicked position
        if (flag && !Mathf.Approximately(gameObject.transform.position.magnitude, endPoint.magnitude))
        {
            //move the gameobject to the desired position
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, endPoint, 1 / (duration * (Vector3.Distance(gameObject.transform.position, endPoint))));
        }
        //set the movement indicator flag to false if the endPoint and current gameobject position are equal
        else if (flag && Mathf.Approximately(gameObject.transform.position.magnitude, endPoint.magnitude))
        {
            flag = false;
            Debug.Log("I am here");
        }

    }
}