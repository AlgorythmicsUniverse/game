using System.Collections.Generic;
using Scripts2D.Enums;
using Scripts2D.Interfaces;
using Scripts2D.Models2D;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts2D.Functionalities
{
    public class NewExpression:MonoBehaviour,IPointerDownHandler
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private UI_ErrorPopup errorPopup; 
        [SerializeField] private UI_DropdownVariableSelector dropdownVariableSelector;
        private int collectedNumber = 10;
        private BoxCollider boxCollider;
        private int clickCount;
        private static List<IBlock> unlockedExpressions = new();

        public void OnPointerDown(PointerEventData eventData)
        {
            UI_Blocker.Show_Static();
            dropdownVariableSelector.Show(
                unlockedExpressions,
                () =>
                {
                    Debug.Log("Cancel!");
                    UI_Blocker.Hide_Static();
                },
                operatorType =>
                {
                    if (clickCount < collectedNumber)
                    {
                        CreateNewExpressionElement(operatorType);
                        clickCount++;
                    }
                    else
                    {
                        errorPopup.Show("All collected items used!");
                    }
                }
            );
        }

        private void CreateNewExpressionElement(string expressionType)
        {
            var transform1 = transform;
            GameObject newGameObject = Instantiate(prefab,transform1.position + new Vector3(0,1,0),Quaternion.identity,transform1.parent);
            newGameObject.transform.Find("Value").GetComponent<TMP_Text>().text = expressionType;
            newGameObject.transform.Find("Value").transform.Find("Value").GetComponent<TMP_Text>().text = expressionType;
            newGameObject.transform.Find("ObjectType").GetComponent<TMP_Text>().text = ObjectBlockTypeE.ExpressionBlock.ToString();
        }

        private void SetCollectedNumber(int value)
        {
            collectedNumber = value;
        }

        public static void UnlockExpression(string opString)
        {
            var newExprGO = new GameObject("Expression");
            var newExpression = newExprGO.AddComponent<Expression>();
            switch (opString)
            {
                case "do":
                    newExpression.SetExpression(ExpressionsE.DO);
                    unlockedExpressions.Add(newExpression);
                    break;
                case "while":
                    newExpression.SetExpression(ExpressionsE.WHILE);
                    unlockedExpressions.Add(newExpression);
                    break;
                case "switch":
                    newExpression.SetExpression(ExpressionsE.SWITCH);
                    unlockedExpressions.Add(newExpression);
                    break;
                case "if":
                    newExpression.SetExpression(ExpressionsE.IF);
                    unlockedExpressions.Add(newExpression);
                    var expr2 = new GameObject("Expression").AddComponent<Expression>();
                    expr2.SetExpression(ExpressionsE.ELSE);
                    unlockedExpressions.Add(expr2);
                    var expr3 = new GameObject("Expression").AddComponent<Expression>();
                    expr3.SetExpression(ExpressionsE.ELSEIF);
                    unlockedExpressions.Add(expr3);
                    break;
                case "for":
                    newExpression.SetExpression(ExpressionsE.FOR);
                    unlockedExpressions.Add(newExpression);
                    break;
            }
        }
    }
}