using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    public Transform player;
 
    void Update()
    {
        transform.position = new Vector3(player.position.x, 0, -20f);

    }
}
