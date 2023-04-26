using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExistingVariable : MonoBehaviour,IPointerDownHandler
{
    [SerializeField] private GameObject variablePrefab;
    [SerializeField] private UI_InputWindow inputWindow;
    [SerializeField] public NewVariable newVariable;
    [SerializeField] private UI_ErrorPopup errorPopup;

    public void OnPointerDown(PointerEventData eventData)
    {
        UI_Blocker.Show_Static();
        inputWindow.Show(
            "abcdefghijklmnopqrstuvwxyz",
            5,
            () =>
            {
                Debug.Log("Cancel");
                UI_Blocker.Hide_Static();
            },
            (string inputText) =>
            {
                if (newVariable.isExistingVariableName(inputText))
                {
                    Debug.Log("OK!" );
                    CreateVariableItem(inputText);
                    UI_Blocker.Hide_Static();
                }
                else
                {
                    errorPopup.Show("No variable found with given name!");
                    Debug.Log("No such variable!");
                }
            }
        );
    }

    private void CreateVariableItem(string variableName)
    {
        foreach (KeyValuePair<string, string> pair in newVariable.usedVariableNames)
        {
            if (variableName == pair.Key)
            {
                GameObject newObject = Instantiate(variablePrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity, transform.parent);
                newObject.transform.Find("Name").GetComponent<TMP_Text>().text = variableName;
            }
        }
    }
}
