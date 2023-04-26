using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class NewVariable : MonoBehaviour,IPointerDownHandler
{
   [SerializeField] private GameObject intPrefab;
   [SerializeField] private GameObject doublePrefab;
   [SerializeField] private GameObject floatPrefab;
   [SerializeField] private GameObject stringPrefab;
   [SerializeField] private GameObject boolPrefab;
   [SerializeField] private UI_InputWindowVariable inputWindow;
   [SerializeField] private UI_ErrorPopup errorPopup;
   [SerializeField] private int collectedNumber;
   private BoxCollider boxCollider;
   private int clickCount = 0;
   public List<KeyValuePair<string,string>> usedVariableNames = new();

   public void OnPointerDown(PointerEventData eventData)
   {
      UI_Blocker.Show_Static();
      inputWindow.Show(
         "abcdefghijklmnopqrstuvwxyz",
         5,
         () =>
         {
            Debug.Log("Cancel");
            UI_Blocker.Hide_Static();
         },
         (string inputText,string variableType) =>
         {
            if (inputText.Length > 0)
            {
               if (clickCount < collectedNumber)
               {
                  Debug.Log("OK!" + variableType);
                  UI_Blocker.Hide_Static();
                  CreateNewVariable(inputText, variableType);
               }
               else
               {
                  errorPopup.Show("Max items reached of this type!");
                  Debug.Log("MAX ITEMS REACHED!");
               }
            }
            else
            {
               errorPopup.Show("Please give the variable a name!");
               Debug.Log("NO VARIABLE NAME PROVIDED!");
            }
         }
      );
   }
   public void SetCollectedCount(int itemsCollected)
   {
      collectedNumber = itemsCollected;
   }

   private void CreateNewVariable(string inputName,string variableType)
   {
      GameObject newObject = new GameObject();
      if (isExistingVariableName(inputName))
      {
         errorPopup.Show("A variable with the same name already exists!");
         Debug.Log("Incorrect variable name!");
      }
      else
      {
         clickCount++;
         switch (variableType)
         {
            case "INT":
               newObject = Instantiate(intPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity, transform.parent);
               newObject.transform.Find("Name").GetComponent<TMP_Text>().text = inputName;
               usedVariableNames.Add(new KeyValuePair<string,string>(inputName,variableType));
               break;
            case "DOUBLE":
               newObject = Instantiate(doublePrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity, transform.parent);
               newObject.transform.Find("Name").GetComponent<TMP_Text>().text = inputName;
               usedVariableNames.Add(new KeyValuePair<string,string>(inputName,variableType));
               break;
            case "FLOAT":
               newObject = Instantiate(floatPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity, transform.parent);
               newObject.transform.Find("Name").GetComponent<TMP_Text>().text = inputName;
               usedVariableNames.Add(new KeyValuePair<string,string>(inputName,variableType));
               break;
            case "STRING":
               newObject = Instantiate(stringPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity, transform.parent);
               newObject.transform.Find("Name").GetComponent<TMP_Text>().text = inputName;
               usedVariableNames.Add(new KeyValuePair<string,string>(inputName,variableType));
               break;
            case "BOOLEAN":
               newObject = Instantiate(boolPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity, transform.parent);
               newObject.transform.Find("Name").GetComponent<TMP_Text>().text = inputName;
               usedVariableNames.Add(new KeyValuePair<string,string>(inputName,variableType));
               break;
         }
      }
   }

   public bool isExistingVariableName(string variableName)
   {
      foreach (KeyValuePair<string, string> pair in usedVariableNames)
      {
         if (variableName == pair.Key)
         {
            return true;
         }
      }
      return false;
   }
}
