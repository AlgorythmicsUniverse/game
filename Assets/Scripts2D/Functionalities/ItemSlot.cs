using System.Collections;
using Scripts2D.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Scripts2D.Functionalities
{
    public class ItemSlot : MonoBehaviour, IDropHandler, IEnumerable
    {
        internal GameObject storedItem;
        [SerializeField] private RectTransform parentObject;
        [SerializeField] private ItemSlotType _itemSlotType;
        [SerializeField] private GameObject invalidPositionBlock;
        [SerializeField] private UI_ErrorPopup _uiErrorPopup;
        
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
                            draggedObject.transform.SetParent(invalidPositionBlock.transform);
                        }
                        else
                        {
                            if (CombineElements.IsMatchingType(gameObject.GetComponent<Collider2D>(),draggedObject.transform.Find("Type").GetComponent<TMP_Text>().text.ToLower()))
                            {
                                draggedObject.transform.parent = transform;
                                draggedObject.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
                                SetStoredObject(draggedObject);
                            }
                            else
                            {
                                var type = "";
                                if (draggedObject.transform.Find("Type"))
                                {
                                    type = draggedObject.transform.Find("Type").GetComponent<TMP_Text>().text;
                                }
                                Debug.Log("Not a valid object to place here!");
                                _uiErrorPopup.Show($"You can't place that item into this slot, your item's type is {type}!");
                                draggedObject.GetComponent<RectTransform>().position = invalidPositionBlock.transform
                                    .GetComponent<RectTransform>().position;
                                draggedObject.transform.SetParent(invalidPositionBlock.transform);
                            }
                        }
                        break;
                    case ItemSlotType.NewVarPlace:
                        Debug.Log("OnDrop Variable");
                        if (IsObject(draggedObject))
                        {
                            Debug.Log("Not a valid object to place here!");
                            _uiErrorPopup.Show("You can't place that item into this slot!");
                            draggedObject.GetComponent<RectTransform>().position = invalidPositionBlock.transform
                                .GetComponent<RectTransform>().position;
                            draggedObject.transform.SetParent(invalidPositionBlock.transform);
                        }
                        else
                        {
                            draggedObject.transform.parent = transform;
                            draggedObject.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
                            SetStoredObject(draggedObject);
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
                                SetStoredObject(draggedObject);
                            }
                            else
                            {
                                draggedObject.GetComponent<RectTransform>().position = draggedObject.transform.parent.transform.GetComponent<RectTransform>().position - new Vector3(692,300,0);
                                _uiErrorPopup.Show("You can't place that item into this slot!");
                                draggedObject.transform.SetParent(invalidPositionBlock.transform);
                            }
                        }
                        else
                        {
                            draggedObject.GetComponent<RectTransform>().position = draggedObject.transform.parent.transform.GetComponent<RectTransform>().position- new Vector3(692,300,0);
                            _uiErrorPopup.Show("You can't place that item into this slot!");
                            draggedObject.transform.SetParent(invalidPositionBlock.transform);
                        }
                        break;
                }
            }
        }

        private bool IsObject(GameObject draggedObject)
        {
            if (draggedObject.transform.Find("ObjectType").GetComponent<TMP_Text>().text == ObjectBlockTypeE.ExpressionBlock.ToString() || draggedObject.transform.Find("ObjectType").GetComponent<TMP_Text>().text == ObjectBlockTypeE.OperatorBlock.ToString() || draggedObject.transform.Find("ObjectType").GetComponent<TMP_Text>().text == ObjectBlockTypeE.ValueBlock.ToString() || draggedObject.transform.Find("ObjectType").GetComponent<TMP_Text>().text == ObjectBlockTypeE.ExistingVarBlock.ToString())
            {
                return true;
            }
            return false;
        }

        private void SetStoredObject(GameObject gameObject)
        {
            storedItem = gameObject;
        }

        public GameObject GetStoredObject()
        {
            return storedItem;
        }

        public IEnumerator GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}