using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeObject : MonoBehaviour
{
    public string Name;
    public CodeObjectType Type;
    public string Description;

    public bool floatingEnabled = true;
    public float floatFrequency = 1f;
    public float floatAmplitude = 0.20f;
    public GameObject Object;

    private float tempY;
    private float randOffset;

    void Start() {
        tempY = Object.transform.position.y;
        randOffset = Random.Range(0, 180);
    }

    void Update() {
        if (floatingEnabled) {
            floatObject();
        }
    }

    void floatObject() {
        Vector3 tempPos = Object.transform.position;
        tempPos.y = tempY + Mathf.Sin(Time.fixedTime * floatFrequency + randOffset) * floatAmplitude;

        Object.transform.position = tempPos;
    }
}
