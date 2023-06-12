using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject ChaptersGroup;

    public void refreshChapters() {
        List<string> unlockedChapters = LevelController.Instance.getUnlockedChapters();

        Button[] chapterButtons = ChaptersGroup.GetComponentsInChildren<Button>();
        foreach (var chapter in chapterButtons) {
            chapter.interactable = unlockedChapters.Contains(chapter.name);
        }
    }

    public void loadChapter(string chapter) {
        LevelController.Instance.loadChapter(chapter);
    }

    public void quitGame() {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
