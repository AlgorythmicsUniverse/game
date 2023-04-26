using UnityEngine;
using System.Collections;

public class MoveOnClick : MonoBehaviour
{
    // The position to move the GameObject to when it is clicked and the click count is 0 or a multiple of 2
    public Vector2 targetPosition1 = new Vector2(1, 2);

    // The position to move the GameObject to when it is clicked and the click count is not 0 or a multiple of 2
    public Vector2 targetPosition2 = new Vector2(3, 4);

    // The position to move the GameObject to if there is an object at target position 2
    public Vector2 targetPosition3 = new Vector2(5, 6);

    // The speed at which to move the GameObject
    public float speed = 5f;

    // A variable to hold the number of times the object has been clicked
    int clickCount = 0;

    void Update()
    {
        // If the left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Get the 2D position of the mouse
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Check if the mouse is over the GameObject
            if (GetComponent<Collider2D>().OverlapPoint(mousePosition))
            {
                // Increment the click count
                clickCount++;

                // If the click count is 0 or a multiple of 2
                if (clickCount == 0 || clickCount % 2 == 0)
                {
                    // Check if there is an object at the first target position
                    if (Physics2D.OverlapPoint(targetPosition1) != null)
                    {
                        // Print a message to the console
                        Debug.Log("Cannot move to target position 1 because there is an object there");
                    }
                    else
                    {
                        // Move the GameObject to the first target position over time
                        StartCoroutine(MoveToPosition(targetPosition1, speed));
                    }
                }
                else
                {
                    // Check if there is an object at the second target position
                    if (Physics2D.OverlapPoint(targetPosition2) != null)
                    {
                        // Check if there is an object at the third target position
                        if (Physics2D.OverlapPoint(targetPosition3) != null)
                        {
                            // Print a message to the console
                            Debug.Log("Cannot move to target position 3 because there is an object there");
                        }
                        else
                        {
                            // Move the GameObject to the third target position over time
                            StartCoroutine(MoveToPosition(targetPosition3, speed));
                        }
                    }
                    else
                    {
                        // Move the GameObject to the second target position over time
                        StartCoroutine(MoveToPosition(targetPosition2, speed));
                    }
                }
            }
        }
    }

    IEnumerator MoveToPosition(Vector2 target, float speed)
    {
        while (transform.position != (Vector3)target)
        {
            // Move the GameObject towards the target position
            transform.position = Vector3.MoveTowards(transform.position, (Vector3)target, speed * Time.deltaTime);

            // Wait for the next frame before continuing
            yield return null;
        }
    }
}
