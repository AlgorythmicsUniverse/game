using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClippyController : MonoBehaviour
{
    // The list of currently picked up game objects.
    private List<GameObject> pickedUp;

    // How many code objects can Clippy pick up?
    [SerializeField]
    private int maximumObjectPickUp = 3;

    void Start() {
        // Instantiate an empty list of picked up items.
        pickedUp = new List<GameObject>();
    }

    void Update() {
        // Update each picked up game object.
        foreach (GameObject obj in pickedUp) {
            // TODO
            obj.transform.RotateAround(transform.position, Vector3.forward, 20 * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other) {
        GameObject obj = other.gameObject;

        if (pickedUp.Contains(obj)) {
            // This object has already been picked up.
            return;
        }

        if (pickedUp.Count >= maximumObjectPickUp) {
            // We cannot pick up more objects.
            Debug.Log("Picked up too many objects");
            return;
        }

        Debug.Log("Picked up: " + obj.name);

        // Pick the object up.
        pickedUp.Add(obj);

        // Disable the collider of this object, since we have picked it up.
        Collider collider = obj.GetComponent<Collider>();
        collider.enabled = false;
    }
}
