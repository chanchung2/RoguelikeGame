using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New item", menuName ="new Item/item")]
public class Item : ScriptableObject
{
    public enum ItemType { Weapon, Other };

    public string itemName;
    public Sprite itemImage;
    public GameObject itemPrefab;
    public ItemType itemType; 
}
