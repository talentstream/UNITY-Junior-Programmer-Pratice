using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset1 = new Vector3(0, 5, -7);
    private Vector3 offset2 = new Vector3(0, 1.75f, 1.25f);
    private int offsetFlag = 1;
    // Update is called once per frame
    void LateUpdate()
    {
        if(Input.GetKeyDown("space"))
        {
            offsetFlag *= -1;
        }
        if (offsetFlag == 1)
        {
            transform.position = player.transform.position + offset1;
        }
        else
        {
            transform.position = player.transform.position + offset2;
        }
    }
}
