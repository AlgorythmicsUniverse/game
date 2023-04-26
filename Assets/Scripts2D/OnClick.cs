using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnClick : MonoBehaviour
{
    // The text that should be displayed in the text bubble
    public string text = "Alma";

    // The text bubble UI element
    public GameObject textBubble;

    // The time after which the text bubble should be hidden, in seconds
    public float hideDelay = 5f;

    // A variable to hold a reference to the coroutine that hides the text bubble
    Coroutine hideCoroutine;

    // Update is called once per frame
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
                // Show the text bubble and set the text
                if (textBubble != null)
                {
                    textBubble.GetComponentInChildren<Text>().text = text;
                    textBubble.SetActive(true);

                    // Stop the previous coroutine that hides the text bubble
                    if (hideCoroutine != null)
                    {
                        StopCoroutine(hideCoroutine);
                    }

                    // Start a new coroutine to hide the text bubble after the specified delay
                    hideCoroutine = StartCoroutine(HideTextBubbleAfterDelay(hideDelay));
                }
            }
        }
    }

    IEnumerator HideTextBubbleAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Hide the text bubble
        textBubble.SetActive(false);
    }
}
