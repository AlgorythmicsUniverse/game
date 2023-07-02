using UnityEngine;

namespace Scripts2D.Functionalities
{
    public class UI_Blocker : MonoBehaviour
    {
        private static UI_Blocker instance;

        private void Awake()
        {
            instance = this;

            Hide_Static();
        }

        public static void Show_Static()
        {
            instance.gameObject.SetActive(true);
            instance.transform.SetAsLastSibling();
        }

        public static void Hide_Static()
        {
            instance.gameObject.SetActive(false);
        }
    }
}
