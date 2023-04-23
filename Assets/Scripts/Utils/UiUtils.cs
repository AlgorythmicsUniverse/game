using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public static class UiUtils {

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
            case CodeObjectType.ControlFlow:
                panelBackgroundColor = new Color32(255, 160, 61, 255);
                topPanelBackgroundColor = new Color32(255, 144, 28, 255);
                break;
            case CodeObjectType.DataType:
                panelBackgroundColor = new Color32(76, 191, 254, 255);
                topPanelBackgroundColor = new Color32(8, 158, 255, 255);
                break;
            case CodeObjectType.Expression:
                panelBackgroundColor = new Color32(27, 255, 130, 255);
                topPanelBackgroundColor = new Color32(7, 217, 102, 255);
                break;
            case CodeObjectType.Statement:
                panelBackgroundColor = new Color32(76, 191, 254, 255);
                topPanelBackgroundColor = new Color32(8, 158, 255, 255);
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
}
