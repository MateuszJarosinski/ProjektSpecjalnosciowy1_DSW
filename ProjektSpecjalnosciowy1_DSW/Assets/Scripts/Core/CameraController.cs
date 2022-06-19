using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float offset;
    private void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y + offset, transform.position.z);
    }
}
