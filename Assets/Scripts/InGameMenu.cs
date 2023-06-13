using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    public void loadMainMenu() {
        LevelController.Instance.loadMainMenu();
    }
}
