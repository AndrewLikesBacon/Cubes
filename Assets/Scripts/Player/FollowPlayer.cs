using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        float x = player.position.x;
        float y = player.position.y;

        transform.position = new Vector2(x, y - .25f);
    }
}
