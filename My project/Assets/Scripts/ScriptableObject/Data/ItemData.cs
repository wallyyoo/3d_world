using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipable,
    Consumable,
    Obstacle
    
}

public enum ConsumableType
{
    Health,
    Stamina
}

[Serializable]
public class ItemDataConsumable
{
    public ConsumableType Type;
    public float value;
    
}


[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
  [Header("Info")]
  
  public string ItemName;
  public string ItemDescription;
  public Sprite ItemIcon;
  public ItemType ItemType;
  public GameObject dropPrefab;
  
  [Header("Stacking")]
  
  public bool canStack;
  public int maxStack;
  
  [Header("Consumable")]
  
  public ItemDataConsumable[] consumables;
  public bool canConsume;
  
  
}
