using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts2D.Functionalities
{
    public class LevelsList : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _levelPrefabs;

        public GameObject GetPrefabByName(string pName)
        {
            return _levelPrefabs.First(p => p.name == pName);
        }
    }
}
