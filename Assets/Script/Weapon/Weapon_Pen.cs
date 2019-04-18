using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Pen : Weapon
{
    private Rigidbody rigidody;

    // Start is called before the first frame update
    void Start()
    {
        rigidody = GetComponent<Rigidbody>();
        speed = PlayerController.WeaponSpeed;
        damage = 2;
    }

    // Update is called once per frame
    void Update()
    {
        rigidody.MovePosition(transform.position + transform.right * -1 * speed * Time.smoothDeltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy_Book>().DecreaseHP(damage);

            GameObject.Find("Player").GetComponent<PlayerController>().weaponHit(other.transform, damage);

            var cloneEffect = Instantiate(hitEffect, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z));
            Destroy(cloneEffect, 0.5f);
            Destroy(gameObject);
        }
    }
}
