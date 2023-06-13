using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter {
    public string Name;
    public string SceneName;
    public string UnlocksChapter;

    public Chapter(string name, string sceneName, string unlocksChapter) {
        this.Name = name;
        this.SceneName = sceneName;
        this.UnlocksChapter = unlocksChapter;
    }
}
