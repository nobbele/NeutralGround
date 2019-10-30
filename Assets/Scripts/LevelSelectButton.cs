using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour
{

    public AudioClip Click;
    AudioSource source;

    string Target {
        get {
            return LevelSelectGenerator.LevelPath + name + LevelSelectGenerator.LevelSuffix;
        }
    }

    // Use this for initialization
    void Start() {
        GetComponent<Button>().onClick.AddListener(OnClick);
        source = GetComponent<AudioSource>();
    }
    public static Canvas UI {
        get {
            return GameObject.Find("PlayerUI").GetComponent<Canvas>();
        }
    }

    void OnClick() {
        //source.volume = AudioInfo.Volume;
        source.PlayOneShot(Click);
        AsyncOperation asyncoperation = SceneManager.LoadSceneAsync(Target);
        asyncoperation.completed += (AsyncOperation async) => { UI.enabled = true; ; };
    }
}
public static class GameObjectExtensionMethods
{
    public static GameObject FindObject(this GameObject parent, string name) {
        Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs) {
            if (t.name == name) {
                return t.gameObject;
            }
        }
        return null;
    }
}
