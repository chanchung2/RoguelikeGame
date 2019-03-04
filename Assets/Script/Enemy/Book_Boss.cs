using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book_Boss : Enemy
{
    private bool isAttack = false;

    [SerializeField]
    private GameObject attackWeapon;

    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TryAttack();
    }

    protected override void Attack()
    {
        StartCoroutine(BossAttackCoroutine());
    }

    protected override void TryAttack()
    {
        Attack();
    }

    IEnumerator BossAttackCoroutine()
    {
        if (!isAttack)
        {
            isAttack = true;

            yield return new WaitForSeconds(0.5f);

            while (true)
            {
                Instantiate(attackWeapon, transform.position, Quaternion.Euler(0, 0, -Mathf.Atan2(player.GetComponent<Transform>().position.x - transform.position.x,
                                                         player.GetComponent<Transform>().position.y - transform.position.y) * Mathf.Rad2Deg + 90.0f));
                count += 1;

                if (count == 20)
                {
                    break;
                }
                
                yield return new WaitForSeconds(0.2f);
            }

            count = 0;

            while (true)
            {
                Instantiate(attackWeapon, transform.position, Quaternion.Euler(0, 0, -Mathf.Atan2(player.GetComponent<Transform>().position.x - transform.position.x,
                                         player.GetComponent<Transform>().position.y - transform.position.y) * Mathf.Rad2Deg + 90.0f));

                count += 1;

                if (count == 80)
                {
                    break;
                }

                yield return new WaitForSeconds(0.05f);
            }

            yield return new WaitForSeconds(1.0f);

            for (int j = 0; j< 2; j++)
            {
                for (int i = 0; i < 360; i += 20)
                {
                    Instantiate(attackWeapon, transform.position, Quaternion.Euler(0, 0, i));
                }

                yield return new WaitForSeconds(0.7f);

                for (int i = 10; i < 360; i += 20)
                {
                    Instantiate(attackWeapon, transform.position, Quaternion.Euler(0, 0, i));
                }
                yield return new WaitForSeconds(0.7f);
            }

            isAttack = false;
        }
    }
}
