using System;
using System.Collections;
using System.Collections.Generic;
using Scripts2D.Scene2Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class NewValueElement : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private UI_InputWindow inputWindow;
    [SerializeField] private UI_DropdownVariableSelector dropdownVariableSelector;
    [SerializeField] private UI_ErrorPopup errorPopup;
    List<string> dropdownItems = new List<string>();
    
    private void Awake()
    {
        foreach (var type in NewVariable.GetUnlockedTypes())
        {
            if (!dropdownItems.Contains(type))
            {
                dropdownItems.Add(type);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UI_Blocker.Show_Static();
        dropdownVariableSelector.Show(
            dropdownItems,
            () =>
            {
                Debug.Log("Cancel!");
                UI_Blocker.Hide_Static();
            },
            (string variableType) =>
            {
                CreateNewValueElement(variableType);
            }
            );
    }

    private void CreateNewValueElement(string variableType)
    {
        GameObject newGameObject;
        string validCharacters = "";
        string inputText = "";
        int charLimit = 5;
        switch (variableType)
        {
            case "INT":
                validCharacters = "0123456789";
                break;
            case "DOUBLE":
                validCharacters = "0123456789.";
                break;
            case "FLOAT":
                validCharacters = "0123456789.";
                break;
            case "STRING":
                validCharacters = "qwertyuiopasdfghjklzxcvbnm0123456789.!?";
                break;
            case "BOOLEAN":
                validCharacters = "01";
                break;
        }
        inputWindow.Show(validCharacters,
            charLimit,
            () =>
            {
                Debug.Log("Cancel!");
                UI_Blocker.Hide_Static();
            },
            ( inputTextTemp) =>
            {
                if (variableType == "BOOLEAN")
                {
                    switch (inputTextTemp)
                    {
                        case "0":
                            inputText = "false";
                            break;
                        case "1":
                            inputText = "true";
                            break;
                    }

                    charLimit = 1;
                }
                else
                {
                    inputText = inputTextTemp;
                }
                Debug.Log("OK! input= " + inputText);
                if (inputText == "")
                {
                    errorPopup.Show("No value given, element could not've been created!");
                }
                else
                {
                    newGameObject = Instantiate(prefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity, transform.parent);
                    newGameObject.transform.Find("Value").GetComponent<TMP_Text>().text = inputText;
                    newGameObject.transform.Find("Type").GetComponent<TMP_Text>().text = variableType;
                    UI_Blocker.Hide_Static();
                }
            }
        );
    }
}
