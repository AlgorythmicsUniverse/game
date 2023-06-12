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
            if (gameObject.transform.Find("Type"))
            {
                string targetObjectName = gameObject.transform.Find("Type").GetComponent<TMP_Text>().text + "Value";
                Transform targetObject = parentObject.transform.Find(targetObjectName);
                if (targetObject != null)
                {
                    Vector2 itemSlotPosition = targetObject.GetComponent<RectTransform>().position;
                    draggedObject.GetComponent<RectTransform>().position = itemSlotPosition;
                }
                else
                {
                    draggedObject.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
                }
            }
            else
            {
                draggedObject.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
            }

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
        Debug.Log(gameObjectName);
        var draggedObjectType = draggedObject.transform.Find("Type").GetComponent<TMP_Text>().text.ToLower();
        if (draggedObjectType == gameObjectName)
        {
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
        else
        {
            Debug.Log("Cannot assign value to variable!");
        }
        
    }
}
