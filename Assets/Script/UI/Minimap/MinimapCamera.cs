using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    public void CameraMove(Transform PlayerPos)
    {
        transform.position = PlayerPos.position;
    }
}
