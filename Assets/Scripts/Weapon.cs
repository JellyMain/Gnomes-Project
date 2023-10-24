using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Transform rightArmGrip;
    [SerializeField] Transform leftArmGrip;
    public Transform RightArmGrip => rightArmGrip;
    public Transform LeftArmGrip => leftArmGrip;



}
