using System;
using CodeMonkey.Utils;
using TMPro;
using UnityEngine;

namespace Scripts2D.Functionalities
{
    public class UI_InputWindow : MonoBehaviour
    {

        private Button_UI OKButton;
        private Button_UI CancelButton;
        private TMP_InputField inputField;

        private void Awake()
        {
            OKButton = transform.Find("OKButton").GetComponent<Button_UI>();
            CancelButton = transform.Find("CancelButton").GetComponent<Button_UI>();
            inputField = transform.Find("InputField").GetComponent<TMP_InputField>();
            inputField.text = "";

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

        public void Show(string validCharacters,int characterLimit,Action onCancel,Action<string> onOk)
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
                onOk(inputField.text);
                inputField.text = "";
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

    }
}
