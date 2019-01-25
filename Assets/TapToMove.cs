using UnityEngine;
using System.Collections;

public class TapToMove : MonoBehaviour
{
    private bool flag = false;
    private Vector3 endPoint;
    public float duration = 10f;
    private float yAxis;

    private Quaternion LookRotation;
    private Vector3 Direction;
    public float RotationSpeed;

    public Animator Anim;

    RaycastHit hit;
    Ray ray;

    bool isClicked;

    void Start()
    {
        yAxis = gameObject.transform.position.y;
        isClicked = false;
    }

    void Update()
    {

        //check if the screen is clicked   
        if (Input.GetMouseButtonDown(0))
        {
            if (flag == false)
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            }

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

            //find the vector pointing from our position to the target
            Direction = (endPoint - transform.position).normalized;

            //create the rotation we need to be in to look at the target
            LookRotation = Quaternion.LookRotation(Direction);

            //rotate us over time according to speed until we are in the required rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, LookRotation, RotationSpeed);

        }
        //check if the flag for movement is true and the current gameobject position is not same as the clicked position
        if (flag && !Mathf.Approximately(gameObject.transform.position.magnitude, endPoint.magnitude))
        {
            //move the gameobject to the desired position
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, endPoint, 1 / (duration * (Vector3.Distance(gameObject.transform.position, endPoint))));
            Anim.SetFloat("Speed", 1);
        }
        //set the movement indicator flag to false if the endPoint and current gameobject position are equal
        else if (flag && Mathf.Approximately(gameObject.transform.position.magnitude, endPoint.magnitude))
        {
            flag = false;
            Debug.Log("Arrived");
            Anim.SetFloat("Speed", 0);
        }

    }
}