using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Pen : MonoBehaviour
{
    private float Speed;

    private Rigidbody rigidody;

    // Start is called before the first frame update
    void Start()
    {
        rigidody = GetComponent<Rigidbody>();
        Speed = PlayerController.WeaponSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.rotation.z == 0)
        {
            rigidody.MovePosition(transform.position + Vector3.left * Speed * Time.smoothDeltaTime);
        }
        else if (transform.rotation.z == 180)
        {
            rigidody.MovePosition(transform.position + Vector3.right * Speed * Time.smoothDeltaTime);
        }
        else if (transform.rotation.z == 90)
        {
            rigidody.MovePosition(transform.position + Vector3.up * Speed * Time.smoothDeltaTime);
        }
        else
        {
            rigidody.MovePosition(transform.position + Vector3.down * Speed * Time.smoothDeltaTime);
        }
    }
}
