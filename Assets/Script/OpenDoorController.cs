using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorController : MonoBehaviour
{
    private Transform transform;
    private PlayerController playerController;

    private int myDirection;

    private int moveX;
    private int moveY;

    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        playerController = FindObjectOfType<PlayerController>();
        moveX = transform.parent.parent.GetComponent<Map>().posX;
        moveY = transform.parent.parent.GetComponent<Map>().posY;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            for (int i = 0; i < transform.parent.parent.GetComponent<Map>().door.Length; i++)
            {
                if (transform.parent.name == transform.parent.parent.GetComponent<Map>().door[i].name)
                {
                    playerController.roomManager.rooms[moveX, moveY].map.SetActive(false);
                    if (i == 0) // switch 문으로 바꾸기
                        playerController.PlayerMapMove(moveX, moveY - 1, 1);
                    else if (i == 1)
                        playerController.PlayerMapMove(moveX, moveY + 1, 0);
                    else if (i == 2)
                        playerController.PlayerMapMove(moveX - 1, moveY, 3);
                    else
                        playerController.PlayerMapMove(moveX + 1, moveY, 2);
                }
            }
        }
    }
}
