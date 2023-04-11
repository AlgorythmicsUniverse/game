using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public static class Utility {
    public static GameObject[] getNearbyObjectsWithTag(Vector3 source, string tag, float minimumDistance) {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);

        List<GameObject> nearbyObjects = new List<GameObject>();
        foreach (GameObject obj in objects) {
            if (Vector3.Distance(source, obj.transform.position) <= minimumDistance) {
                nearbyObjects.Add(obj);
            }
        }

        return nearbyObjects.ToArray();
    }

    public static void styleCodeblockTooltip(GameObject tooltip, GameObject obj) {
        CodeObject codeBlock = obj.GetComponent<CodeObject>();

        GameObject itemNameText = tooltip.transform.Find("Panel/ItemNameText").gameObject;
        itemNameText.GetComponent<TMP_Text>().text = codeBlock.Name;
        
        GameObject itemDescriptionText = tooltip.transform.Find("Panel/ItemDescriptionText").gameObject;
        string descriptionText = codeBlock.Description;
        if (descriptionText.Length == 0) {
            descriptionText = "No description";
        }
        itemDescriptionText.GetComponent<TMP_Text>().text = descriptionText;

        Color32 panelBackgroundColor = new Color32(125, 125, 125, 255);
        Color32 topPanelBackgroundColor = new Color32(93, 93, 93, 255);
        switch (codeBlock.Type) {
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
    }
}
