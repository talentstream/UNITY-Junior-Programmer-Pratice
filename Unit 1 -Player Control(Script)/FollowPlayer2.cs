using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer2 : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset = new Vector3(0, 5, -7);
    private int offsetFlag = 1;
    // Update is called once per frame
    void LateUpdate()
    {
            transform.position = player.transform.position + offset;
    }
}
