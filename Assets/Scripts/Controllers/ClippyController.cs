using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ClippyController : MonoBehaviour
{
    private List<GameObject> pickedUp;
    private Dictionary<GameObject, float> objectToRotAngle;
    private List<GameObject> nearbyObjects;
    private Dictionary<GameObject, GameObject> tooltipsForNearbyObjects;
    private List<GameObject> nearbyInteractables;
    private Dictionary<GameObject, GameObject> tooltipsForInteractableObjects;

    [SerializeField]
    public int maximumObjectPickUp = 3;

    [SerializeField]
    public float orbitDistance = 1.5f;

    [SerializeField]
    public float orbitDegreesPerSec = 90.0f;

    [SerializeField]
    public float orbitHeight = 1.0f;

    [SerializeField]
    public float minimumObjectDistance = 5.0f;

    [SerializeField]
    public GameObject GameUI;

    [SerializeField]
    public GameObject CodeObjectTooltip;

    [SerializeField]
    public GameObject InteractableTooltip;

    [SerializeField]
    public float tooltipAltitude = 1.0f;

    public float fallingHeight = 0.0f;

    private Vector3 lastValidPosition;
    private Quaternion lastValidRotation;

    void Start() {
        pickedUp = new List<GameObject>();
        objectToRotAngle = new Dictionary<GameObject, float>();
        nearbyObjects = new List<GameObject>();
        tooltipsForNearbyObjects = new Dictionary<GameObject, GameObject>();
        nearbyInteractables = new List<GameObject>();
        tooltipsForInteractableObjects = new Dictionary<GameObject, GameObject>();
    }

    void Update() {
        updateUi();
    }

    void FixedUpdate() {
        handleFallingOff();

        rotatePickedup();
        storeNearbyObjects();
        Utility.pointObjectsTowardsPlayer(transform.position, tooltipsForNearbyObjects.Values.ToArray());
        storeNearbyInteractableObjects();
        Utility.pointObjectsTowardsPlayer(transform.position, tooltipsForInteractableObjects.Values.ToArray());
    }

    void OnTriggerEnter(Collider other) {
        handleCodeObjectTrigger(other);
    }

    void OnInteract(InputValue value) {
        if (nearbyInteractables.Count > 0) {
            GameObject interactable = Utility.getClosestObject(transform.position, nearbyInteractables.ToArray());

            if (interactable.tag == "Extraction") {
                extractPickedup(interactable.transform.Find("ExtractPoint").position);
            }
        }
    }

    void handleFallingOff() {
        if (transform.GetComponent<StarterAssets.ThirdPersonController>().Grounded) {
            lastValidPosition = transform.position;
            lastValidRotation = transform.rotation;
        }

        if (isPlayerFallingOff()) {
            // If player is falling out of world, teleport back to last valid position
            Vector3 rot = lastValidRotation.eulerAngles;
            rot = new Vector3(rot.x, rot.y + 180, rot.z);
            Quaternion rotation = Quaternion.Euler(rot);

            transform.SetPositionAndRotation(lastValidPosition, rotation);

            transform.position += transform.forward * 1;
        }
    }

    bool isPlayerFallingOff() {
        return transform.position.y < fallingHeight;
    }

    void extractPickedup(Vector3 target) {
        float delay = 0;
        foreach (GameObject obj in pickedUp.ToArray()) {
            StartCoroutine(extractCodeObject(obj, target, delay));
            delay += 1f;
        }
    }

    IEnumerator extractCodeObject(GameObject obj, Vector3 target, float delay) {
        yield return new WaitForSeconds(delay);

        GameObject temp = obj;
        pickedUp.Remove(obj);

        temp.GetComponent<CodeObject>().disabled = true;
        yield return StartCoroutine(Utility.moveOverSeconds(obj, target, 1f));
        Destroy(temp);
    }
    
    void updateUi() {
        GameObject itemNameText = GameUI.transform.Find("PickedupInfo").gameObject;
        itemNameText.GetComponent<TMP_Text>().text = string.Format("{0}/{1} picked up", pickedUp.Count, maximumObjectPickUp);
    }

    void rotatePickedup() {
        // Rotate picked up items around Clippy
        foreach (GameObject obj in pickedUp) {
            float actualTheta = objectToRotAngle[obj] + (orbitDegreesPerSec * Time.deltaTime);
            objectToRotAngle[obj] = actualTheta;

            float theta = actualTheta * Mathf.Deg2Rad;

            Vector3 center = transform.position;
            float x = Mathf.Cos(theta) * orbitDistance + center.x;
            float y = center.y + orbitHeight;
            float z = Mathf.Sin(theta) * orbitDistance + center.z;

            obj.transform.position = new Vector3(x, y, z);
        }
    }
    
    void storeNearbyObjects() {
        // Store nearby objects in local list to prevent multiple triggering
        List<GameObject> objects = new List<GameObject>(Utility.getNearbyObjectsWithTag(transform.position, "CodeObject", minimumObjectDistance));
        foreach (GameObject obj in objects) {
            if (!obj.GetComponent<CodeObject>().disabled && !pickedUp.Contains(obj) && !nearbyObjects.Contains(obj)) {
                nearbyObjects.Add(obj);

                // Create tooltip for object
                GameObject tooltip = Instantiate(CodeObjectTooltip, obj.transform.position + new Vector3(0, tooltipAltitude, 0), Quaternion.identity);
                tooltip.transform.SetParent(obj.transform);
                tooltipsForNearbyObjects[obj] = tooltip;

                Utility.styleCodeObjectTooltip(tooltip, obj);
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

    void storeNearbyInteractableObjects() {
        // Store nearby interactables in local list to prevent multiple triggering
        List<GameObject> objects = new List<GameObject>(Utility.getNearbyInteractables(transform.position, minimumObjectDistance));
        foreach (GameObject obj in objects) {
            if (!nearbyInteractables.Contains(obj)) {
                nearbyInteractables.Add(obj);

                // Create tooltip for interactable
                GameObject tooltip = Instantiate(InteractableTooltip, obj.transform.position + new Vector3(0, tooltipAltitude, 0), Quaternion.identity);
                tooltip.transform.SetParent(obj.transform);
                tooltipsForInteractableObjects[obj] = tooltip;

                string key = obj.GetComponent<Interactable>().Key;

                Utility.styleInteractableTooltip(tooltip, key);
            }
        }

        // Remove objects no longer nearby
        foreach (GameObject obj in nearbyInteractables.ToArray()) {
            if (!objects.Contains(obj)) {
                nearbyInteractables.Remove(obj);

                Destroy(tooltipsForInteractableObjects[obj]);
                tooltipsForInteractableObjects.Remove(obj);
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

    void handleCodeObjectTrigger(Collider other) {
        GameObject obj = other.gameObject;

        if (!obj.CompareTag("Hitbox")) {
            // Is not a hitbox
            return;
        }

        GameObject parentObj = obj.transform.parent.gameObject;

        if (!parentObj.CompareTag("CodeObject")) {
            // Is not a CodeObject
            return;
        }

        if (parentObj.GetComponent<CodeObject>().disabled) {
            // CodeObject is disabled
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
