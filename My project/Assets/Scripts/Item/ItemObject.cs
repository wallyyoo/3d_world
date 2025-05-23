

using UnityEngine;

public interface IInteractable
{
   public string GetInteractText();
   public void OnInteract();
   
}
public class ItemObject : MonoBehaviour, IInteractable
{
   public ItemData data;

   public string GetInteractText()
   {
      string str = $"{data.ItemName}\n{data.ItemDescription}";
      return str;
   }

   public void OnInteract()
   {
      CharacterManager.Instance.Player.itemData = data;
      CharacterManager.Instance.Player.addItem?.Invoke();
      Destroy(gameObject);
   }
}
