using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle {
    public string Name;
    public string Description;
    public string PrefabName;

    public Puzzle(string name, string description, string prefabName) {
        this.Name = name;
        this.Description = description;
        this.PrefabName = prefabName;
    }
}
