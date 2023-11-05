using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] int inventoryCapacity;
    [SerializeField] InventorySlot slotPrefab;
    [SerializeField] GameObject inventroyUI;
    [SerializeField] Transform slotsContainer;
    private List<InventorySlot> itemSlotsList;
    private bool isInventoryActive = false;


    private void Awake()
    {
        itemSlotsList = new List<InventorySlot>();
    }


    private void OnEnable()
    {
        GameInput.OnInventoryToggledAction += ToggleInventory;
    }


    private void OnDisable()
    {
        GameInput.OnInventoryToggledAction -= ToggleInventory;
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



    private void UpdateInventory()
    {
        foreach (InventorySlot slot in itemSlotsList)
        {
            slot.UpdateSlot();
        }
    }


    public void AddItem(ItemSO itemToAdd)
    {
        foreach (InventorySlot slot in itemSlotsList)
        {
            if (!slot.IsEmpty() && itemToAdd == slot.Item && itemToAdd.IsStackable && slot.StackCount < itemToAdd.MaxStackSize)
            {
                slot.StackItem();
                return;
            }

            if (slot.IsEmpty())
            {
                slot.SetItem(itemToAdd);
                return;
            }
        }
    }



    private void ToggleInventory()
    {
        isInventoryActive = !isInventoryActive;
        inventroyUI.SetActive(isInventoryActive);
        UpdateInventory();
    }

}
