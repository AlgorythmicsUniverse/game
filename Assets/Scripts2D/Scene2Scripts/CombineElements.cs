using UnityEngine;
using UnityEngine.EventSystems;

public class CombineElements : MonoBehaviour,IEndDragHandler, IBeginDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{ 
    private Vector2 mousePosition;
    private Vector3 offset;
    private CanvasGroup canvasGroup;
    private bool mouseButtonReleased;
    private Transform originalParent;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        originalParent = transform.parent;
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
    }

    public void OnDrag(PointerEventData eventData)
    {
        System.Diagnostics.Debug.Assert(Camera.main != null, "Camera.main != null");
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, 0f) + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        mouseButtonReleased = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        string thisGameObjectName = gameObject.name;
        string collisionGameObjectName = collision.gameObject.name;

        if (mouseButtonReleased)
        {
            //if (itemPlacedInArray(thisGameObjectName, collisionGameObjectName,collision))
            //{
                Debug.Log(thisGameObjectName);
                transform.SetParent(collision.transform);
                //}
        }
    }

    /*private bool itemPlacedInArray(string thisGameObjectName, string collisionGameObjectName,Collider2D collision)
    {
        Debug.Log(collisionGameObjectName.Length);
        if (collisionGameObjectName.Length > 10)
        {
            collisionGameObjectName = collisionGameObjectName.Remove(collisionGameObjectName.Length - 1);
        }
        if (thisGameObjectName == "ArrayElement(Clone)" && collisionGameObjectName == "ArrayPlace")
        {
            return true;
        }

        if (thisGameObjectName == "ArrayElement(Clone)" && collisionGameObjectName == "Value")
        {
            return true;
        }
        return false;
    }*/

}
