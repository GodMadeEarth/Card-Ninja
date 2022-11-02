using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement_Logic : MonoBehaviour
{
    Vector3 enemy_Movement_Vect = Vector3.zero;
    public float speed = 3;
    //public float distance_From_Player = 2;
    //public float forward_Step_Strength = 1;
    //public float side_Step_Strength = 1;

    Vector3 targetVect = Vector3.zero;
    Vector3 velocityVect = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Calculates the enemy's movement vector logic
        enemy_Movement_Vect = Vector3.zero;
        
        Vector3 player_Positon = GameObject.Find("Player").transform.position;

        targetVect += player_Positon - transform.position;


        //Vector3 enemy_Side_Step_Vect = getRotateAroundOffset(player_Positon,transform.position,10).normalized * enemy_Side_Step_Strength;

        //Vector3 enemy_Forward_Step_Vect = (player_Positon - transform.position).normalized * enemy_Forward_Step_Strength;

        //enemy_Movement_Vect = (enemy_Side_Step_Vect + enemy_Forward_Step_Vect).normalized * enemy_Speed;

        //transform.position += enemy_Movement_Vect * Time.deltaTime;

        Debug.LogError(targetVect.normalized);

        //velocityVect = SmoothAxisMovement.Calculate(targetVect.normalized, velocityVect);

        transform.position += targetVect.normalized * Time.deltaTime * speed;

    }

    Vector3 getRotateAroundOffset(Vector3 target_Pos, Vector3 current_Pos,float deg)
    {

        transform.RotateAround(target_Pos, Vector3.forward, deg);

        Vector3 side_Step_Vect = transform.position - current_Pos;

        transform.RotateAround(target_Pos, Vector3.forward, -deg);

        return side_Step_Vect;

    }
}
