using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    void Start() {
        GameController.Instance.Reload();
    }

    public void nextPuzzle() {
        GameController.Instance.nextPuzzle();
    }
}
