using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] ItemSO itemSO;
    [SerializeField] float itemPickUpCooldown;
    private Rigidbody2D rb2d;
    private float timer;
    private bool canBePicked;

    public ItemSO ItemSO => itemSO;
    public Rigidbody2D Rb2d => rb2d;
    public bool CanBePicked => canBePicked;



    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }


    private void Start()
    {
        StartPickUpCooldown();
    }


    private void StartPickUpCooldown()
    {
        timer = itemPickUpCooldown;
        canBePicked = false;
    }


    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            canBePicked = true;
        }
    }
}
