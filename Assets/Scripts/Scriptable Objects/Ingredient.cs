using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "Scriptable Object/New Ingredient")]
public class Ingredient : ScriptableObject
{
    public Sprite picture;
    public int id;
    public int beaterId;
}
