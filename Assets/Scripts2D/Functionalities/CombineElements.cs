using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts2D.Functionalities
{
    public class CombineElements : MonoBehaviour,IEndDragHandler, IBeginDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        private Vector2 mousePosition;
        private Vector3 offset;
        private CanvasGroup canvasGroup;
        private static bool mouseButtonReleased;
        private Transform originalParent;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            var parent = transform.parent;
            originalParent = parent;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            mouseButtonReleased = false;
            System.Diagnostics.Debug.Assert(Camera.main != null, "Camera.main != null");
            offset = transform.position - new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0f);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("OnBeginDrag");
            Debug.Log(transform.parent.name);
            transform.SetParent(originalParent);
            canvasGroup.alpha = .6f;
            canvasGroup.blocksRaycasts = false;

            // Letiltjuk a gyermek elemek raycast-ét
            foreach (var child in transform.GetComponentsInChildren<Transform>())
            {
                var childCanvasGroup = child.GetComponent<CanvasGroup>();
                if (childCanvasGroup != null)
                {
                    childCanvasGroup.blocksRaycasts = false;
                    childCanvasGroup.interactable = false;
                }
            }
        }


        public void OnDrag(PointerEventData eventData)
        {
            System.Diagnostics.Debug.Assert(Camera.main != null, "Camera.main != null");
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var vector = new Vector3(mousePosition.x, mousePosition.y, 0f) + offset;
            transform.position = vector;
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
            //var objectName = gameObject.transform.Find("Name").GetComponent<TMP_Text>().text;
            string objectType = "no type";
            if (gameObject.transform.Find("Type"))
            {
                objectType = gameObject.transform.Find("Type").GetComponent<TMP_Text>().text;
            }
            Debug.Log($"OnEndDrag = {objectType}");
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        
            // Visszaállítjuk a gyermek elemek raycast-ét
            foreach (var child in transform.GetComponentsInChildren<Transform>())
            {
                var childCanvasGroup = child.GetComponent<CanvasGroup>();
                if (childCanvasGroup != null)
                {
                    childCanvasGroup.blocksRaycasts = true;
                    childCanvasGroup.interactable = true;
                }
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            mouseButtonReleased = true;
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (gameObject.transform.Find("") != null)
            {
                string objectType = null;
                if (gameObject.transform.Find("Type"))
                {
                    objectType = gameObject.transform.Find("Type").GetComponent<TMP_Text>().text;
                }

                if (collision.transform.Find("Type") != null && objectType != null)
                {
                    Debug.Log($"type of itemplace found: {collision.transform.Find("Type").GetComponent<TMP_Text>().text}" );
                    if (IsMatchingType(collision, objectType))
                    {
                        Debug.Log("Matching type!");
                        if (mouseButtonReleased)
                        {
                            Debug.Log("Object type: " + objectType);
                            transform.SetParent(collision.transform);
                        }
                    }
                    else
                    {
                        Debug.Log("Not matching types!");
                    }
                }
                else
                {
                    if (mouseButtonReleased)
                    {
                        Debug.Log("Object  type: " + objectType);
                        transform.SetParent(collision.transform);
                    }
                }
                mouseButtonReleased = false;
            }
        }
        internal static bool IsMatchingType(Collider2D collision,string objectType)
        {
            return collision.transform.Find("Type").GetComponent<TMP_Text>().text.ToLower().Equals(objectType.ToLower());
        }
    }
}
