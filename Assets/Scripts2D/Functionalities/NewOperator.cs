using System.Collections.Generic;
using Scripts2D.Enums;
using Scripts2D.Interfaces;
using Scripts2D.Models2D;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts2D.Functionalities
{
    public class NewOperator : MonoBehaviour,IPointerDownHandler
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private UI_ErrorPopup errorPopup; 
        [SerializeField] private UI_DropdownVariableSelector dropdownVariableSelector;
        private int collectedNumber = 10;
        private BoxCollider boxCollider;
        private int clickCount;
        private static List<IBlock> unlockedOperators = new();


        private void Start()
        {
            unlockedOperators = new List<IBlock>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            UI_Blocker.Show_Static();
            dropdownVariableSelector.Show(
                unlockedOperators,
                () =>
                {
                    Debug.Log("Cancel!");
                    UI_Blocker.Hide_Static();
                },
                operatorType =>
                {
                    if (clickCount < collectedNumber)
                    {
                        CreateNewOperatorElement(operatorType);
                        clickCount++;
                    }
                    else
                    {
                        errorPopup.Show("All collected items used!");
                    }
                }
            );
        }

        private void CreateNewOperatorElement(string operatorType)
        {
            var transform1 = transform;
            GameObject newGameObject = Instantiate(prefab,transform1.position + new Vector3(0,1,0),Quaternion.identity,transform1.parent);
            newGameObject.transform.Find("Value").GetComponent<TMP_Text>().text = operatorType;
            newGameObject.transform.Find("ObjectType").GetComponent<TMP_Text>().text = ObjectBlockTypeE.OperatorBlock.ToString();
        }

        private void SetCollectedNumber(int value)
        {
            collectedNumber = value;
        }

        public static void UnlockOperator(string opString)
        {
            var newOperatorGO = new GameObject("Operator");
            var newOperator = newOperatorGO.AddComponent<Operator>();
            switch (opString)
            {
                case "+":
                    newOperator.SetOperator(OperatorE.Add);
                    unlockedOperators.Add(newOperator);
                    break;
                case "-":
                    newOperator.SetOperator(OperatorE.Subtract);
                    unlockedOperators.Add(newOperator);
                    break;
                case "*":
                    newOperator.SetOperator(OperatorE.Multiply);
                    unlockedOperators.Add(newOperator);
                    break;
                case "/":
                    newOperator.SetOperator(OperatorE.Divide);
                    unlockedOperators.Add(newOperator);
                    break;
                case "(":
                    newOperator.SetOperator(OperatorE.LParenthesis);
                    unlockedOperators.Add(newOperator);
                    break;
                case ")":
                    newOperator.SetOperator(OperatorE.RParenthesis);
                    unlockedOperators.Add(newOperator);
                    break;
                case "^":
                    newOperator.SetOperator(OperatorE.Exponentiation);
                    unlockedOperators.Add(newOperator);
                    break;
                case "%":
                    newOperator.SetOperator(OperatorE.Modulus);
                    unlockedOperators.Add(newOperator);
                    break;
                case "=":
                    newOperator.SetOperator(OperatorE.Assignment);
                    unlockedOperators.Add(newOperator);
                    break;
                
            }
        }

    }
}
