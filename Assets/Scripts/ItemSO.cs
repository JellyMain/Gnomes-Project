using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Create New Item")]
public class ItemSO : ScriptableObject
{
    [SerializeField] Sprite itemImage;
    [SerializeField] bool isStackable;
    public Sprite ItemImage => itemImage;
    public bool IsStackable => isStackable;
}
