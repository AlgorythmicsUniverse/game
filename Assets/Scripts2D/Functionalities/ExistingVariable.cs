using Scripts2D.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts2D.Functionalities
{
    public class ExistingVariable : MonoBehaviour,IPointerDownHandler
    {
        [SerializeField] private GameObject variablePrefab;
        [SerializeField] private UI_InputWindow inputWindow;
        [SerializeField] private UI_ErrorPopup errorPopup;

        public void OnPointerDown(PointerEventData eventData)
        {
            UI_Blocker.Show_Static();
            inputWindow.Show(
                "abcdefghijklmnopqrstuvwxyz1234567890",
                12,
                () =>
                {
                    Debug.Log("Cancel");
                    UI_Blocker.Hide_Static();
                },
                inputText =>
                {
                    if (NewVariable.IsExistingVariableName(inputText))
                    {
                        Debug.Log("OK!" );
                        CreateVariableItem(inputText);
                        UI_Blocker.Hide_Static();
                    }
                    else
                    {
                        errorPopup.Show("No variable found with given name!");
                        Debug.Log("No such variable!");
                    }
                }
            );
        }

        private void CreateVariableItem(string variableName)
        {
            foreach (var pair in NewVariable.GetUsedVariableNames())
            {
                if (variableName == pair.Key)
                {
                    var transform1 = transform;
                    GameObject newObject = Instantiate(variablePrefab, transform1.position + new Vector3(0, 1, 0), Quaternion.identity, transform1.parent);
                    newObject.transform.Find("Name").GetComponent<TMP_Text>().text = variableName;
                    newObject.transform.Find("ObjectType").GetComponent<TMP_Text>().text =
                        ObjectBlockTypeE.ExistingVarBlock.ToString();
                }
            }
        }
    }
}
