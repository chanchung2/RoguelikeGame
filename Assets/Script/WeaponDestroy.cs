using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDestroy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag != "Player")
        {
            Destroy(other.gameObject);
        }
    }
}
