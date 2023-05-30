using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public static class Utility {
    public static GameObject[] getNearbyObjectsWithTag(Vector3 center, string tag, float minimumDistance) {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);

        List<GameObject> nearbyObjects = new List<GameObject>();
        foreach (GameObject obj in objects) {
            if (Vector3.Distance(center, obj.transform.position) <= minimumDistance) {
                nearbyObjects.Add(obj);
            }
        }

        return nearbyObjects.ToArray();
    }

    public static GameObject[] getNearbyInteractables(Vector3 center, float minimumDistance) {
        Interactable[] interactables = GameObject.FindObjectsOfType<Interactable>();

        List<GameObject> objects = new List<GameObject>();
        foreach (Interactable interactable in interactables) {
            if (interactable.Enabled) {
                GameObject obj = interactable.gameObject;
                if (Vector3.Distance(center, obj.transform.position) <= minimumDistance) {
                    objects.Add(obj);
                }
            }
        }

        return objects.ToArray();
    }

    public static GameObject getClosestObject(Vector3 center, GameObject[] objects) {
        GameObject closest = null;
        if (objects.Length > 0) {
            closest = objects[0];
        }
        foreach (GameObject obj in objects) {
            if (Vector3.Distance(center, obj.transform.position) < Vector3.Distance(center, closest.transform.position)) {
                closest = obj;
            }
        }

        return closest;
    }

    public static void setupCodeObjectTooltip(GameObject tooltip, GameObject obj) {
        CodeObject codeObject = obj.GetComponent<CodeObject>();

        GameObject itemNameText = tooltip.transform.Find("Panel/ItemNameText").gameObject;
        itemNameText.GetComponent<TMP_Text>().text = codeObject.Name;
        
        GameObject itemDescriptionText = tooltip.transform.Find("Panel/ItemDescriptionText").gameObject;
        string descriptionText = codeObject.Description;
        if (descriptionText.Length == 0) {
            descriptionText = "No description";
        }
        itemDescriptionText.GetComponent<TMP_Text>().text = descriptionText;

        Color32 panelBackgroundColor = new Color32(125, 125, 125, 255);
        Color32 topPanelBackgroundColor = new Color32(93, 93, 93, 255);
        switch (codeObject.Type) {
            case CodeObjectType.Declaration:
                panelBackgroundColor = new Color32(76, 191, 254, 255);
                topPanelBackgroundColor = new Color32(8, 158, 255, 255);
                break;
            case CodeObjectType.ControlFlow:
                panelBackgroundColor = new Color32(255, 160, 61, 255);
                topPanelBackgroundColor = new Color32(255, 144, 28, 255);
                break;
            case CodeObjectType.Expression:
                panelBackgroundColor = new Color32(27, 255, 130, 255);
                topPanelBackgroundColor = new Color32(7, 217, 102, 255);
                break;
            default:
            break;
        }

        GameObject panel = tooltip.transform.Find("Panel").gameObject;
        panel.GetComponent<Image>().color = panelBackgroundColor;

        GameObject topPanel = tooltip.transform.Find("Panel/ItemNamePanel").gameObject;
        topPanel.GetComponent<Image>().color = topPanelBackgroundColor;

        AudioGuide audioGuide = obj.GetComponent<AudioGuide>();
        GameObject playAudioButton = tooltip.transform.Find("Panel/PlayAudioButton").gameObject;
        if (audioGuide != null) {
            playAudioButton.SetActive(true);
            playAudioButton.GetComponent<Button>().onClick.AddListener(
                delegate {
                    audioGuide.playAudio();
                }
            );
        } else {
            playAudioButton.SetActive(false);
        }
        
        ExampleGuide exampleGuide = obj.GetComponent<ExampleGuide>();
        GameObject displayExampleButton = tooltip.transform.Find("Panel/DisplayExampleButton").gameObject;
        if (exampleGuide != null) {
            displayExampleButton.SetActive(true);
            displayExampleButton.GetComponent<Button>().onClick.AddListener(
                delegate {
                    exampleGuide.displayExample();
                }
            );
        } else {
            displayExampleButton.SetActive(false);
        }
    }

    public static void setupExamplePopup(GameObject popup, string code) {
        GameObject codeText = popup.transform.Find("Panel/CodeArea/CodeText").gameObject;
        codeText.GetComponent<TMP_Text>().SetText(code);
    }
    
    public static void setupInteractableTooltip(GameObject tooltip, string key) {
        Texture2D texture = Resources.Load<Texture2D>("KeyTextures/" + key);
        

        GameObject keyImage = tooltip.transform.Find("KeyImage").gameObject;
        keyImage.GetComponent<RawImage>().texture = texture;
    }

    public static void pointObjectsTowardsTarget(Vector3 target, GameObject[] objects) {
        foreach (GameObject obj in objects) {
            var lookPos = obj.transform.position - target;
            lookPos.y = 0;

            var rotation = Quaternion.LookRotation(lookPos);
            obj.transform.rotation = rotation;
        }
    }

    public static void pointObjectsTowardsCamera(GameObject[] objects) {
        Vector3 target = Camera.main.transform.position;
        pointObjectsTowardsTarget(target, objects);
    }

    public static IEnumerator moveOverSpeed(Transform transform, Vector3 target, float speed, float delay=0) {
        yield return new WaitForSeconds(delay);

        while (transform.position != target) {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    public static IEnumerator moveOverSeconds(Transform transform, Vector3 target, float seconds, float delay=0) {
        yield return new WaitForSeconds(delay);
        
        float elapsedTime = 0;
        Vector3 startingPos = transform.position;
        while (elapsedTime < seconds) {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startingPos, target, (elapsedTime / seconds));
            yield return new WaitForEndOfFrame();
        }

    }
}
