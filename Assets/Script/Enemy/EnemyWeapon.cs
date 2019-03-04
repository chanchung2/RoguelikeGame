using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyWeapon : MonoBehaviour
{
    [SerializeField]
    protected float speed;

    [SerializeField]
    protected int damage;

    [SerializeField]
    protected Transform transform;

    [SerializeField]
    protected Rigidbody rigidbody;

    protected abstract void playerHit(int _damage);
}
