using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
   public ItemSlot[] slots;
   
   public GameObject inventorySlot;
   public Transform slotPanel;

   public TextMeshProUGUI selectedItemName;
   public TextMeshProUGUI selectedItemDescription;
   public TextMeshProUGUI invenUI_Health;
   public TextMeshProUGUI invenUI_Stamina;
   public TextMeshProUGUI inven_HealthVelue;
   public TextMeshProUGUI inven_StaminaVelue;
   
   public GameObject useButton;
   public GameObject dropButton;
   
   private PlayerController controller;
   private PlayerCondition condition;

   void Start()
   {
      controller = CharacterManager.Instance.Player.controller;
      condition = CharacterManager.Instance.Player.condition;
      controller.inventory += ToggleInventory;
      
      CharacterManager.Instance.Player.addItem += AddItem;
      
      inventorySlot.SetActive(false);
      slots = new ItemSlot[slotPanel.childCount];

      for (int i = 0; i < slots.Length; i++)
      {
         slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
         slots[i].index = i;
         slots[i].inventory = this;
      }
      
   }

   void Update()
   {
      
   }

   void ClearSelctedItemSlot()
   {
      selectedItemName.text = string.Empty;
      selectedItemDescription.text = string.Empty;
      invenUI_Health.text = string.Empty;
      inven_HealthVelue.text = string.Empty;
      invenUI_Stamina.text = string.Empty;
      inven_StaminaVelue.text = string.Empty;
      
      useButton.SetActive(false);
      dropButton.SetActive(false);
   }


   public void ToggleInventory()
   {
      if (IsOpen())
      {
         inventorySlot.SetActive(false);
      }
      else
      {
         inventorySlot.SetActive(true);
      }
   }

   public bool IsOpen()
   {
      return inventorySlot.activeInHierarchy;
      
   }

   void AddItem()
   {
      ItemData data = CharacterManager.Instance.Player.itemData;

      if (data.canStack)
      {
         ItemSlot slot = GetItemStack(data);
         if (slot != null)
         {
            slot.quantity++;
            UpdateUI();
            CharacterManager.Instance.Player.itemData = null;
            return;
         }
         
      }

      ItemSlot emptyslot = GetEmptySlot();

      if (emptyslot != null)
      {
         emptyslot.item = data;
         emptyslot.quantity = 1;
         UpdateUI();
         CharacterManager.Instance.Player.itemData = null;
         return;
      }
      
      ThrowItem(data);
      
      CharacterManager.Instance.Player.itemData = null;

   }

   void UpdateUI()
   {
      for (int i = 0; i < slots.Length; i++)
      {
         if (slots[i].item != null)
         {
            slots[i].Set();
         }
         else
         {
            slots[i].Clear();
         }
      }
   }
   ItemSlot GetItemStack(ItemData data)
   {
      for (int i = 0; i < slots.Length; i++)
      {
         if (slots[i].item == data && slots[i].quantity > 0)
      }
      return null;
   }

   ItemSlot GetEmptySlot()
   {
      return null;
   }

   void ThrowItem(ItemData data)
   {
      
   }
}
