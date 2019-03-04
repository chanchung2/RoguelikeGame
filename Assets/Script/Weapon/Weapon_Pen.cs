using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Pen : MonoBehaviour
{
    private float Speed;

    private Rigidbody rigidody;

    private float damage = 2;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy_Book>().DecreaseHP(damage);
            Destroy(gameObject);
        }
    }
}
