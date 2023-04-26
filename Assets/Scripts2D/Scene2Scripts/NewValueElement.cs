using System.Collections;
using System.Collections.Generic;
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

    public void OnPointerDown(PointerEventData eventData)
    {
        UI_Blocker.Show_Static();
        dropdownVariableSelector.Show(
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
        GameObject newGameObject = null;
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
            case "BOOL":
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
                Debug.Log("OK! + " + inputText);
                if (inputText == "")
                {
                    errorPopup.Show("No value given, element could not been created!");
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
