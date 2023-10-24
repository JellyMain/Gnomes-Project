using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] GameObject SlotImageObject;
    private Image SlotImage;
    private ItemSO item;


    private void Awake()
    {
        SlotImage = SlotImageObject.GetComponent<Image>();
    }


    public void SetItem(ItemSO newItem)
    {
        this.item = newItem;
        SlotImageObject.SetActive(true);
        SlotImage.sprite = item.ItemImage;
    }


    public bool IsEmpty()
    {
        return item == null;
    }
}
