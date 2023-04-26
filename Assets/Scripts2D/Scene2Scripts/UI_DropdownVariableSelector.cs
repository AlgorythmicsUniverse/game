using System;
using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using TMPro;
using UnityEngine;

public class UI_DropdownVariableSelector : MonoBehaviour
{
    private Button_UI OKButton;
    private Button_UI CancelButton;
    private TMP_Dropdown variableDropdown;
    private string variableType;
    
    private void Awake()
    {
        OKButton = transform.Find("OKButton").GetComponent<Button_UI>();
        CancelButton = transform.Find("CancelButton").GetComponent<Button_UI>();
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
    
    public void Show(Action onCancel,Action<string> onOk)
    {
        gameObject.SetActive(true);
        transform.SetAsLastSibling();
        OKButton.ClickFunc = () =>
        {
            Hide();
            onOk(variableType);
        };
        CancelButton.ClickFunc = () =>
        {
            Hide();
            onCancel();
        };
    }
    

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void DropdownItemSelected(TMP_Dropdown dropdown)
    {
        int index = dropdown.value;
        Debug.Log(dropdown.options[index].text);
        variableType = dropdown.options[index].text;
    }
}
