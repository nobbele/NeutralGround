using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform Player1 {
        get {
            return GameObject.Find("WhitePlayer").transform;
        }
    }
    public Transform Player2 { get {
            return GameObject.Find("BlackPlayer").transform;
        }
    }

    // Update is called once per frame
    void Update () {
        float z = transform.position.z;
        Vector3 pos = (Player1.position + Player2.position) / 2; //x + y / 2 gets the avarage of x and y
        pos.z = z;
        transform.position = pos;

        GameObject[] objects = GameObject.FindGameObjectsWithTag("BackgroundAudioController");
        if(objects.Length > 1) {
            for(int i = 1; i < objects.Length; i++) {
                Destroy(objects[i]);
            }
        }
    }
}
