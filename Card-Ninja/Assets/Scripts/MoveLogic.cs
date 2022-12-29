using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class MoveLogic : MonoBehaviour
{
    GameObject targetObject;
    Vector3 targetVector = Vector3.zero;
    Vector3 velocityVector = Vector3.zero;
    float totalWeights = 0;

    public BasicSettings Basic = new BasicSettings();

    public List<LineMoveSettings> Aproach;

    public List<LineMoveSettings> Flee;

    public List<CircleMoveSettings> Circle;



    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (var aproach in Aproach)
        {
            if (Basic.enabled && aproach.enabled)
            {
                if (aproach.localTarget == string.Empty)
                {
                    targetObject = ClosestObject(GameObject.FindGameObjectsWithTag(Basic.globalTarget), gameObject);
                }
                else
                {
                    targetObject = ClosestObject(GameObject.FindGameObjectsWithTag(aproach.localTarget), gameObject);
                }
                if (aproach.Advanced.enabled)
                {
                    if (PassedBaseline(aproach.Advanced.activeAbove, aproach.Advanced.baseline, aproach.Advanced.triggerInactive, aproach.Advanced.triggeredBehaviorId))
                    {
                        targetVector += (targetObject.transform.position - gameObject.transform.position).normalized * aproach.strength;
                        totalWeights += aproach.strength;
                    }
                }
                else
                {
                    targetVector += (targetObject.transform.position - gameObject.transform.position).normalized * aproach.strength;
                    totalWeights += aproach.strength;
                }


            }
        }
        foreach (var flee in Flee)
        {
            if (Basic.enabled && flee.enabled)
            {
                if (flee.localTarget == string.Empty)
                {
                    targetObject = ClosestObject(GameObject.FindGameObjectsWithTag(Basic.globalTarget), gameObject);
                }
                else
                {
                    targetObject = ClosestObject(GameObject.FindGameObjectsWithTag(flee.localTarget), gameObject);
                }

                if (flee.Advanced.enabled)
                {
                    if (PassedBaseline(flee.Advanced.activeAbove, flee.Advanced.baseline, flee.Advanced.triggerInactive, flee.Advanced.triggeredBehaviorId))
                    {
                        targetVector += -(targetObject.transform.position - gameObject.transform.position).normalized * flee.strength;
                        totalWeights += flee.strength;
                    }
                }
                else
                {
                    targetVector += -(targetObject.transform.position - gameObject.transform.position).normalized * flee.strength;
                    totalWeights += flee.strength;
                }

            }
        }
        foreach (var circle in Circle)
        {
            if (Basic.enabled && circle.enabled)
            {
                if (circle.localTarget == string.Empty)
                {
                    targetObject = ClosestObject(GameObject.FindGameObjectsWithTag(Basic.globalTarget), gameObject);
                }
                else
                {
                    targetObject = ClosestObject(GameObject.FindGameObjectsWithTag(circle.localTarget), gameObject);
                }

                if (circle.Advanced.enabled)
                {
                    if (PassedBaseline(circle.Advanced.activeAbove, circle.Advanced.baseline, circle.Advanced.triggerInactive, circle.Advanced.triggeredBehaviorId))
                    {
                        if (circle.clockwise)
                        {
                            targetVector += Quaternion.Euler(0, 0, 75) * (targetObject.transform.position - gameObject.transform.position).normalized * circle.strength;
                            totalWeights += circle.strength;
                        }
                        else
                        {
                            targetVector += Quaternion.Euler(0, 0, -75) * (targetObject.transform.position - gameObject.transform.position).normalized * circle.strength;
                            totalWeights += circle.strength;
                        }
                    }

                }
                else
                {
                    if (circle.clockwise)
                    {
                        targetVector += Quaternion.Euler(0, 0, 75) * (targetObject.transform.position - gameObject.transform.position).normalized * circle.strength;
                        totalWeights += circle.strength;
                    }
                    else
                    {
                        targetVector += Quaternion.Euler(0, 0, -75) * (targetObject.transform.position - gameObject.transform.position).normalized * circle.strength;
                        totalWeights += circle.strength;
                    }
                }



            }
        }



        if (totalWeights != 0 && targetVector != Vector3.zero)
        {
            targetVector = targetVector / totalWeights;
            
            totalWeights = 0;
        }

        targetVector += EdgeAvoidance(0.9f) * 1.1f;

        velocityVector = SmoothAxisMovement.Calculate(targetVector, velocityVector);
        transform.position += velocityVector * Basic.speed * Time.deltaTime;

        targetVector = Vector3.zero;

        

    }
    GameObject ClosestObject(GameObject[] TargetedObjects, GameObject localObject)
    {
        GameObject closestTargetObject = gameObject;
        foreach (var currentTargetObject in TargetedObjects)
        {
            if (closestTargetObject.GetInstanceID() == localObject.GetInstanceID())
            {
                closestTargetObject = currentTargetObject;
            }
            if (currentTargetObject.GetInstanceID() != localObject.GetInstanceID() && Vector3.Distance(currentTargetObject.transform.position, localObject.transform.position) < Vector3.Distance(closestTargetObject.transform.position, localObject.transform.position))
            {
                closestTargetObject = currentTargetObject;
            }
        }
        return closestTargetObject;
    }

    bool PassedBaseline(bool activeAbove,float baseline,bool triggerInactive,int triggeredBehaviorId)
    {
        if (activeAbove == true && Vector3.Distance(targetObject.transform.position, gameObject.transform.position) > baseline)
        {
            return true;
        }
        else if (activeAbove == false && Vector3.Distance(targetObject.transform.position, gameObject.transform.position) < baseline)
        {
            return true;
        }
        else if (triggerInactive)
        {
            foreach (MoveLogic component in gameObject.GetComponents(typeof(MoveLogic)))
            {
                if (component.Basic.behaviorId == triggeredBehaviorId)
                {
                    component.Basic.enabled = true;
                    Basic.enabled = false;
                    break;
                }
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    Vector3 EdgeAvoidance(float bufferVal)
    {
        Vector3 correctionVector = Vector3.zero;

        if (Screen.width / 62.7 * bufferVal < math.abs(transform.position.x)  )
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
  