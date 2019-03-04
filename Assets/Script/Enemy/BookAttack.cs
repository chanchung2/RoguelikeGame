using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookAttack : EnemyWeapon
{
    private PlayerController playerController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.MovePosition(transform.position + transform.right * speed * Time.smoothDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerHit(damage);
            Destroy(gameObject);
        }
    }

    protected override void playerHit(int _damage)
    {
        playerController.DecreaseHP(_damage);
    }
}
