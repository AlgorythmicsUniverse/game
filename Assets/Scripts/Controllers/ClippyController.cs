using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClippyController : MonoBehaviour
{
    private List<GameObject> pickedUp;
    private Dictionary<GameObject, float> objectToRotAngle;
    private List<GameObject> nearbyObjects;
    private Dictionary<GameObject, GameObject> tooltipsForNearbyObjects;

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

    [SerializeField]
    public GameObject CodeblockTooltip;

    [SerializeField]
    public float tooltipAltitude = 1.0f;

    void Start() {
        pickedUp = new List<GameObject>();
        objectToRotAngle = new Dictionary<GameObject, float>();
        nearbyObjects = new List<GameObject>();
        tooltipsForNearbyObjects = new Dictionary<GameObject, GameObject>();
    }

    void Update() {}

    void FixedUpdate() {
        rotatePickedup();
        storeNearbyObjects();
        pointTooltipsTowardsPlayer();
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
            float y = center.y + characterHeight;
            float z = Mathf.Sin(theta) * orbitDistance + center.z;

            obj.transform.position = new Vector3(x, y, z);
        }
    }

    void pointTooltipsTowardsPlayer() {
        foreach (GameObject obj in tooltipsForNearbyObjects.Values) {
            var lookPos = obj.transform.position - transform.position;
            lookPos.y = 0;

            var rotation = Quaternion.LookRotation(lookPos);
            obj.transform.rotation = rotation;
        }
    }
    
    void storeNearbyObjects() {
        // Store nearby objects in local list to prevent multiple triggering
        List<GameObject> objects = new List<GameObject>(Utility.getNearbyObjectsWithTag(transform.position, "CodeBlock", minimumObjectDistance));
        foreach (GameObject obj in objects) {
            if (!pickedUp.Contains(obj) && !nearbyObjects.Contains(obj)) {
                nearbyObjects.Add(obj);

                // Create tooltip for object
                GameObject tooltip = Instantiate(CodeblockTooltip, obj.transform.position + new Vector3(0, tooltipAltitude, 0), Quaternion.identity);
                tooltip.transform.SetParent(obj.transform);
                tooltipsForNearbyObjects[obj] = tooltip;

                Utility.styleCodeblockTooltip(tooltip, obj);
            }
        }

        // Remove objects no longer nearby
        foreach (GameObject obj in nearbyObjects.ToArray()) {
            if (!objects.Contains(obj)) {
                nearbyObjects.Remove(obj);

                Destroy(tooltipsForNearbyObjects[obj]);
                tooltipsForNearbyObjects.Remove(obj);
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

        // Pick the object up
        pickedUp.Add(parentObj);

        // Disable floating
        parentObj.GetComponent<CodeObject>().floatingEnabled = false;

        // Remove from nearbyObjects list
        nearbyObjects.Remove(parentObj);

        // Remove tooltip for object
        Destroy(tooltipsForNearbyObjects[parentObj]);
        tooltipsForNearbyObjects.Remove(parentObj);

        // Initialize rotation for picked up object
        objectToRotAngle[parentObj] = 0;

        this.evenlyDividePickedup();

        // Disable the collider of this object's hitbox, since we have picked it up.
        Collider collider = obj.GetComponent<Collider>();
        collider.enabled = false;
    }
}
