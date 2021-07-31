using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1000)]
public class CameraFollow : MonoBehaviour
{
    private Transform player;
    private Vector3 cameraOffset;
    [Range(0.01f, 1.0f)]
    private float smoothness = 0.5f;

    void Start()
    {
        player = FindObjectOfType<PlayerController>().GetComponent<Transform>();
        cameraOffset = transform.position - player.transform.position;
    }

    void Update()
    {
        Vector3 newPos = player.position + cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPos, smoothness);
    }
}
