using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Rigidbody weaponRb;
    // Start is called before the first frame update
    void Start()
    {
        weaponRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(weaponRb.velocity == Vector3.zero || transform.position.y < -1)
        {
            Destroy(gameObject);
        }
    }
}
