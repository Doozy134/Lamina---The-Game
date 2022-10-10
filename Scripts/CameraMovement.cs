using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float bufferDistance;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;


    private void Update()
    {
        //setting new position after every frame
        transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);

        //calculating the new value for extra distance to lookahead
        lookAhead = Mathf.Lerp(lookAhead, (bufferDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
    }
}
