using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
   public ItemSlot[] slots;
   
   public GameObject inventorySlot;
   public Transform slotPanel;
   public Transform dropposition;

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
      dropposition = CharacterManager.Instance.Player.dropPosition;
      
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
         if (slots[i].item == data && slots[i].quantity < data.maxStack)
         {
            return slots[i];
         }
      }
      return null;
   }

   ItemSlot GetEmptySlot()
   {
      for (int i = 0; i < slots.Length; i++)
      {
         if (slots[i].item == null)
         {
            return slots[i];
         }
      }
      return null;
   }

   void ThrowItem(ItemData data)
   {
      Instantiate(data.dropPrefab, dropposition.position, Quaternion.Euler(Vector3.one * Random.value * 360));
   }

   private ItemSlot selectedSlot;

   public void SelectItem(ItemSlot slot)
   {
      selectedSlot = slot;
      
      selectedItemName.text = selectedSlot.item.ItemName;
      selectedItemDescription.text = selectedSlot.item.ItemDescription;

      invenUI_Health.text = " ";
      inven_HealthVelue.text = " ";
      invenUI_Stamina.text = " ";
      inven_StaminaVelue.text = " ";

      foreach (var con in slot.item.consumables)
      {
         if (con.Type == ConsumableType.Health)
         {
            invenUI_Health.text = "채력 회복";
            inven_HealthVelue.text = con.value.ToString();
         }
         else if (con.Type == ConsumableType.Stamina)
         {
            invenUI_Stamina.text = "스테미나 회복";
            inven_StaminaVelue.text = con.value.ToString();
         }
      }
      
      useButton.SetActive(slot.item.canConsume);
      dropButton.SetActive(true);
   }

   public void UseItem()
   {
      if(selectedSlot == null || !selectedSlot.item.canConsume)return;

      foreach (var con in selectedSlot.item.consumables)
      {
         if (con.Type == ConsumableType.Health)
            condition.HealHeath(con.value);
         else if (con.Type == ConsumableType.Stamina)
            condition.HealStamina(con.value);
      }
      
      selectedSlot.quantity--;
      if (selectedSlot.quantity <= 0)
      {
         selectedSlot.Clear();
      }
      UpdateUI();
      ClearSelctedItemSlot();
   }

}

