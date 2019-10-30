using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalHandler : MonoBehaviour {

    const string Prefix = "Level";

    string Target {
        get {
            return GetLevel(1);
        }
    }

    private void Awake() {
        BlackStanding = false;
        WhiteStanding = false;
    }

    public static string GetLevel(int add) {
        string name = SceneManager.GetActiveScene().name;
        string num = name.Substring(Prefix.Length, 1);
        return name.Substring(0, Prefix.Length) + (int.Parse(num) + add);
    }

    public static bool BlackStanding, WhiteStanding;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name == "BlackPlayer")
            BlackStanding = true;
        else if (collision.gameObject.name == "WhitePlayer")
            WhiteStanding = true;

        if(BlackStanding && WhiteStanding) {
            if(Application.CanStreamedLevelBeLoaded(Target))
                SceneManager.LoadScene(Target);
            else {
                //Completed game
                LoadLevelSelect();
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.name == "BlackPlayer")
            BlackStanding = false;
        else if (collision.gameObject.name == "WhitePlayer")
            WhiteStanding = false;
    }

    public static void LoadLevelSelect() {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("LevelSelect");
        asyncLoad.completed += (AsyncOperation op) => {
            LevelSelectButton.UI.enabled = false; //Same as above but for the UI
            GameObject playerTrack = GameObject.Find("PlayerTrackCamera");
            if (playerTrack != null)
                Destroy(playerTrack); //Because the main camera is a dont destroy on load, this removes the camera before going to the level select menu
        };
    }
}
