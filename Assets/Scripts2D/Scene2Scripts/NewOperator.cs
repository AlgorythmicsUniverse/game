using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewOperator : MonoBehaviour,IPointerDownHandler
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private UI_ErrorPopup errorPopup;
    [SerializeField] private int collectedNumber;
    [SerializeField] private UI_DropdownVariableSelector dropdownVariableSelector;
    private BoxCollider boxCollider;
    private int clickCount = 0;
    List<string> dropdownItems = new List<string>();
    
    private void Awake()
    {
        dropdownItems.Add("+");
        dropdownItems.Add("-");
        dropdownItems.Add("=");
        dropdownItems.Add("*");
        dropdownItems.Add("/");
        dropdownItems.Add(";");
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        UI_Blocker.Show_Static();
        dropdownVariableSelector.Show(
            dropdownItems,
            () =>
            {
                Debug.Log("Cancel!");
                UI_Blocker.Hide_Static();
            },
            operatorType =>
            {
                if (clickCount != collectedNumber)
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
}
