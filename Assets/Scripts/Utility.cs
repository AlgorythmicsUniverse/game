using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility {
    public static GameObject[] getNearbyObjectsWithTag(Vector3 source, string tag, float minimumDistance) {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);

        List<GameObject> nearbyObjects = new List<GameObject>();
        foreach (GameObject obj in objects) {
            if (Vector3.Distance(source, obj.transform.position) <= minimumDistance) {
                nearbyObjects.Add(obj);
            }
        }

        return nearbyObjects.ToArray();
    }
}
