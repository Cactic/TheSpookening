using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class State : MonoBehaviour
{
    private static State sInstance;
    public static State Instance
    {
        get
        {
            if (sInstance == null)
            {
                sInstance = new GameObject("Dialogue", typeof(State)).GetComponent<State>();
            }
            return sInstance;

        }
    }

    public IEnumerator GetStateText(Dialogue dialogue, Text text, Canvas canvas)
    {
        foreach (string s in dialogue.storyText)
        {
            canvas.gameObject.SetActive(true);
            text.text = s;


            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            yield return new WaitForEndOfFrame();
        }
        canvas.gameObject.SetActive(false);
        yield break;
    }
}
