using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SmoothAxisMovement
{
    

    public static Vector3 Calculate(Vector3 targetVect,Vector3 velocityVect)
    {
        
        Vector3 velocityAngle = velocityVect.normalized;
        float velocitySpeed = Vector3.Distance(Vector3.zero, velocityVect);

        Vector3 targetAngle = targetVect.normalized;
        float targetSpeed = Vector3.Distance(Vector3.zero, targetVect);

        float rotateRate = 0.05f;
        float accelRate = 0.05f;
        float decelRate = 0.1f;

        //Vector3 newVelocityVect = velocityAngle * velocitySpeed;

        if (Diffrence(0, velocitySpeed) < .1f)
        {
            velocityAngle = targetAngle;
            
        }
        else 
        {
            velocityAngle += targetAngle * rotateRate;

        }

        if (targetSpeed > velocitySpeed)
        {
            velocitySpeed += accelRate;
        }
        else if(Diffrence(targetSpeed,velocitySpeed) < 0.1f)
        {
            velocitySpeed = targetSpeed;
        }
        else if (targetSpeed < velocitySpeed)
        {
            velocitySpeed -= decelRate;
        }
        Vector3 newVelocityVect = velocityAngle * velocitySpeed;

        return newVelocityVect;
    }

    static float Diffrence(float num1, float num2)
    {
        float cout;
        cout = Mathf.Max(num2, num1) - Mathf.Min(num1, num2);
        return cout;
    }
}
