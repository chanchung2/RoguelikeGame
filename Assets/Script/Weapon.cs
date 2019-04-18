using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected float speed;
    protected float damage;

    [SerializeField] protected GameObject hitEffect;
    [SerializeField] protected GameObject hitDamageText;

}
