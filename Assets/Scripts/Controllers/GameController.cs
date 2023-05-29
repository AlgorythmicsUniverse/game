using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance = null;

    public Camera MainCamera;
    private bool Is3D = true;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public bool GetIs3D() {
        return Is3D;
    }

    public void switchMode() {
        if (Is3D) {
            to2D();
        } else {
            to3D();
        }
    }

    public void to2D() {
        Is3D = false;

        MainCamera.orthographic = true;
        MainCamera.GetComponent<Cinemachine.CinemachineBrain>().enabled = false;

        Quaternion rotation = Quaternion.Euler(Vector3.zero);

        MainCamera.transform.SetPositionAndRotation(Vector3.back, rotation);
    }

    public void to3D() {
        Is3D = true;

        MainCamera.orthographic = false;
        MainCamera.GetComponent<Cinemachine.CinemachineBrain>().enabled = true;
    }
}
