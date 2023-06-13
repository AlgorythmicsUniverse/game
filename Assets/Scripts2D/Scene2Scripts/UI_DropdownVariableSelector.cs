using System;
using System.Collections.Generic;
using CodeMonkey.Utils;
using TMPro;
using UnityEngine;

namespace Scripts2D.Scene2Scripts
{
    public class UI_DropdownVariableSelector : MonoBehaviour
    {
        private Button_UI OKButton;
        private Button_UI CancelButton;
        private TMP_Dropdown variableDropdown;
        private string variableType;
        private List<string> dropdownItems = new();
        [SerializeField] private UI_ErrorPopup errorPopup;

        private void Awake()
        {
            OKButton = transform.Find("OKButton").GetComponent<Button_UI>();
            CancelButton = transform.Find("CancelButton").GetComponent<Button_UI>();
            variableDropdown = transform.Find("Dropdown").GetComponent<TMP_Dropdown>();
            variableDropdown.options.Clear();
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
    
        public void Show(List<string> items,Action onCancel,Action<string> onOk)
        {
            variableDropdown.ClearOptions();
            if (items.Count > 0)
            {
                dropdownItems = items;
                foreach (var item in dropdownItems )
                {
                    variableDropdown.options.Add(new TMP_Dropdown.OptionData() { text = item });
                }

                variableDropdown.value = 1;
                DropdownItemSelected(variableDropdown);
        
                variableDropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(variableDropdown); });
                gameObject.SetActive(true);
                transform.SetAsLastSibling();
                OKButton.ClickFunc = () =>
                {
                    Hide();
                    onOk(variableType);
                    UI_Blocker.Hide_Static();
                };
                CancelButton.ClickFunc = () =>
                {
                    Hide();
                    onCancel();
                    UI_Blocker.Hide_Static();
                };
            }
            else
            {
                errorPopup.Show("No items unlocked of this type!");
            }
            
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
}
