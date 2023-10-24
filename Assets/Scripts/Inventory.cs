using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] int inventoryCapacity;
    [SerializeField] InventorySlot slotPrefab;
    [SerializeField] Transform slotsContainer;
    private List<InventorySlot> itemSlotsList;


    private void Awake()
    {
        itemSlotsList = new List<InventorySlot>();
    }



    private void Start()
    {
        CreateInventorySlots();
    }


    private void CreateInventorySlots()
    {
        for (int i = 0; i < inventoryCapacity; i++)
        {
            InventorySlot spawnedSlot = Instantiate(slotPrefab, slotsContainer);
            itemSlotsList.Add(spawnedSlot);
        }
    }


    public bool hasEmptySlots()
    {
        foreach (InventorySlot slot in itemSlotsList)
        {
            if (slot.IsEmpty())
            {
                return true;
            }
        }
        return false;
    }


    public void AddItem(ItemSO itemToAdd)
    {
        foreach (InventorySlot slot in itemSlotsList)
        {
            if (slot.IsEmpty())
            {
                slot.SetItem(itemToAdd);
                break;
            }
        }
    }

}
