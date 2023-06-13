using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts2D.Scene2Scripts
{
    public class NewOperator : MonoBehaviour,IPointerDownHandler
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private UI_ErrorPopup errorPopup; 
        [SerializeField] private UI_DropdownVariableSelector dropdownVariableSelector;
        private int collectedNumber = 10;
        private BoxCollider boxCollider;
        private int clickCount;
        private static List<string> unlockedOperators = new();


        private void Start()
        {
            unlockedOperators = new List<string>();
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
            newGameObject.transform.Find("Type").GetComponent<TMP_Text>().text = operatorType;
        }

        private void SetCollectedNumber(int value)
        {
            collectedNumber = value;
        }

        public static void UnlockOperator(string opString)
        {
            unlockedOperators.Add(opString);
        }
    }
}
