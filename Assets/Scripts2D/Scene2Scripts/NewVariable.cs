using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts2D.Scene2Scripts
{
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
      private int clickCount;
      private static Dictionary<string,string> usedVariableNames = new();
      private static List<string> unlockedTypes = new();

      public void Awake()
      {
         usedVariableNames.Clear();
         if (!unlockedTypes.Contains("INT"))
         {
            UnlockType("INT");
         }
      }
   
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
            (inputText, variableType) =>
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

      private void CreateNewVariable(string inputName,string variableType)
      {
         if (IsExistingVariableName(inputName))
         {
            errorPopup.Show("A variable with the same name already exists!");
            Debug.Log("Incorrect variable name!");
         }
         else
         {
            clickCount++;
            GameObject newObject;
            switch (variableType)
            {
               case "INT":
                  var transform1 = transform;
                  newObject = Instantiate(intPrefab, transform1.position + new Vector3(0, 1, 0), Quaternion.identity, transform1.parent);
                  newObject.transform.Find("Name").GetComponent<TMP_Text>().text = inputName;
                  usedVariableNames.Add(inputName,variableType);
                  break;
               case "DOUBLE":
                  var transform2 = transform;
                  newObject = Instantiate(doublePrefab, transform2.position + new Vector3(0, 1, 0), Quaternion.identity, transform2.parent);
                  newObject.transform.Find("Name").GetComponent<TMP_Text>().text = inputName;
                  usedVariableNames.Add(inputName,variableType);
                  break;
               case "FLOAT":
               {
                  var transform3 = transform;
                  newObject = Instantiate(floatPrefab, transform3.position + new Vector3(0, 1, 0), Quaternion.identity, transform3.parent);
                  newObject.transform.Find("Name").GetComponent<TMP_Text>().text = inputName;
                  usedVariableNames.Add(inputName,variableType);
                  break;
               }
               case "STRING":
                  var transform4 = transform;
                  newObject = Instantiate(stringPrefab, transform4.position + new Vector3(0, 1, 0), Quaternion.identity, transform4.parent);
                  newObject.transform.Find("Name").GetComponent<TMP_Text>().text = inputName;
                  usedVariableNames.Add(inputName,variableType);
                  break;
               case "BOOLEAN":
                  var transform5 = transform;
                  newObject = Instantiate(boolPrefab, transform5.position + new Vector3(0, 1, 0), Quaternion.identity, transform5.parent);
                  newObject.transform.Find("Name").GetComponent<TMP_Text>().text = inputName;
                  usedVariableNames.Add(inputName,variableType);
                  break;
            }
         }
      }

      public static bool IsExistingVariableName(string variableName)
      {
         foreach (var pair in usedVariableNames)
         {
            if (variableName == pair.Key)
            {
               return true;
            }
         }
         return false;
      }

      public static void UnlockType(string typeString)
      {
         unlockedTypes.Add(typeString);
      }

      public static List<string> GetUnlockedTypes()
      {
         return unlockedTypes;
      }

      public static Dictionary<string, string> GetUsedVariableNames()
      {
         return usedVariableNames;
      }

   }
}
