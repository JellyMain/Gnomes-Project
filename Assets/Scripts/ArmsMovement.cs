using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsMovement : MonoBehaviour
{
    [SerializeField] Transform weaponContainer;
    [SerializeField] Transform leftArmTarget;
    [SerializeField] Transform rightArmTarget;
    [SerializeField] float rotationSpeed = 5f;

    [SerializeField] GameObject leftArmSolver;
    [SerializeField] BoxCollider2D[] armColliders;
    [SerializeField] Rigidbody2D[] armRigidbodies;
    [SerializeField] HingeJoint2D[] armHingeJoints;

    private Transform leftArmGrip;
    private Transform weaponTransform;
    private Vector2 cursorPosition;
    private Vector2 lookDirection;

    public Vector2 LookDirection => lookDirection;



    private void OnEnable()
    {
        WeaponInventory.OnWeaponSet += SetArmGrips;
    }



    private void OnDisable()
    {
        WeaponInventory.OnWeaponSet -= SetArmGrips;
    }



    private void MoveArms()
    {
        cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (leftArmGrip != null)
        {
            leftArmTarget.position = leftArmGrip.position;
        }
        rightArmTarget.position = cursorPosition;
    }


    private void RotateWeapon()
    {

        lookDirection = ((Vector3)cursorPosition - transform.position).normalized;


        if (lookDirection.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }


        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        weaponTransform.rotation = Quaternion.Slerp(weaponTransform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }


    private void Update()
    {
        MoveArms();
        RotateWeapon();
    }


    private void ToggleLeftArmRagdoll(bool isLeftGripFree)
    {
        foreach (Collider2D collider in armColliders)
        {
            collider.enabled = isLeftGripFree;
        }
        foreach (Rigidbody2D rb2d in armRigidbodies)
        {
            rb2d.simulated = isLeftGripFree;
        }
        foreach (HingeJoint2D hingeJoint in armHingeJoints)
        {
            hingeJoint.enabled = isLeftGripFree;
        }
        leftArmSolver.SetActive(!isLeftGripFree);
    }


    public void SetArmGrips(Weapon weapon)
    {
        ToggleLeftArmRagdoll(weapon.LeftArmGrip == null);
        weaponTransform = weapon.transform;
        leftArmGrip = weapon.LeftArmGrip;
    }

}
