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
        rigidody.MovePosition(transform.position + transform.right * -1 * Speed * Time.smoothDeltaTime);
    }
}
