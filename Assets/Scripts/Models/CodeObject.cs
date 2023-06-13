using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeObject : MonoBehaviour
{
    public string Name;
    public CodeObjectType Type;
    public string Code;
    [TextArea]
    public string Description;
    
    public bool disabled = false;
}
