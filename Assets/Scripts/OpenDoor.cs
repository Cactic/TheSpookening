using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour {
    
    public IEnumerator Open(Quaternion target) {
        float step = 0;
        Quaternion rotation = transform.rotation;
        while(transform.rotation != target) {
            transform.rotation = Quaternion.Lerp(rotation, target, step);
            step += 0.005f;
            yield return new WaitForSeconds(0f);
        }
        transform.GetChild(0).GetComponent<GlowObject>().GlowColor = Color.red;
        transform.GetChild(0).tag = "Open Door";
        yield break;
    }
}
