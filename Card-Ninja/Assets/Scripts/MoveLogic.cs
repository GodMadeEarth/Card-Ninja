using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLogic : MonoBehaviour
{
    GameObject targetObject;
    Vector3 targetVector = Vector3.zero;
    Vector3 velocityVector = Vector3.zero;
    float totalWeights = 0;

    public BasicSettings Basic = new BasicSettings();

    public LineMoveSettings Aproach = new LineMoveSettings();

    public LineMoveSettings Flee = new LineMoveSettings();

    public CircleMoveSettings Circle = new CircleMoveSettings();

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Basic.enabled && Aproach.enabled)
        {
            if (Aproach.localTarget == string.Empty)
            {
                targetObject = ClosestObject(GameObject.FindGameObjectsWithTag(Basic.globalTarget), gameObject);
            }
            else
            {
                targetObject = ClosestObject(GameObject.FindGameObjectsWithTag(Aproach.localTarget), gameObject);
            }
            if (Aproach.Advanced.enabled)
            {
                if (PassedBaseline(Aproach.Advanced.activeAbove, Aproach.Advanced.baseline, Aproach.Advanced.triggerInactive, Aproach.Advanced.triggeredBehaviorId))
                {
                    targetVector += (targetObject.transform.position - gameObject.transform.position).normalized * Aproach.strength;
                    totalWeights += Aproach.strength;
                }        
            }
            else
            {
                targetVector += (targetObject.transform.position - gameObject.transform.position).normalized * Aproach.strength;
                totalWeights += Aproach.strength;
            }
            

        }
        if (Basic.enabled && Flee.enabled)
        {
            if (Flee.localTarget == string.Empty)
            {
                targetObject = ClosestObject(GameObject.FindGameObjectsWithTag(Basic.globalTarget), gameObject);
            }
            else
            {
                targetObject = ClosestObject(GameObject.FindGameObjectsWithTag(Flee.localTarget), gameObject);
            }

            if (Flee.Advanced.enabled)
            {
                if (PassedBaseline(Flee.Advanced.activeAbove, Flee.Advanced.baseline, Flee.Advanced.triggerInactive, Flee.Advanced.triggeredBehaviorId))
                {
                    targetVector += -(targetObject.transform.position - gameObject.transform.position).normalized * Flee.strength;
                    totalWeights += Flee.strength;
                }
            }
            else
            {
                targetVector += -(targetObject.transform.position - gameObject.transform.position).normalized * Flee.strength;
                totalWeights += Flee.strength;
            }

        }
        if (Basic.enabled && Circle.enabled)
        {
            if (Circle.localTarget == string.Empty)
            {
                targetObject = ClosestObject(GameObject.FindGameObjectsWithTag(Basic.globalTarget), gameObject);
            }
            else
            {
                targetObject = ClosestObject(GameObject.FindGameObjectsWithTag(Circle.localTarget), gameObject);
            }

            if (Circle.Advanced.enabled)
            {
                if (PassedBaseline(Circle.Advanced.activeAbove, Circle.Advanced.baseline, Circle.Advanced.triggerInactive, Circle.Advanced.triggeredBehaviorId))
                {
                    if (Circle.clockwise)
                    {
                        targetVector += Quaternion.Euler(0, 0, 90) * (targetObject.transform.position - gameObject.transform.position).normalized * Circle.strength;
                        totalWeights += Circle.strength;
                    }
                    else
                    {
                        targetVector += Quaternion.Euler(0, 0, -90) * (targetObject.transform.position - gameObject.transform.position).normalized * Circle.strength;
                        totalWeights += Circle.strength;
                    }
                }

            }
            else
            {
                if (Circle.clockwise)
                {
                    targetVector += Quaternion.Euler(0, 0, 90) * (targetObject.transform.position - gameObject.transform.position).normalized * Circle.strength;
                    totalWeights += Circle.strength;
                }
                else
                {
                    targetVector += Quaternion.Euler(0, 0, -90) * (targetObject.transform.position - gameObject.transform.position).normalized * Circle.strength;
                    totalWeights += Circle.strength;
                }
            }

            
            

        }

        if (totalWeights != 0 && targetVector != Vector3.zero)
        {
            targetVector = targetVector / totalWeights;
            totalWeights = 0;
        }

        velocityVector = SmoothAxisMovement.Calculate(targetVector.normalized, velocityVector);
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

    private bool PassedBaseline(bool activeAbove,float baseline,bool triggerInactive,int triggeredBehaviorId)
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
}
  