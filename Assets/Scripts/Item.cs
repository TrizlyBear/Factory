using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Factory/Item", order = 1)]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    public int maxStackSize;
}
