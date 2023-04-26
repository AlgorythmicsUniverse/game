using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public enum TYPE
{
    INT,
    FLOAT,
    DOUBLE,
    STRING,
    BOOL
}

public class ItemSlot : MonoBehaviour, IDropHandler
{

    [SerializeField] private RectTransform parentObject;
    private string savedVariableName;
    private string savedValue;
    private TYPE savedValueType;
    public void OnDrop(PointerEventData eventData) 
    {
        GameObject draggedObject = eventData.pointerDrag;
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            if (draggedObject.transform.Find("Name"))
            {
                savedVariableName = draggedObject.transform.Find("Name").GetComponent<TMP_Text>().text;
                Debug.Log("Variable name: " + savedVariableName);
            }
            if (draggedObject.transform.Find("Value"))
            {
                Debug.Log("VALUE FOUND!");
                ParseValueByName(draggedObject);
            }
        }
    }

    private void ParseValueByName(GameObject draggedObject)
    {
        string gameObjectName = gameObject.name.Substring(0, gameObject.name.Length - 5);
        switch (gameObjectName)
        {
            case "int":
                savedValue = draggedObject.transform.Find("Value").GetComponent<TMP_Text>().text;
                savedValueType = TYPE.INT;
                Debug.Log("INT dropped, value: " + savedValue);
                break;
            case "float":
                savedValue = draggedObject.transform.Find("Value").GetComponent<TMP_Text>().text;
                savedValueType = TYPE.FLOAT;
                Debug.Log("FLOAT dropped, value: " + savedValue);
                break;
            case "double":
                savedValue = draggedObject.transform.Find("Value").GetComponent<TMP_Text>().text;
                savedValueType = TYPE.DOUBLE;
                Debug.Log("DOUBLE dropped, value: " + savedValue);
                break;
        }
    }
}
