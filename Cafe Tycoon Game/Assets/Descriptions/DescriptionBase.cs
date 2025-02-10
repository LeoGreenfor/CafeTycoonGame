using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Description", menuName = "ScriptableObjects/Object Description", order = 0)]
public class DescriptionBase : ScriptableObject
{
    public string Title;
    public Sprite Icon;
    public string Description;
}
