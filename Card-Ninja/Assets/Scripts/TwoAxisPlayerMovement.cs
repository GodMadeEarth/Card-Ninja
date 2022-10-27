using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoAxisPlayerMovement : MonoBehaviour
{

    public float speed = 5;

    Vector3 inputVect = Vector3.zero;
    Vector3 velocityVect = Vector3.zero;


    void FixedUpdate()
    {
        inputVect = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;


        /*if(Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            if(Mathf.Abs(inputVect.x) > Mathf.Abs(inputVect.y))
            {
                inputVect.x = inputVect.y;
            }
            else if(Mathf.Abs(inputVect.y) > Mathf.Abs(inputVect.x))
            {
                inputVect.y = inputVect.x;
            }

        }


        inputVect.Normalize();*/
        velocityVect = SmoothAxisMovement.Calculate(inputVect, velocityVect);
        
        transform.position += velocityVect * Time.deltaTime * speed;

        //Vector3 targetVect = transform.position + velocityVect * speed;


        //transform.position = Vector3.MoveTowards(transform.position, targetVect, speed * Time.deltaTime);
        //transform.position = Vector3.Lerp(transform.position, targetVect, Time.deltaTime);
    }
}
