using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectGenerator : MonoBehaviour {

    public static int LevelCount { get; set; }

    public int XCount = 5, YCount = 5;
    public int XSpacing = 10, YSpacing = 25;
    public Transform Canvas;

    public GameObject LevelButtonPrefab;

    public int ButtonWidth, ButtonHeight;

    public static string LevelPath = "Assets/Scenes/Level";
    public static string LevelSuffix = ".unity";

    public string Format = "Level {0}";

    int GetXPos(int index) {
        return (index * (XSpacing + ButtonWidth)) + (ButtonWidth / 2);
    }
    int GetYPos(int row) {
        return (row * (YSpacing + ButtonHeight)) + (ButtonHeight / 2);
    }
    Vector2 GetStartingLocalPos() {
        return new Vector2(transform.localPosition.x, transform.localPosition.y);
    }
    Vector2 GetStartingGlobalPos() {
        return new Vector2(transform.position.x, transform.position.y);
    }

	void Start () {
        LevelCount = SceneManager.sceneCountInBuildSettings - 1;
        RectTransform canvasRect = Canvas.GetComponent<RectTransform>();

        for (int i = 0, row = 0, total = 0; total < LevelCount; i++) {
            if (i > XCount - 1) {
                
            }
            GameObject instance = Instantiate(LevelButtonPrefab, Canvas);

            RectTransform rect = instance.GetComponent<RectTransform>();
            Vector2 position = GetStartingLocalPos();

            int x = GetXPos(i);
            if (GetStartingGlobalPos().x + x + (ButtonWidth / 2) > canvasRect.rect.width) {
                row++;
                i = 0;
                x = GetXPos(i);
            }
            position.x += x;
            position.y -= GetYPos(row);

            rect.localPosition = position;

            Text text = instance.GetComponentInChildren<Text>();
            string name = string.Format(Format, total + 1);
            text.text = name;

            instance.name = total.ToString();

            total++;
        }
    }
}
