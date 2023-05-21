using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float cameraDistance = 3.3f;
    public float cameraHorizontalSpeed = 5f;
    public float cameraVerticalSpeed = 8f;
    
    private void Update()
    {
        var position = transform.position;
        var playerPosition = player.transform.position;
        var targetHorizontalPosition = new Vector3(playerPosition.x, position.y, playerPosition.z - cameraDistance);
        var targetVerticalPosition = new Vector3(position.x, Mathf.Max(playerPosition.y, 1), playerPosition.z - cameraDistance);
        position = Vector3.Lerp(position, targetHorizontalPosition, cameraHorizontalSpeed * Time.deltaTime);
        position = Vector3.Lerp(position, targetVerticalPosition, cameraVerticalSpeed * Time.deltaTime);
        transform.position = position;
    }
}