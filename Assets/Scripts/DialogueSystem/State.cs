using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class State : MonoBehaviour
{
    public bool isClicked;
    private static State sInstance;

    public static State Instance
    {
        get
        {
            if (sInstance == null)
            {
                sInstance = GameObject.Find("Dialogue").AddComponent<State>();
            }
            return sInstance;
        }
    }

    public void Awake()
    {
        isClicked = false;
    }

    public IEnumerator GetStateText(Dialogue dialogue, Text text, Canvas canvas)
    {
        for(int i = 0; i < dialogue.storyText.Length; i++) {
            canvas.gameObject.SetActive(true);
            text.text = dialogue.storyText[i];
            AudioManager.Instance.PlayVoiceOver(i);
            isClicked = true;

            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            yield return new WaitForEndOfFrame();
        }
        //foreach (string s in dialogue.storyText)
        //{
        //    canvas.gameObject.SetActive(true);
        //    text.text = s;
        //    isClicked = true;
        //
        //    yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        //    yield return new WaitForEndOfFrame();
        //    AudioManager.Instance.PlayVoiceOver(ID);
        //}
        canvas.gameObject.SetActive(false);
        isClicked = false;
        yield break;
    }


}
