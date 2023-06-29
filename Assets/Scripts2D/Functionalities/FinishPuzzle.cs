using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts2D.Functionalities
{
    public class FinishPuzzle : MonoBehaviour,IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            SceneController.nextPuzzle();
        }
    }
}