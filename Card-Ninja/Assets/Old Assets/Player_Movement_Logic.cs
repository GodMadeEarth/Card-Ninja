using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement_Logic : MonoBehaviour
{
    Vector3 player_Movement_Vect = Vector3.zero;
    public float player_Speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Transforms WASD into a movement vector
        player_Movement_Vect = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            player_Movement_Vect.y++;
        }
        if (Input.GetKey(KeyCode.S))
        {
            player_Movement_Vect.y--;
        }
        if (Input.GetKey(KeyCode.D))
        {
            player_Movement_Vect.x++;
        }
        if (Input.GetKey(KeyCode.A))
        {
            player_Movement_Vect.x--;
        }
        player_Movement_Vect = player_Movement_Vect.normalized * player_Speed;

        transform.position += player_Movement_Vect * Time.deltaTime;
        
    }
}
