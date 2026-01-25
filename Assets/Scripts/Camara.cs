using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;

    private Vector3 offset;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        transform.position = player.position + offset;
    }
}
