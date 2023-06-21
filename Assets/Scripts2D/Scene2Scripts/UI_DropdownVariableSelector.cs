using System;
using System.Collections.Generic;
using CodeMonkey.Utils;
using Scripts2D.Interfaces;
using Scripts2D.Models2D;
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
        [SerializeField] private UI_ErrorPopup errorPopup;

        private void Awake()
        {
            OKButton = transform.Find("OKButton").GetComponent<Button_UI>();
            CancelButton = transform.Find("CancelButton").GetComponent<Button_UI>();
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
    
        public void Show(List<IBlock> items, Action onCancel, Action<string> onOk)
        {
            variableDropdown.ClearOptions();

            if (items.Count > 0)
            {
                if (IsOperator(items[0]))
                {
                    foreach (var item in items)
                    {
                        var newItem = (Operator)item;
                        variableDropdown.options.Add(new TMP_Dropdown.OptionData() { text = newItem.GetOpSymbol() });
                    }
                }
                else if (IsVariable(items[0]))
                {
                    foreach (var item in items)
                    {
                        var newItem = (Variable)item;
                        variableDropdown.options.Add(new TMP_Dropdown.OptionData() { text = newItem.GetVarType().ToString() });
                    }
                }
                
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
                Hide();
            }
        }

        private bool IsOperator(IBlock item)
        {
            if (item is Operator nItem && nItem.GetOpSymbol() != null)
            {
                Debug.Log("EZ EGY TESZT OPERATOR");
                return true;
            }

            return false;
        }

        private bool IsVariable(IBlock item)
        {
            if (item is Variable nItem && nItem.GetVarType() != null)
            {
                Debug.Log("EZ EGY TESZT VAR");
                return true;
            }

            return false;
        }
        
        private void Hide()
        {
            gameObject.SetActive(false);
        }

        private void DropdownItemSelected(TMP_Dropdown dropdown)
        {
            dropdown.RefreshShownValue();
            int index = dropdown.value;
            Debug.Log($"SELECTED FROM DROPDOWN: {dropdown.options[index].text}");
            variableType = dropdown.options[index].text;
        }

    }
}
