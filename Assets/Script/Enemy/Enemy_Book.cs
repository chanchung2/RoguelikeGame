using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Book : Enemy
{
    private bool isAttack = false;

    [SerializeField]
    private float attackSpeed;
    [SerializeField]
    private GameObject attackWeapon;

    //[SerializeField]
    //private GameObject attackBall;

    // Update is called once per frame
    void Update()
    {
        TryAttack();
    }

    protected override void TryAttack()
    {
        //Debug.Log(transform.position);
        //Debug.Log(playerPos.position);
        if (Vector3.Distance(transform.position, player.GetComponent<Transform>().position) < attackDistance)
        {
            Attack();
        }
    }
    protected override void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        if (!isAttack)
        {
            isAttack = true;

            Instantiate(attackWeapon, transform.position, Quaternion.Euler(0, 0, -Mathf.Atan2(player.GetComponent<Transform>().position.x - transform.position.x,
                                                                                             player.GetComponent<Transform>().position.y - transform.position.y) * Mathf.Rad2Deg + 90.0f));

            yield return new WaitForSeconds(attackSpeed);

            isAttack = false;
        }
    }
}
