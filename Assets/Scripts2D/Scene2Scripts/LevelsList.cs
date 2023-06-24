using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelsList : MonoBehaviour
{
    [SerializeField] private List<GameObject> _levelPrefabs;

    public GameObject GetPrefabByName(string pName)
    {
        return _levelPrefabs.First(p => p.name == pName);
    }
}
