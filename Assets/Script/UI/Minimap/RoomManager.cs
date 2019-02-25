using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private int roomXsize;
    [SerializeField] private int roomYsize;
    [SerializeField] private int roomNumber;

    [SerializeField] public float mapDistanceX;
    [SerializeField] public float mapDistanceY;

    [SerializeField] private GameObject[] room;
    [SerializeField] private GameObject map;
    [SerializeField] public Transform parent;
    [SerializeField] private Transform mapParent; // 필드맵 부모

    public GameObject miniMapPlayerPos;

    public MinimapCamera minimapCamera;

    private PlayerController playerController;

    public Room[,] rooms;    // 방에 대한 정보.
    public GameObject[,] miniMap; // 미니맵

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        RoomSetting();
        RoomTypeSetting();
        RoomCreate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RoomSetting()
    {
        rooms = new Room[roomXsize, roomYsize];
        miniMap = new GameObject[roomXsize, roomYsize];

        int count = 0;
        int direction = 0;

        int x = 0;
        int y = 0;

        for (x = 0; x < roomXsize; x++)
        {
            for (y = 0; y < roomYsize; y++)
            {
                rooms[x, y] = new Room(0, 0, 0);
            }
        }   // 룸 초기화

        x = Random.Range(0, roomXsize);
        y = Random.Range(0, roomYsize);

        rooms[x, y] = new Room(0, 0, ++count); // 시작지점. 첫번째방 지정

        while (roomNumber > count)
        {
            x = Random.Range(0, roomXsize);
            y = Random.Range(0, roomYsize);

            if (rooms[x, y].order != 0)
            {
                direction = Random.Range(0, 4);

                if ((Room.Direction)direction == Room.Direction.Left && y - 1 >= 0)
                {
                    if (rooms[x, y - 1].order == 0)
                    {
                        rooms[x, y - 1] = new Room(x, y, ++count);
                    }
                }
                else if ((Room.Direction)direction == Room.Direction.Up && x - 1 >= 0)
                {
                    if (rooms[x - 1, y].order == 0)
                    {
                        rooms[x - 1, y] = new Room(x, y, ++count);
                    }
                }
                else if ((Room.Direction)direction == Room.Direction.Right && y + 1 <= roomYsize - 1)
                {
                    if (rooms[x, y + 1].order == 0)
                    {
                        rooms[x, y + 1] = new Room(x, y, ++count);
                    }
                }
                else if ((Room.Direction)direction == Room.Direction.Down && x + 1 <= roomXsize - 1)
                {
                    if (rooms[x + 1, y].order == 0)
                    {
                        rooms[x + 1, y] = new Room(x, y, ++count);
                    }
                }
            }
        }

        for (x = 0; x < 5; x++)
        {
            Debug.Log(rooms[x, 0].order + "(" + rooms[x, 0].targetX + "," + rooms[x, 0].targetY + ")      " + rooms[x, 1].order + "(" + rooms[x, 1].targetX + "," + rooms[x, 1].targetY + ")      " + rooms[x, 2].order + "(" + rooms[x, 2].targetX + "," + rooms[x, 2].targetY + ")      " + rooms[x, 3].order + "(" + rooms[x, 3].targetX + "," + rooms[x, 3].targetY + ")      " + rooms[x, 4].order + "(" + rooms[x, 4].targetX + "," + rooms[x, 4].targetY + ")");
        }
            
        
    }

    private void RoomTypeSetting()
    {
        int x, y;
        int count = 1;
        int direction = 0;
        int designate = 0;

        while (roomNumber > count)
        {
            for (x = 0; x < roomXsize; x++)
            {
                for (y = 0; y < roomYsize; y++)
                {
                    if (rooms[x, y].order == count)
                    {
                        //Debug.Log(rooms[x, y].order + "     " + "(" + rooms[x, y].targetX + "," + rooms[x, y].targetY + ")");

                        if (y - 1 >= 0 && rooms[x, y - 1].order != 0) // 배열 예외처리
                        {
                            //Debug.Log("1. target x : " + rooms[x, y - 1].targetX + "     target y : " + rooms[x, y - 1].targetY + "    x : " + x + "    y : " + y);
                            if (rooms[x, y - 1].targetX == x && rooms[x, y - 1].targetY == y) // Left
                            {
                               // Debug.Log("ang1");
                                rooms[x, y].LeftType = true;
                                direction++;
                            }
                        }
                        if (x - 1 >= 0 && rooms[x - 1, y].order != 0)
                        {
                            //Debug.Log("2. target x : " + rooms[x-1, y].targetX + "     target y : " + rooms[x-1, y].targetY + "    x : " + x + "    y : " + y);
                            if (rooms[x - 1, y].targetX == x && rooms[x - 1, y].targetY == y) // Up
                            {
                               // Debug.Log("ang2");
                                rooms[x, y].UpType = true;
                                direction++;
                            }
                        }
                        if (y + 1 <= roomYsize - 1 && rooms[x, y + 1].order != 0)
                        {
                            //Debug.Log("3. target x : " + rooms[x, y +1].targetX + "     target y : " + rooms[x, y +1].targetY + "    x : " + x + "    y : " + y);
                            if (rooms[x, y + 1].targetX == x && rooms[x, y + 1].targetY == y) // Right
                            {
                              //  Debug.Log("ang3");
                                rooms[x, y].RightType = true;
                                direction++;
                            }
                        }
                        if (x + 1 <= roomXsize - 1 && rooms[x + 1, y].order != 0)
                        {
                            //Debug.Log("4. target x : " + rooms[x+1,y].targetX + "     target y : " + rooms[x+1, y].targetY + "    x : " + x + "    y : " + y);
                            if (rooms[x + 1, y].targetX == x && rooms[x + 1, y].targetY == y) // Down
                            {
                                rooms[x, y].DownType = true;
                                direction++;
                            }
                        }

                        if ((x - rooms[x, y].targetX) < 0 && rooms[x, y].order != 1)
                        {
                            rooms[x, y].DownType = true;
                        }
                        if ((x - rooms[x, y].targetX) > 0 && rooms[x, y].order != 1)
                        {
                            rooms[x, y].UpType = true;
                        }
                        if ((y - rooms[x, y].targetY) > 0 && rooms[x, y].order != 1)
                        {
                            rooms[x, y].LeftType = true;
                        }
                        if ((y - rooms[x, y].targetY) < 0 && rooms[x, y].order != 1)
                        {
                            rooms[x, y].RightType = true;
                        }

                        if (direction == 0)
                        {
                            if (y - 1 >= 0)
                            {
                                if (rooms[x, y].targetX == x && rooms[x, y].targetY == y - 1)
                                {
                                    rooms[x, y].LeftType = true;
                                }
                            }
                            if (x - 1 >= 0)
                            {
                                if (rooms[x, y].targetX == x - 1 && rooms[x, y].targetY == y)
                                {
                                    rooms[x, y].UpType = true;
                                }
                            }
                            if (y + 1 <= roomYsize - 1)
                            {
                                if (rooms[x, y].targetX == x && rooms[x, y].targetY == y + 1)
                                {
                                    rooms[x, y].RightType = true;
                                }
                            }
                            if (x + 1 <= roomXsize - 1)
                            {
                                if (rooms[x, y].targetX == x + 1 && rooms[x, y].targetY == y)
                                {
                                    rooms[x, y].DownType = true;
                                }
                            }
                        }
                        Debug.Log(" Left : " + rooms[x, y].LeftType + "      Right : " + rooms[x, y].RightType + "          Up : " + rooms[x, y].UpType + "           Down : " + rooms[x, y].DownType);
                        designate = RoomDesignate(rooms[x, y].LeftType, rooms[x, y].RightType, rooms[x, y].UpType, rooms[x, y].DownType);
                        Debug.Log("order : "+ rooms[x,y].order + " designate : " +(Room.RoomType)designate);
                        rooms[x, y].roomType = (Room.RoomType)designate;
                        x = 0;
                        y = 0;
                        count++;
                        direction = 0;
                    }
                }
            }
        }
    }

    private int RoomDesignate(bool _left, bool _right, bool _up, bool _down)
    {
        int value = 0;

        int left = _left ? 8 : 0;
        int right = _right ? 4 : 0;
        int up = _up ? 2 : 0;
        int down = _down ? 1 : 0;

        value = left + right + up + down;

        return value;
    }

    private void RoomCreate()
    {
        int roomType;
        int count = 1;
        float positionX = 1.593f;  // sprite 크기
        float positionY = 1.593f; ;

        for (int x = 0; x < roomXsize; x++)
        {
            positionY = 1.593f * -(x + 1);
            for (int y = 0; y < roomYsize; y++)
            {
                if (rooms[x, y].roomType != Room.RoomType.NULL)
                {
                    positionX = 1.593f * (y + 1);

                    roomType = (int)rooms[x, y].roomType - 1;
                    //Debug.Log("num : " + rooms[x,y].order + "       " + rooms[x, y].roomType);

                    miniMap[x,y] = Instantiate(room[roomType], new Vector3(positionX, positionY, 0.0f), Quaternion.identity);
                    MapCreate(x,y);
                    if (rooms[x,y].order == 1) // 첫 맵 초기화.
                    {
                        playerController.PlayerMapMove(x, y);
                    }
                    miniMap[x,y].transform.parent = parent;
                    count++;
                }
            }
        }
    }

    private void MapCreate(int _x, int _y)
    {
        rooms[_x,_y].map = Instantiate(map, new Vector3(_y * mapDistanceX, -_x * mapDistanceY, 0f), Quaternion.identity);
        rooms[_x, _y].map.GetComponent<Map>().posX = _x;
        rooms[_x, _y].map.GetComponent<Map>().posY = _y;
        rooms[_x, _y].map.GetComponent<Map>().roomType = rooms[_x, _y].roomType;
        rooms[_x, _y].map.transform.parent = mapParent;
        //[_x, _y].map.gameObject.SetActive(false);
    }
}
