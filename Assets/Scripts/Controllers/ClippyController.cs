using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClippyController : MonoBehaviour
{
    // The list of currently picked up game objects.
    private List<GameObject> pickedUp;
    private Dictionary<GameObject, float> objectToRotAngle;
    private List<GameObject> nearbyObjects;

    // How many code objects can Clippy pick up?
    [SerializeField]
    public int maximumObjectPickUp = 3;

    [SerializeField]
    public float orbitDistance = 1.5f;

    [SerializeField]
    public float orbitDegreesPerSec = 90.0f;

    [SerializeField]
    public float bobUpAndDownMultiplier = 0.25f;

    [SerializeField]
    public float characterHeight = 1.0f;

    [SerializeField]
    public float minimumObjectDistance = 5.0f;

    void Start() {
        // Instantiate an empty list of picked up items.
        pickedUp = new List<GameObject>();
        objectToRotAngle = new Dictionary<GameObject, float>();
        nearbyObjects = new List<GameObject>();
    }

    void Update() {}

    void FixedUpdate() {
        rotatePickedup();
        storeNearbyObjects();
    }

    void OnTriggerEnter(Collider other) {
        handleCodeblockTrigger(other);
    }

    void rotatePickedup() {
        // Rotate picked up items around Clippy
        foreach (GameObject obj in pickedUp) {
            float actualTheta = objectToRotAngle[obj] + (orbitDegreesPerSec * Time.deltaTime);
            objectToRotAngle[obj] = actualTheta;

            float theta = actualTheta * Mathf.Deg2Rad;

            Vector3 center = transform.position;
            float x = Mathf.Cos(theta) * orbitDistance + center.x;
            float y = center.y + characterHeight + (Mathf.Sin(theta) * bobUpAndDownMultiplier);
            float z = Mathf.Sin(theta) * orbitDistance + center.z;

            obj.transform.position = new Vector3(x, y, z);
        }
    }
    
    GameObject[] getNearbyObjectsWithTag(string tag, float minimumDistance) {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);

        List<GameObject> nearbyObjects = new List<GameObject>();
        foreach (GameObject obj in objects) {
            if (Vector3.Distance(transform.position, obj.transform.position) <= minimumDistance) {
                nearbyObjects.Add(obj);
            }
        }

        return nearbyObjects.ToArray();
    }
    
    void storeNearbyObjects() {
        // Store nearby objects in local list to prevent multiple triggering
        List<GameObject> objects = new List<GameObject>(getNearbyObjectsWithTag("CodeBlock", minimumObjectDistance));
        foreach (GameObject obj in objects) {
            if (!pickedUp.Contains(obj) && !nearbyObjects.Contains(obj)) {
                nearbyObjects.Add(obj);
            }
        }

        // Remove objects no longer nearby
        foreach (GameObject obj in nearbyObjects.ToArray()) {
            if (!objects.Contains(obj)) {
                nearbyObjects.Remove(obj);
            }
        }
    }

    void evenlyDividePickedup() {
        float step = 360f / pickedUp.Count;
        int i = 0;
        foreach (GameObject obj in pickedUp) {
            objectToRotAngle[obj] = step * i;
            i++;
        }
    }

    void handleCodeblockTrigger(Collider other) {
        GameObject obj = other.gameObject;

        if (!obj.CompareTag("Hitbox")) {
            // Is not a hitbox
            return;
        }

        GameObject parentObj = obj.transform.parent.gameObject;

        if (!parentObj.CompareTag("CodeBlock")) {
            // Is not a CodeBlock
            return;
        }

        if (pickedUp.Contains(parentObj)) {
            // This object has already been picked up.
            return;
        }

        if (pickedUp.Count >= maximumObjectPickUp) {
            // We cannot pick up more objects.
            return;
        }

        // Pick the object up.
        pickedUp.Add(parentObj);

        // Remove from nearbyObjects list
        nearbyObjects.Remove(parentObj);

        // Initialize rotation for picked up object
        objectToRotAngle[parentObj] = 0;

        this.evenlyDividePickedup();

        // Disable the collider of this object's hitbox, since we have picked it up.
        Collider collider = obj.GetComponent<Collider>();
        collider.enabled = false;
    }
}
