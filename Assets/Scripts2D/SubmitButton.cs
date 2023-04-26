using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public SpriteRenderer firstItem;
    public SpriteRenderer secondItem;
    public float firstPositionX;
    public float firstPositionY;
    public float secondPositionX;
    public float secondPositionY;

    void Update()
    {
        // Check if the left mouse button was clicked
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Input.mousePosition;
            // Check if the mouse was clicked over a UI element
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            // Check if the mouse position is within the bounds of the object
            if (GetComponent<Collider2D>().bounds.Contains(worldPos))
            {
                if (firstItem != null && secondItem != null)
                {
                    if ((firstItem.transform.position.x == firstPositionX && firstItem.transform.position.y == firstPositionY) &&
                        (secondItem.transform.position.x == secondPositionX && secondItem.transform.position.y == secondPositionY))
                    {
                        Debug.Log("The sprite is at the specified position.");
                    }else
                    {
                        Debug.Log("Np");
                    }
                }
            }
        }
        
    }
}
