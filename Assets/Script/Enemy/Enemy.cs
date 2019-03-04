using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    protected float attackDistance;

    [SerializeField]
    protected float hp;

    protected GameObject player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    public void DecreaseHP(float _damage)
    {
        hp -= _damage;

        if (hp <= 0)
        {
            Debug.Log("파괴");
            transform.parent.parent.parent.GetComponent<Map>().enemyCount -= 1;
            Destroy(gameObject);
        }
    }

    protected abstract void TryAttack();
    protected abstract void Attack();
}
