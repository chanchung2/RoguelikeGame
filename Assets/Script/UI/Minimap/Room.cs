using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public enum RoomType { NULL, B, T, TB, R, RB, TR, TRB, L, LB, TL, TLB, LR, LBR, LTR, LTRB };
    public enum Direction { Left, Up, Right, Down };

    public RoomType roomType = RoomType.NULL;

    public bool LeftType = false;
    public bool UpType = false;
    public bool RightType = false;
    public bool DownType = false;

    public int targetX;
    public int targetY;
    public int order = 0;

    public GameObject map;

    public Room(int x, int y, int order)
    {
        targetX = x;
        targetY = y;
        this.order = order;
    }
}
