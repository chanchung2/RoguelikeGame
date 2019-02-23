using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isAttack = false;

    public int maxHp;
    public int currentHp;

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float attackSpeed;
    [SerializeField]
    public static float WeaponSpeed = 30; // 투사체 속도

    private float h;
    private float v;

    [SerializeField]
    private Rigidbody rigidbody;

    private Vector3 lastPos;

    [SerializeField]
    private Transform leftPos;
    [SerializeField]
    private Transform rightPos;
    [SerializeField]
    private Transform upPos;
    [SerializeField]
    private Transform downPos;

    [SerializeField]
    private Item player_Weapon;

    private MapController theMapController;
    private PlayerHeart playerHeart;

    void Start()
    {
        lastPos = transform.position;
        theMapController = FindObjectOfType<MapController>();
        playerHeart = FindObjectOfType<PlayerHeart>();
    }

    void Update()
    {
        Move();
        TryAttack();
    }

    private void Move()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 movePos = new Vector3(h, v, 0f);
        var move = transform.position + movePos * moveSpeed * Time.smoothDeltaTime;
        move.x = Mathf.Clamp(move.x, -theMapController.sizeX, theMapController.sizeX);
        move.y = Mathf.Clamp(move.y, -theMapController.sizeY, theMapController.sizeY + 1.3f);

        rigidbody.MovePosition(move);
    }

    private void TryAttack()
    {
        if (!isAttack)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                StartCoroutine(AttackCoroutine(leftPos, Quaternion.Euler(0, 0, 0)));
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                StartCoroutine(AttackCoroutine(rightPos, Quaternion.Euler(0, 0, 180)));
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                StartCoroutine(AttackCoroutine(upPos, Quaternion.Euler(0, 0, 270)));
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                StartCoroutine(AttackCoroutine(downPos, Quaternion.Euler(0, 0, 90)));
            }
        }
    }

    public void IncreaseHP(int _hp)
    {
        currentHp += _hp;

        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
        playerHeart.HeartReset();
    }

    IEnumerator AttackCoroutine(Transform pos, Quaternion qua)
    {
        isAttack = true;

        var clone = Instantiate(player_Weapon.itemPrefab, pos.position, qua);

        yield return new WaitForSeconds(attackSpeed);

        isAttack = false;
    }
}
