using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using TMPro;

public class UI_InputWindowVariable : MonoBehaviour
{

    private Button_UI OKButton;
    private Button_UI CancelButton;
    private TMP_InputField inputField;
    private TMP_Dropdown variableDropdown;
    private string variableType;

    private void Awake()
    {
        OKButton = transform.Find("OKButton").GetComponent<Button_UI>();
        CancelButton = transform.Find("CancelButton").GetComponent<Button_UI>();
        inputField = transform.Find("InputField").GetComponent<TMP_InputField>();
        variableDropdown = transform.Find("Dropdown").GetComponent<TMP_Dropdown>();
        
        List<string> dropdownItems = new List<string>();
        dropdownItems.Add("INT");
        dropdownItems.Add("DOUBLE");
        dropdownItems.Add("FLOAT");
        dropdownItems.Add("STRING");
        dropdownItems.Add("BOOLEAN");

        foreach (var item in dropdownItems )
        {
            variableDropdown.options.Add(new TMP_Dropdown.OptionData() { text = item });
        }

        DropdownItemSelected(variableDropdown);
        
        variableDropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(variableDropdown); });
        
        Hide();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            OKButton.ClickFunc();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CancelButton.ClickFunc();
        }
    }

    public void Show(string validCharacters,int characterLimit,Action onCancel,Action<string,string> onOk)
    {
        gameObject.SetActive(true);
        transform.SetAsLastSibling();

        inputField.characterLimit = characterLimit;
        inputField.onValidateInput = (string text, int charIndex, char addedChar) =>
        {
            return ValidateChar(validCharacters, addedChar);
        };
        


        OKButton.ClickFunc = () =>
        {
            Hide();
            onOk(inputField.text,variableType);
        };

        CancelButton.ClickFunc = () =>
        {
            Hide();
            onCancel();
        };
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private char ValidateChar(string validCharacters,char addedChar)
    {
        if(validCharacters.IndexOf(addedChar) != -1)
        {
            return addedChar;
        }
        else
        {
            return '\0';
        }
    }

    private void DropdownItemSelected(TMP_Dropdown dropdown)
    {
        int index = dropdown.value;
        Debug.Log(dropdown.options[index].text);
        variableType = dropdown.options[index].text;
    }

}
