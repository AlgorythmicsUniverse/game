using System;
using CodeMonkey.Utils;
using Scripts2D.Models2D;
using TMPro;
using UnityEngine;

namespace Scripts2D.Functionalities
{
    public class UI_InputWindowVariable : MonoBehaviour
    {
        [SerializeField] private UI_ErrorPopup errorPopup;
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
            variableDropdown.ClearOptions();
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
            variableDropdown.ClearOptions();
            var dropdownItems = NewVariable.GetUnlockedTypes();
            foreach (var item in dropdownItems )
            {
                var nItem = (Variable)item;
                Debug.Log($"TESZT: {nItem.GetVarType().ToString()}");
                variableDropdown.options.Add(new TMP_Dropdown.OptionData() { text = nItem.GetVarType().ToString() });
            }
            DropdownItemSelected(variableDropdown);
        
            variableDropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(variableDropdown); });
            if (variableDropdown.options.Count < 1)
            {
                errorPopup.Show("No items unlocked of this type!");
            }
            else
            {
                gameObject.SetActive(true);
                transform.SetAsLastSibling();
                
                inputField.characterLimit = characterLimit;
                inputField.onValidateInput = (_, index, addedChar) =>
                {
                    return ValidateChar(validCharacters, index, addedChar);
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
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private char ValidateChar(string validCharacters, int index,char addedChar)
        {
            if (index == 0) {
                if ("1234567890".IndexOf(addedChar) != -1) {
                    return '\0';
                }
            }
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
            dropdown.RefreshShownValue();
            int index = dropdown.value;
            if (dropdown.options.Count > 0) {
                Debug.Log($"SELECTED FROM DROPDOWN: {dropdown.options[index].text}");
                variableType = dropdown.options[index].text;
            }
        }

    }
}
