using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts2D.Functionalities
{
    public class NewArray : MonoBehaviour, IPointerDownHandler
    {
        public GameObject prefab5Length;
        [SerializeField] private UI_InputWindow inputWindow;
        private BoxCollider boxCollider;
        private double arrayLength;

        [SerializeField] private int collectedNumber = 0;
        private int clickCount = 0;

        public void OnPointerDown(PointerEventData eventData)
        {
            UI_Blocker.Show_Static();
            inputWindow.Show(
                "0123456789",
                2,
                () => 
                { 
                    Debug.Log("Cancel!");
                    UI_Blocker.Hide_Static();
                },
                (string InputText) => 
                {
                    if (clickCount != collectedNumber)
                    {
                        Debug.Log("OK! " + InputText);
                        CreateNewArray(int.Parse(InputText));
                        UI_Blocker.Hide_Static();
                        clickCount++;
                    }
                    else
                    {
                        Debug.Log("MAX ITEMS REACHED!");
                        UI_Blocker.Hide_Static();
                    }
                }
            );
        }

        private void CreateNewArray(int value)
        {
            if(value == 5)
            {
                GameObject newObject = Instantiate(prefab5Length, transform.position + new Vector3(0, 1, 0), Quaternion.identity, transform.parent);
            }
        }
    }
}