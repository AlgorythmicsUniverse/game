using CodeMonkey.Utils;
using TMPro;
using UnityEngine;

namespace Scripts2D.Functionalities
{
    public class UI_ErrorPopup : MonoBehaviour
    {
        private Button_UI okButton;
        private TMP_Text errorMessage;
        private void Awake()
        {
            okButton = transform.Find("OKButton").GetComponent<Button_UI>();
            errorMessage = transform.Find("ErrorText").GetComponent<TMP_Text>();
            Hide();
        }
    
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Escape))
            {
                okButton.ClickFunc();
            }
        }
    
        public void Show(string errorMessage)
        {
            gameObject.SetActive(true);
            UI_Blocker.Show_Static();
            this.errorMessage.text = errorMessage;
            transform.SetAsLastSibling();

            okButton.ClickFunc = () =>
            {
                UI_Blocker.Hide_Static();
                Hide();
            };
        }
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
