using System;
using CodeMonkey.Utils;
using TMPro;
using UnityEngine;

namespace Scripts2D.Scene2Scripts
{
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
            variableDropdown.options.Clear();
            var dropdownItems = NewVariable.GetUnlockedTypes();
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
}
