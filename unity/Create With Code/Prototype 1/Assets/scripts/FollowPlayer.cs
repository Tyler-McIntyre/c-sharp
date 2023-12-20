using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset = new(0, 5, -7);

    void Start()
    {

    }

    private void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
