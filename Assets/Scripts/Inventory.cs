using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] int inventoryCapacity;
    [SerializeField] GameInventorySlot slotPrefab;
    [SerializeField] GameObject inventroyUI;
    [SerializeField] Transform slotsContainer;
    private List<GameInventorySlot> itemSlotsList;
    private bool isInventoryActive = false;


    private void Awake()
    {
        itemSlotsList = new List<GameInventorySlot>();
    }


    private void OnEnable()
    {
        GameInput.OnGnomeInventoryToggledAction += ToggleInventory;
    }


    private void OnDisable()
    {
        GameInput.OnGnomeInventoryToggledAction -= ToggleInventory;
    }


    private void Start()
    {
        CreateInventorySlots();
    }


    private void CreateInventorySlots()
    {
        for (int i = 0; i < inventoryCapacity; i++)
        {
            GameInventorySlot spawnedSlot = Instantiate(slotPrefab, slotsContainer);
            itemSlotsList.Add(spawnedSlot);
        }
    }


    public bool hasEmptySlots()
    {
        foreach (GameInventorySlot slot in itemSlotsList)
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
        foreach (GameInventorySlot slot in itemSlotsList)
        {
            slot.UpdateSlot();
        }
    }


    public void AddItem(ItemSO itemToAdd)
    {
        foreach (GameInventorySlot slot in itemSlotsList)
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
