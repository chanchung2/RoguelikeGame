using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Heart : MonoBehaviour
{
    private Rigidbody rigidbody;
    private Transform transform;
    private PlayerController playerController;

    private Vector3 originPos;

    [SerializeField]
    private float upSpeed;
    [SerializeField]
    private float rotateSpeed;

    [SerializeField]
    private Item item;

    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody>();
        playerController = FindObjectOfType<PlayerController>();

        originPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (originPos.y - transform.position.y >= 0.01)
        {
            rigidbody.AddForce(Vector3.up * upSpeed * Time.smoothDeltaTime);
        }

        transform.Rotate(Vector3.up * rotateSpeed * Time.smoothDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (item.itemName == "Heart")
            {
                playerController.IncreaseHP(2);
                Destroy(gameObject);
            }
            else if (item.itemName == "Half_Heart")
            {
                playerController.IncreaseHP(1);
                Debug.Log("ang");
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("체력회복 error");
            }
        }
    }
}
