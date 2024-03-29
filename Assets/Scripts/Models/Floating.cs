using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    public bool disabled = false;

    public bool floatingEnabled = true;
    public float floatFrequency = 1f;
    public float floatAmplitude = 0.20f;
    public GameObject Object;

    public bool rotatingEnabled = true;
    public float rotationSpeed = 30.0f;

    private float randOffset;

    
    void Start() {
        randOffset = Random.Range(0, 180);
    }

    void Update() {
        if (!disabled) {
            if (floatingEnabled) {
                floatObject();
            }
            if (rotatingEnabled) {
                rotateObject();
            }
        }
    }

    void floatObject() {
        Vector3 tempPos = Object.transform.position;
        float parentY = Object.transform.parent.position.y;
        tempPos.y = parentY + Mathf.Sin(Time.fixedTime * floatFrequency + randOffset) * floatAmplitude;

        Object.transform.position = tempPos;
    }
    
    void rotateObject() {
        Object.transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime));
    }
}
