using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Scripts2D.Functionalities;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance = null;

    public float SwitchDuration = 0.2f;
    public float TabMinScale = 0.2f;

    private Camera MainCamera;
    private Canvas GameUI;
    private Canvas ScreenUI;
    private GameObject Player;

    private bool Is3D = true;
    private bool lockSwitch = false;
    private static int currentPuzzle = 0; // -1 means no puzzles loaded yet
    private GameObject loadedPuzzle;
    private bool paused = false;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        // Manually reload when debugging single scene
        if (SceneManager.GetActiveScene().name != "MainMenu") {
            Reload();
        }
    }

    public void Reload() {
        MainCamera = new List<GameObject>(SceneManager.GetActiveScene().GetRootGameObjects()).Find(x => x.name == "MainCamera").GetComponent<Camera>();
        GameUI = new List<GameObject>(SceneManager.GetActiveScene().GetRootGameObjects()).Find(x => x.name == "GameUI").GetComponent<Canvas>();
        ScreenUI = new List<GameObject>(SceneManager.GetActiveScene().GetRootGameObjects()).Find(x => x.name == "ScreenUI").GetComponent<Canvas>();
        Player = new List<GameObject>(SceneManager.GetActiveScene().GetRootGameObjects()).Find(x => x.name == "PlayerClippy");

        StartCoroutine(to3D(false));
    }

    public bool GetIs3D() {
        return Is3D;
    }

    public bool GetPaused() {
        return paused;
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

    public IEnumerator to2D(bool animation = true) {
        lockSwitch = true;
        Is3D = false;

        MainCamera.GetComponent<Cinemachine.CinemachineBrain>().enabled = false;
        GameUI.enabled = false;
        ScreenUI.enabled = true;
        Transform screenGeometry = ScreenUI.transform.Find("Canvas").transform.Find("Geometry");

        if (animation) {
            screenGeometry.transform.localScale = Vector3.one*TabMinScale;
            yield return StartCoroutine(Utility.scaleOverSeconds(screenGeometry, Vector3.one, SwitchDuration));
        }

        MainCamera.orthographic = true;
        MainCamera.orthographicSize = 540;
        Quaternion rotation = Quaternion.Euler(Vector3.zero);
        MainCamera.transform.SetPositionAndRotation(Vector3.back, rotation);

        lockSwitch = false;
    }

    public IEnumerator to3D(bool animation = true) {
        lockSwitch = true;
        Is3D = true;

        MainCamera.GetComponent<Cinemachine.CinemachineBrain>().enabled = true;
        MainCamera.orthographic = false;
        Transform screenGeometry = ScreenUI.transform.Find("Canvas").transform.Find("Geometry");

        if (animation) {
            screenGeometry.transform.localScale = Vector3.one;
            yield return StartCoroutine(Utility.scaleOverSeconds(screenGeometry, Vector3.one*TabMinScale, SwitchDuration));
        }

        GameUI.enabled = true;
        ScreenUI.enabled = false;
        
        lockSwitch = false;
    }

    public void pauseGame() {
        MainCamera.GetComponent<Cinemachine.CinemachineBrain>().enabled = false;
        Time.timeScale = 0;
		Cursor.lockState = CursorLockMode.None;
    }

    public void unpauseGame() {
        MainCamera.GetComponent<Cinemachine.CinemachineBrain>().enabled = true;
        Time.timeScale = 1;
		Cursor.lockState = CursorLockMode.Locked;
    }

    public void showMenu() {
        GameObject menu = GameUI.transform.Find("Menu").gameObject;
        menu.SetActive(true);

        paused = true;
        pauseGame();
    }

    public void hideMenu() {
        GameObject menu = GameUI.transform.Find("Menu").gameObject;
        menu.SetActive(false);

        paused = false;
        unpauseGame();
    }

    public void nextPuzzle() {
        // If last puzzle
        if (Constants.Puzzles[SceneManager.GetActiveScene().name].Count == (currentPuzzle + 1)) {
            string nextChapter = Constants.Chapters.Find(x => x.SceneName == SceneManager.GetActiveScene().name).UnlocksChapter;

            // Not last chapter
            if (nextChapter != "") {
                // Unlock next chapter
                LevelController.Instance.unlockChapter(nextChapter);
                Debug.Log("Unlocked next chapter: " + nextChapter);

                // Load next chapter
                LevelController.Instance.loadChapter(nextChapter);
                currentPuzzle = 0;
            }
        } else {
            // Unload current puzzle
            unloadPuzzle();

            currentPuzzle++;
            // Load next puzzle
            loadPuzzle(Constants.Puzzles[SceneManager.GetActiveScene().name][currentPuzzle].PrefabName);
        }
    }

    public void unloadPuzzle()
    {
        var objectToDelete = GameObject.Find("Canvas");
        if (objectToDelete != null)
        {
            Destroy(objectToDelete); // Az objektum megsemmisítése
        }
        else
        {
            Debug.LogError("Az objektum nem található: ScreenUI");
        }
    }

    public void loadPuzzle(string puzzlePath) {
        // Load puzzle
        var levels = GameObject.Find("ScreenUI").GetComponent<LevelsList>();
        Debug.Log("PREFAB: " + puzzlePath);
        GameObject prefab = levels.GetPrefabByName(puzzlePath);
        
        if (prefab != null)
        {
            GameObject newObject = Instantiate(prefab, Vector3.zero, Quaternion.identity); // Prefab alapján új elem létrehozása
            newObject.name = "Canvas"; // Elem átnevezése
            newObject.transform.SetParent(GameObject.Find("ScreenUI").transform);
        }
        else
        {
            Debug.Log("Couldn't load puzzle: " + puzzlePath);
        }
        
        
    }

    public static string GetCurrentLevel() {
        return Constants.Puzzles[SceneManager.GetActiveScene().name][currentPuzzle].PrefabName;
    }
}
