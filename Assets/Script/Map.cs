using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Room.RoomType roomType;

    public float clampMaxX;
    public float clampMinX;
    public float clampMaxY;
    public float clampMinY;

    public int enemyCount;

    private bool leftDoor = false;
    private bool rightDoor = false;
    private bool upDoor = false;
    private bool downDoor = false;

    public int posX; // 맵 위치
    public int posY;

    public GameObject[] door;
    public PolygonCollider2D collider;
    private RoomManager room;

    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject enemyBoss;

    void Awake()
    {
        enemyCount = Random.Range(1, 10);

        collider = GetComponent<PolygonCollider2D>();
        room = FindObjectOfType<RoomManager>();

        clampMaxX = transform.position.x + 17.5f;
        clampMinX = transform.position.x - 17.5f;
        clampMaxY = transform.position.y + 14.0f;
        clampMinY = transform.position.y - 13.0f;
    }

    private void Start()
    {
        DoorReset();

        if (room.rooms[posX, posY].bossRoom == true)
        {
            Instantiate(enemyBoss, transform.position, Quaternion.identity);
        }
        else
        {
            if (room.rooms[posX, posY].order != 1 && room.rooms[posX, posY].specialRoom != true && room.rooms[posX, posY].bossRoom != true)
            {
                EnemySetting();
            }
            else
            {
                enemyCount = 0;
            }
        }
    }

    private void Update()
    {
        if (enemyCount == 0)
        {
            //DoorOpen();
        }
    }

    private void DoorReset()
    {
        int doorCheck = (int)roomType;

        //Debug.Log("doorCheck : " + doorCheck);
        //Debug.Log(doorCheck / 8);
        if (doorCheck / 8 == 1)
        {
            leftDoor = true;
            door[0].gameObject.SetActive(true);
            doorCheck %= 8;
        }
        //Debug.Log(doorCheck / 4);
        if (doorCheck / 4 == 1)
        {
            //Debug.Log("ang");
            rightDoor = true;
            door[1].gameObject.SetActive(true);
            doorCheck %= 4;
        }
        //Debug.Log(doorCheck / 2);
        if (doorCheck / 2 == 1)
        {
            upDoor = true;
            door[2].gameObject.SetActive(true);
            doorCheck %= 2;
        }
        //Debug.Log(doorCheck / 1);
        if (doorCheck / 1 == 1)
        {
            downDoor = true;
            door[3].gameObject.SetActive(true);
        }
    }

    private void EnemySetting()
    {
        int temp;

        for (int i = 0; i < enemyCount; i++)
        {
            while (true)
            {
                temp = Random.Range(0, 9);
                if (transform.Find("Spawn").GetChild(temp).gameObject.activeSelf == false)
                {
                    transform.Find("Spawn").GetChild(temp).gameObject.SetActive(true);
                    var clone = Instantiate(enemy, transform.Find("Spawn").GetChild(temp).transform.position, Quaternion.identity);
                    clone.transform.parent = transform.Find("Spawn").GetChild(temp);
                    break;
                }
            }
        }
    }
}
