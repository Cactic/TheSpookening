using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;
    public Animator anim;
    public RaycastHit hit;

    public RadialMenu thisRadialMenu;

    private void Update()
    {
        //and mouse not hovering over obj.
        if (thisRadialMenu.showingMenu == false && State.Instance.isClicked == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                thisRadialMenu.middleImage.transform.position = Input.mousePosition;
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    //move agent

                    agent.SetDestination(hit.point);
                    anim.SetFloat("Speed", 1);
                }
            }
        }

        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    // Done
                    anim.SetFloat("Speed", 0);

                }
            }
        }

    }
}
