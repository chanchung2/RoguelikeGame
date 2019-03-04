using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    private GameObject OpenDoor;
    [SerializeField]
    private GameObject CloseDoor;

    private void Update()
    {
        if (transform.parent.GetComponent<Map>().enemyCount == 0)
        {
            DoorOpen();
        }
    }

    private void DoorOpen()
    {
        CloseDoor.SetActive(false);
        OpenDoor.SetActive(true);
    }
}
