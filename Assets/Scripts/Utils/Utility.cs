using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility {
    public static GameObject[] getNearbyObjectsWithTag(Vector3 center, string tag, float minimumDistance) {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);

        List<GameObject> nearbyObjects = new List<GameObject>();
        foreach (GameObject obj in objects) {
            if (Vector3.Distance(center, obj.transform.position) <= minimumDistance) {
                nearbyObjects.Add(obj);
            }
        }

        return nearbyObjects.ToArray();
    }

    public static GameObject[] getNearbyInteractables(Vector3 center, float minimumDistance) {
        Interactable[] interactables = GameObject.FindObjectsOfType<Interactable>();

        List<GameObject> objects = new List<GameObject>();
        foreach (Interactable interactable in interactables) {
            if (interactable.Enabled) {
                GameObject obj = interactable.gameObject;
                if (Vector3.Distance(center, obj.transform.position) <= minimumDistance) {
                    objects.Add(obj);
                }
            }
        }

        return objects.ToArray();
    }

    public static GameObject getClosestObject(Vector3 center, GameObject[] objects) {
        GameObject closest = null;
        if (objects.Length > 0) {
            closest = objects[0];
        }
        foreach (GameObject obj in objects) {
            if (Vector3.Distance(center, obj.transform.position) < Vector3.Distance(center, closest.transform.position)) {
                closest = obj;
            }
        }

        return closest;
    }

    public static void pointObjectsTowardsTarget(Vector3 target, GameObject[] objects) {
        foreach (GameObject obj in objects) {
            var lookPos = obj.transform.position - target;
            lookPos.y = 0;

            var rotation = Quaternion.LookRotation(lookPos);
            obj.transform.rotation = rotation;
        }
    }

    public static void pointObjectsTowardsCamera(GameObject[] objects) {
        Vector3 target = Camera.main.transform.position;
        pointObjectsTowardsTarget(target, objects);
    }

    public static IEnumerator moveOverSpeed(GameObject obj, Vector3 target, float speed, float delay=0) {
        yield return new WaitForSeconds(delay);

        while (obj.transform.position != target) {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, target, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    public static IEnumerator moveOverSeconds(GameObject obj, Vector3 target, float seconds, float delay=0) {
        yield return new WaitForSeconds(delay);
        
        float elapsedTime = 0;
        Vector3 startingPos = obj.transform.position;
        while (elapsedTime < seconds) {
            elapsedTime += Time.deltaTime;
            obj.transform.position = Vector3.Lerp(startingPos, target, (elapsedTime / seconds));
            yield return new WaitForEndOfFrame();
        }

    }
}
