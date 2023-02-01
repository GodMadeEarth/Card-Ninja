using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TwoAxisPlayerMovement : MonoBehaviour
{

    public float speed = 5;

    Vector3 inputVect = Vector3.zero;
    Vector3 velocityVect = Vector3.zero;


    void FixedUpdate()
    {
        inputVect = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        inputVect += EdgeAvoidance(0.9f) * 1.1f;
        
        velocityVect = SmoothAxisMovement.Calculate(inputVect, velocityVect);
        
        transform.position += velocityVect * Time.deltaTime * speed;

        
    }

    Vector3 EdgeAvoidance(float bufferVal)
    {
        Vector3 correctionVector = Vector3.zero;

        if (Screen.width / 62.7 * bufferVal < math.abs(transform.position.x))
        {
            correctionVector.x = -transform.position.x;
        }
        if (Screen.height / 62.7 * bufferVal < math.abs(transform.position.y))
        {
            correctionVector.y = -transform.position.y;
        }

        return correctionVector.normalized;
    }

}
