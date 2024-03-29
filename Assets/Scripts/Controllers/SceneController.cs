using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    void Start() {
        GameController.Instance.Reload();
    }

    public static void nextPuzzle() {
        GameController.Instance.nextPuzzle();
    }

    public void unpauseGame() {
        GameController.Instance.hideMenu();
    }

    public void loadMainMenu() {
        unpauseGame();
        LevelController.Instance.loadMainMenu();
    }
}
