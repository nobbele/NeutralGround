using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundAudioController : MonoBehaviour {

    private static bool created = false;

    void Awake() {
        if (!created) {
            DontDestroyOnLoad(gameObject);
            created = true;
        } else {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update() {
        if (SceneManager.GetActiveScene().name == "LevelSelect")
            Destroy(gameObject);
    }

    private void OnDestroy() {
        created = false;
    }
}
