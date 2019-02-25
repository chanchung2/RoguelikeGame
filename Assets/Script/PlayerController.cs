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

    private float clampMaxX;
    private float clampMinX;
    private float clampMaxY;
    private float clampMinY;

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

    [SerializeField]
    private RoomManager roomManager;
    private PlayerHeart playerHeart;
    [SerializeField]
    private Cinemachine.CinemachineConfiner confiner;

    private GameObject miniMap;

    void Start()
    {
        lastPos = transform.position;
        playerHeart = FindObjectOfType<PlayerHeart>();
        confiner = FindObjectOfType<Cinemachine.CinemachineConfiner>();
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
        move.x = Mathf.Clamp(move.x, clampMinX, clampMaxX);
        move.y = Mathf.Clamp(move.y, clampMinY, clampMaxY);
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

    public void PlayerMapMove(int _x, int _y)
    {
        if (miniMap != null)
        {
            Destroy(miniMap);
        }
        float posX = roomManager.rooms[_x, _y].map.GetComponent<Map>().posY;
        float posY = roomManager.rooms[_x, _y].map.GetComponent<Map>().posX;
        float mapDistanceX = roomManager.mapDistanceX;
        float mapDistanceY = roomManager.mapDistanceY;

        clampMaxX = roomManager.rooms[_x, _y].map.GetComponent<Map>().clampMaxX;
        clampMinX = roomManager.rooms[_x, _y].map.GetComponent<Map>().clampMinX;
        clampMaxY = roomManager.rooms[_x, _y].map.GetComponent<Map>().clampMaxY;
        clampMinY = roomManager.rooms[_x, _y].map.GetComponent<Map>().clampMinY;

        //roomManager.rooms[_x, _y].map.gameObject.SetActive(true);

        transform.position = new Vector3(posX * mapDistanceX,-posY * mapDistanceY, 0.0f);

        confiner.m_BoundingShape2D = roomManager.rooms[_x, _y].map.GetComponent<Map>().collider;

        miniMap = Instantiate(roomManager.miniMapPlayerPos, roomManager.miniMap[_x,_y].transform.position, Quaternion.identity);
        miniMap.transform.parent = roomManager.parent;
        roomManager.minimapCamera.CameraMove(miniMap.transform);
        Debug.Log("x : " + _x + " y : " + _y);
    }

    public void PlayerMapMove(int _x, int _y, int _doorPos)
    {
        Debug.Log("x : " + _x + " y : " + _y);
        if (miniMap != null)
        {
            Destroy(miniMap);
        }
        float posX = roomManager.rooms[_x, _y].map.GetComponent<Map>().posY;
        float posY = roomManager.rooms[_x, _y].map.GetComponent<Map>().posX;
        float mapDistanceX = roomManager.mapDistanceX;
        float mapDistanceY = roomManager.mapDistanceY;

        clampMaxX = roomManager.rooms[_x, _y].map.GetComponent<Map>().clampMaxX;
        clampMinX = roomManager.rooms[_x, _y].map.GetComponent<Map>().clampMinX;
        clampMaxY = roomManager.rooms[_x, _y].map.GetComponent<Map>().clampMaxY;
        clampMinY = roomManager.rooms[_x, _y].map.GetComponent<Map>().clampMinY;

        transform.position = roomManager.rooms[_x, _y].map.GetComponent<Map>().door[_doorPos].transform.Find("SpawnPos").transform.position;

        confiner.m_BoundingShape2D = roomManager.rooms[_x, _y].map.GetComponent<Map>().collider;

        miniMap = Instantiate(roomManager.miniMapPlayerPos, roomManager.miniMap[_x, _y].transform.position, Quaternion.identity);     // 미니맵 이동 스크립트 새로만들기/..
        miniMap.transform.parent = roomManager.parent;
        roomManager.minimapCamera.CameraMove(miniMap.transform);
    }
}
