using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClippyController : MonoBehaviour
{
    // The list of currently picked up game objects.
    private List<GameObject> pickedUp;

    private Dictionary<GameObject, float> objectToRotAngle;

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

    void Start() {
        // Instantiate an empty list of picked up items.
        pickedUp = new List<GameObject>();
        objectToRotAngle = new Dictionary<GameObject, float>();
    }

    void Update() {
        // Update each picked up game object.
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

    void OnTriggerEnter(Collider other) {
        GameObject obj = other.gameObject;

        if (pickedUp.Contains(obj)) {
        GameObject parentObj = obj.transform.parent.gameObject;

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

        // Initialize rotation for picked up object
        objectToRotAngle[parentObj] = 0;

        // Disable the collider of this object's hitbox, since we have picked it up.
        Collider collider = obj.GetComponent<Collider>();
        collider.enabled = false;
    }
}
