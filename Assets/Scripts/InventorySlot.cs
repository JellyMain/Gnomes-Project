using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject slotImageObject;
    [SerializeField] TMP_Text quantityText;
    [SerializeField] float pushForce = 5;
    private Sprite slotItemSprite;
    private Image slotImage;
    private ItemSO item;
    private int stackCount = 1;

    public int StackCount => stackCount;
    public ItemSO Item => item;


    private void Awake()
    {
        slotImage = slotImageObject.GetComponent<Image>();
    }


    public void SetItem(ItemSO newItem)
    {
        this.item = newItem;
        slotItemSprite = item.ItemImage;
    }



    private void DropItem()
    {
        if (stackCount > 1)
        {
            stackCount--;
            ThrowDropedItem();
            UpdateSlot();
        }
        else
        {
            ThrowDropedItem();
            ClearSlot();
        }
    }


    private void ThrowDropedItem()
    {
        Item spawnedItem = Instantiate(item.ItemPrefab, Gnome.Instance.transform.position, Quaternion.identity);
        spawnedItem.Rb2d.AddForce(Gnome.Instance.ArmsMovement.LookDirection * pushForce, ForceMode2D.Impulse);
    }


    private void ClearSlot()
    {
        item = null;
        UpdateSlot();
    }


    public void StackItem()
    {
        stackCount++;
    }


    public void UpdateSlot()
    {
        slotImageObject.SetActive(!IsEmpty());
        quantityText.gameObject.SetActive(!IsEmpty());
        quantityText.text = stackCount.ToString();
        slotImage.sprite = slotItemSprite;
    }


    public bool IsEmpty()
    {
        return item == null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            DropItem();
        }
    }
}
