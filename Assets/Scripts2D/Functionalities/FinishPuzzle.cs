using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Scripts2D.Functionalities
{
    public class FinishPuzzle : MonoBehaviour, IPointerDownHandler
    {
        private UI_ErrorPopup _errorPopup;

        public void OnPointerDown(PointerEventData eventData)
        {
            _errorPopup = gameObject.transform.parent.Find("UI_ErrorPopup").GetComponent<UI_ErrorPopup>();
            if (AllBlockCorrect(transform.parent.Find("Geometry")))
            {
                SceneController.nextPuzzle();
                Debug.Log($"All slots filled!");
            }
            else
            {
                _errorPopup.Show("You have to fill all item slots! Try Again!");
            }
        }

        private bool AllBlockCorrect(Transform parent)
        {
            var children = parent.GetComponentsInChildren<ItemSlot>();
            Debug.Log($"COUNT: {children.Length.ToString()}");
            var filledSlots = new List<bool>();
            foreach (var child in children)
            {
                if (child.GetStoredObject() != null)
                {
                    filledSlots.Add(true);
                }
                else
                {
                    filledSlots.Add(false);
                }
            }

            if (filledSlots.Count == children.Length && !filledSlots.Contains(false))
            {
                return true;
            }

            return false;
        }
    }
}