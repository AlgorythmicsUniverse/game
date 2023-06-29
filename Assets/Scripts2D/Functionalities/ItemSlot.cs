using Scripts2D.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts2D.Functionalities
{
    public class ItemSlot : MonoBehaviour, IDropHandler
    {

        [SerializeField] private RectTransform parentObject;
        private string savedVariableName;
        private string savedValue;
        private TYPE savedValueType;
        [SerializeField] private ItemSlotType _itemSlotType;
        [SerializeField] private GameObject invalidPositionBlock;
        public void OnDrop(PointerEventData eventData) 
        {
            var draggedObject = eventData.pointerDrag;
            if (eventData.pointerDrag != null)
            {
                switch (_itemSlotType)
                {
                    case ItemSlotType.ObjectPlace:
                        Debug.Log("OnDrop Object");
                        if (!IsObject(draggedObject))
                        {
                            Debug.Log("Not a valid object to place here!");
                            draggedObject.GetComponent<RectTransform>().position = invalidPositionBlock.transform
                                .GetComponent<RectTransform>().position;
                        }
                        else
                        {
                            draggedObject.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
                        }
                        break;
                    case ItemSlotType.NewVarPlace:
                        Debug.Log("OnDrop Variable");
                        if (IsObject(draggedObject))
                        {
                            Debug.Log("Not a valid object to place here!");
                            draggedObject.GetComponent<RectTransform>().position = invalidPositionBlock.transform
                                .GetComponent<RectTransform>().position;
                        }
                        else
                        {
                            draggedObject.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
                        }
                        break;
                    case ItemSlotType.NewVarValueSlot:
                        Debug.Log("OnDrop Value to Var");
                        if (draggedObject.transform.Find("Type") && parentObject.transform.Find("Type"))
                        {
                            var draggedObjectType = draggedObject.transform.Find("Type").GetComponent<TMP_Text>().text.ToLower();
                            var gameObjectType = parentObject.transform.Find("Type")
                                .GetComponent<TMP_Text>().text.ToLower();
                            if (draggedObjectType.Equals(gameObjectType))
                            {
                                Debug.Log($"Item type : {draggedObjectType} " +
                                          $"ItemSlot type: {gameObjectType}");
                                draggedObject.GetComponent<RectTransform>().position =
                                    GetComponent<RectTransform>().position;
                            }
                            else
                            {
                                draggedObject.GetComponent<RectTransform>().position = draggedObject.transform.parent.transform.GetComponent<RectTransform>().position - new Vector3(692,300,0);
                            }
                        }
                        else
                        {
                            draggedObject.GetComponent<RectTransform>().position = draggedObject.transform.parent.transform.GetComponent<RectTransform>().position- new Vector3(692,300,0);
                        }
                        break;
                }
            }
        }

        private bool IsObject(GameObject draggedObject)
        {
            if (draggedObject.transform.Find("ObjectType").GetComponent<TMP_Text>().text == ObjectBlockTypeE.OperatorBlock.ToString() || draggedObject.transform.Find("ObjectType").GetComponent<TMP_Text>().text == ObjectBlockTypeE.ValueBlock.ToString() || draggedObject.transform.Find("ObjectType").GetComponent<TMP_Text>().text == ObjectBlockTypeE.ExistingVarBlock.ToString())
            {
                return true;
            }
            return false;
        }
        
    }
}