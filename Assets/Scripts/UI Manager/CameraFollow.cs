using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterMechanism.System;

[DefaultExecutionOrder(1000)]
public class CameraFollow : MonoBehaviour
{
    static readonly Vector3 CameraOffset = new Vector3(0f, 14.94f, -8.4f);
    static readonly Vector3 RotationEuler = new Vector3(57.051f, -1.262f, 0.003f);
    static readonly float FieldOfViewDefault = 40f;

    private Transform player;
    [Range(0.01f, 1.0f)]
    private float smoothness = 0.5f;

    private void Start()
    {
        this.GetComponent<Camera>().fieldOfView = FieldOfViewDefault;
        transform.position = CameraOffset + player.position;
        transform.rotation = Quaternion.Euler(RotationEuler);
    }

    void Update()
    {
        Vector3 newPosition = player.position + CameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPosition, smoothness);
    }

    static public CameraFollow Create(CharacterSystem characterSystem)
    {
        string nameCameraFlow = "Camera Flow - " + characterSystem.GetProfile.Name;
        GameObject cameraFollowObject = new GameObject(nameCameraFlow, typeof(Camera));
        cameraFollowObject.AddComponent<CameraFollow>().SetTransfomTarget(characterSystem.gameObject.transform);

        return cameraFollowObject.GetComponent<CameraFollow>();
    }

    private void SetTransfomTarget(Transform transform)
    {
        this.player = transform;
    }
}
