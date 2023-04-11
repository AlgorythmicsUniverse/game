using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeBlock : MonoBehaviour
{
    public string Name;
    public CodeBlockTypes Type;
    public string Description;

    public bool floatingEnabled = true;
    public float floatFrequency = 1f;
    public float floatAmplitude = 0.20f;
    public GameObject Object;

    private float tempY;

    void Start() {
        tempY = Object.transform.position.y;
    }

    void Update() {
        if (floatingEnabled) {
            floatObject();
        }
    }

    void floatObject() {
        Vector3 tempPos = Object.transform.position;
        tempPos.y = tempY + Mathf.Sin(Time.fixedTime * floatFrequency) * floatAmplitude;

        Object.transform.position = tempPos;
    }
}
