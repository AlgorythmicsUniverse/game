using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance = null;

    public float SwitchDuration = 0.2f;
    public float TabMinScale = 0.2f;

    public Camera MainCamera;
    public Canvas GameUI;
    public Canvas ScreenUI;
    private bool Is3D = true;
    private bool lockSwitch = false;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        // Initialize camera
        GameUI.enabled = true;
        ScreenUI.enabled = false;
    }

    public bool GetIs3D() {
        return Is3D;
    }

    public void switchMode() {
        if (lockSwitch) {
            return;
        }

        if (Is3D) {
            StartCoroutine(to2D());
        } else {
            StartCoroutine(to3D());
        }
    }

    public IEnumerator to2D() {
        lockSwitch = true;
        Is3D = false;

        MainCamera.GetComponent<Cinemachine.CinemachineBrain>().enabled = false;
        GameUI.enabled = false;
        ScreenUI.enabled = true;
        Transform screenGeometry = ScreenUI.transform.Find("Geometry");
        screenGeometry.transform.localScale = Vector3.one*TabMinScale;

        yield return StartCoroutine(Utility.scaleOverSeconds(screenGeometry, Vector3.one, SwitchDuration));

        MainCamera.orthographic = true;
        Quaternion rotation = Quaternion.Euler(Vector3.zero);
        MainCamera.transform.SetPositionAndRotation(Vector3.back, rotation);

        lockSwitch = false;
    }

    public IEnumerator to3D() {
        lockSwitch = true;
        Is3D = true;

        MainCamera.GetComponent<Cinemachine.CinemachineBrain>().enabled = true;
        MainCamera.orthographic = false;
        Transform screenGeometry = ScreenUI.transform.Find("Geometry");
        screenGeometry.transform.localScale = Vector3.one;

        yield return StartCoroutine(Utility.scaleOverSeconds(screenGeometry, Vector3.one*TabMinScale, SwitchDuration));

        GameUI.enabled = true;
        ScreenUI.enabled = false;
        
        lockSwitch = false;
    }
}
