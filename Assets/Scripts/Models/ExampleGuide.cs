using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleGuide : MonoBehaviour {
    [TextArea]
    public string codeExample;

    [SerializeField]
    public GameObject CodeExamplePopup;

    public void displayExample() {
        GameObject popup = Instantiate(CodeExamplePopup);

        Utility.setupExamplePopup(popup, codeExample);
    }
}
