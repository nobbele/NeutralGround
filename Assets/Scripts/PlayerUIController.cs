using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour {

    private static bool created = false;

    void Awake() {
        if (!created) {
            DontDestroyOnLoad(gameObject);
            created = true;
        } else {
            Destroy(gameObject);
        }
        GetComponent<Canvas>().enabled = false;
    }
    private void Update() {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PlayerUI");
        if (objects.Length > 1) {
            for (int i = 1; i < objects.Length; i++) {
                Destroy(objects[i]);
            }
        }
#if DEBUG
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
#endif
    }
#if DEBUG
    float deltaTime = 0.0f;

    void OnGUI() {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
    }
#endif

    private void OnDisable() {
        created = false;
    }
}
