using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance = null;

    private List<string> unlockedChapters = new List<string>();
    
    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        // Unlock tutorial chapter by default
        unlockChapter("Chapter0");
    }

    public List<string> getUnlockedChapters() {
        return unlockedChapters;
    }

    public void unlockChapter(string chapter) {
        if (!this.unlockedChapters.Contains(chapter)) {
            this.unlockedChapters.Add(chapter);
        }
    }

    public void loadChapter(string chapter) {
        if (this.unlockedChapters.Contains(chapter)) {
            SceneManager.LoadScene(chapter, LoadSceneMode.Single);
        }
    }
}
