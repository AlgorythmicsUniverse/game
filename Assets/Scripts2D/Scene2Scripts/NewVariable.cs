using System;
using System.Collections.Generic;
using Scripts2D.Enums;
using Scripts2D.Interfaces;
using Scripts2D.Models2D;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts2D.Scene2Scripts
{
    public class NewVariable : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private GameObject varPrefab;
        [SerializeField] private GameObject intPrefab;
        [SerializeField] private GameObject longPrefab;
        [SerializeField] private GameObject doublePrefab;
        [SerializeField] private GameObject floatPrefab;
        [SerializeField] private GameObject charPrefab;
        [SerializeField] private GameObject stringPrefab;
        [SerializeField] private GameObject boolPrefab;
        [SerializeField] private UI_InputWindowVariable inputWindow;
        [SerializeField] private UI_ErrorPopup errorPopup;
        [SerializeField] private int collectedNumber;
        private BoxCollider boxCollider;
        private int clickCount;
        private static Dictionary<string, TYPE> usedVariableNames = new Dictionary<string, TYPE>();
        private static List<IBlock> unlockedTypes = new List<IBlock>();

        public void Awake()
        {
            usedVariableNames.Clear();
        }

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
                (inputText, variableType) =>
                {
                    if (inputText.Length > 0)
                    {
                        if (clickCount < collectedNumber)
                        {
                            Debug.Log("OK!" + variableType);
                            UI_Blocker.Hide_Static();
                            Debug.Log($"VARIABLE TYPE TESZT: {variableType}");
                            CreateNewVariable(inputText,variableType);
                        }
                        else
                        {
                            errorPopup.Show("Max items reached of this block!");
                            Debug.Log("MAX ITEMS REACHED!");
                        }
                    }
                    else
                    {
                        errorPopup.Show("Please give the variable a name!");
                        Debug.Log("NO VARIABLE NAME PROVIDED!");
                    }
                }
            );
        }

        private void CreateNewVariable(string inputName, string variableType)
        {
            if (IsExistingVariableName(inputName))
            {
                errorPopup.Show("A variable with the same name already exists!");
                Debug.Log("Incorrect variable name!");
            }
            else
            {
                clickCount++;
                GameObject newObject;
                variableType = variableType.ToLower();
                switch (variableType)
                {
                    case "int":
                        newObject = Instantiate(intPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity, transform.parent);
                        break;
                    case "long":
                        newObject = Instantiate(longPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity, transform.parent);
                        break;
                    case "double":
                        newObject = Instantiate(doublePrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity, transform.parent);
                        break;
                    case "float":
                        newObject = Instantiate(floatPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity, transform.parent);
                        break;
                    case "char":
                        newObject = Instantiate(charPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity, transform.parent);
                        break;
                    case "string":
                        newObject = Instantiate(stringPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity, transform.parent);
                        break;
                    case "bool":
                        newObject = Instantiate(boolPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity, transform.parent);
                        break;
                    default:
                        newObject = Instantiate(varPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity, transform.parent);
                        break;
                }

                newObject.transform.Find("Name").GetComponent<TMP_Text>().text = inputName;
                usedVariableNames.Add(inputName, StringToVarType(variableType));
            }
        }

        private TYPE StringToVarType(string str)
        {
            switch (str)
            {
                case "int":
                    return TYPE.INT;
                case "long":
                    return TYPE.LONG;
                case "float":
                    return TYPE.FLOAT;
                case "double":
                    return TYPE.DOUBLE;
                case "char":
                    return TYPE.CHAR;
                case "string":
                    return TYPE.STRING;
                case "bool":
                    return TYPE.BOOL;
                default:
                    throw new ArgumentException("Invalid type string");
            }
        }
        public static bool IsExistingVariableName(string variableName)
        {
            return usedVariableNames.ContainsKey(variableName);
        }

        public static void UnlockType(string typeString)
        {
            var newVariableGO = new GameObject("Variable");
            var newVariable = newVariableGO.AddComponent<Variable>();
            switch (typeString)
            {
                case "int":
                    newVariable.SetVarType(TYPE.INT);
                    Debug.Log("INT UNLOCKED");
                    break;
                case "long":
                    newVariable.SetVarType(TYPE.LONG);
                    Debug.Log("LONG UNLOCKED");
                    break;
                case "double":
                    newVariable.SetVarType(TYPE.DOUBLE);
                    Debug.Log("DOUBLE UNLOCKED");
                    break;
                case "float":
                    newVariable.SetVarType(TYPE.FLOAT);
                    Debug.Log("FLOAT UNLOCKED");
                    break;
                case "string":
                    newVariable.SetVarType(TYPE.STRING);
                    break;
                case "char":
                    newVariable.SetVarType(TYPE.CHAR);
                    break;
                case "bool":
                    newVariable.SetVarType(TYPE.BOOL);
                    break;
            }
            unlockedTypes.Add(newVariable);
        }

        public static List<IBlock> GetUnlockedTypes()
        {
            return unlockedTypes;
        }

        public static Dictionary<string, TYPE> GetUsedVariableNames()
        {
            return usedVariableNames;
        }

        public static void ChangeVariableType(string varName, TYPE newType)
        {
            if (usedVariableNames.ContainsKey(varName))
            {
                usedVariableNames[varName] = newType;
            }
        }
    }
}
