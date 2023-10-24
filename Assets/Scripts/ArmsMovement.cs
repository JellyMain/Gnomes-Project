using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsMovement : MonoBehaviour
{
    [SerializeField] Transform leftArmTarget;
    [SerializeField] Transform rightArmTarget;
    [SerializeField] Transform rightArmGrip;
    [SerializeField] Transform leftArmGrip;
    [SerializeField] Transform weaponTransform;
    [SerializeField] float rotationSpeed = 5f;
    private Vector2 cursorPosition;


    private void MoveArms()
    {
        cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        leftArmTarget.position = leftArmGrip.position;
        rightArmTarget.position = cursorPosition;
    }


    private void RotateWeapon()
    {

        Vector2 direction = ((Vector3)cursorPosition - transform.position).normalized;


        if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }


        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        weaponTransform.rotation = Quaternion.Slerp(weaponTransform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }


    private void Update()
    {
        MoveArms();
        RotateWeapon();
    }



    public void SetArmGrips(Transform rightArmGrip, Transform leftArmGrip)
    {
        this.rightArmGrip = rightArmGrip;
        this.leftArmGrip = leftArmGrip;
    }

}
