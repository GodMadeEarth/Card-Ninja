using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AdvancedSettings
{
    public bool enabled;
    public float baseline;
    public bool activeAbove;
    public bool triggerInactive;
    public int triggeredBehaviorId;
}
[System.Serializable]
public struct BasicSettings
{
    public int behaviorId;
    public bool enabled;
    public string globalTarget;
    public float speed;
}
[System.Serializable]
public struct LineMoveSettings
{
    public bool enabled;
    public float strength;
    public string localTarget;

    public AdvancedSettings Advanced; 
   
}
[System.Serializable]
public struct CircleMoveSettings
{
    public bool enabled;
    public bool clockwise;
    public float strength;
    public string localTarget;

    public AdvancedSettings Advanced;

}

public class XY_Movement : MonoBehaviour
{
    public BasicSettings Basic;

    public List<LineMoveSettings> Aproach;

    public List<LineMoveSettings> Flee;

    public List<CircleMoveSettings> Circle;

    private Vector3 velocity;
    private Vector3 direction;
    private float weights;

    private Vector3 globalTargetDirection;
    private Vector3 localTargetDirection;
    private Vector3 currentTargetDirection;


    


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Basic.enabled == true)

        {

            globalTargetDirection = ClosestTarget(Basic.globalTarget);

            foreach (var context in Aproach)
            {
                if (context.enabled)
                {
                    DetermineTarget(context);

                    AdvancedChecks(context);
                }
            }
            foreach (var context in Flee)
            {
                if (context.enabled)
                {
                    DetermineTarget(context);

                    //currentTargetDirection = -currentTargetDirection;

                    AdvancedChecks(context);
                }
            }
            foreach (var context in Circle)
            {
                if (context.enabled)
                {
                    DetermineTarget(context);



                    AdvancedChecks(context);
                }
            }
        }

        if (weights > 0)
        {
            direction = direction / weights;
            weights = 0;
        }
        else
        {
            direction = Vector3.zero;
        }


        velocity = SmoothAxisMovement.Calculate(direction.normalized, velocity);

        transform.position += velocity * Basic.speed * Time.deltaTime;


    }
    Vector3 ClosestTarget(string targetTag)
    {
        Vector3 targetDirection = Vector3.zero;
        try
        {
 
            foreach (var target in GameObject.FindGameObjectsWithTag(targetTag))
            {
                if (Vector3.Distance(target.transform.position, transform.position) < Vector3.Distance(targetDirection, transform.position) || targetDirection == Vector3.zero)
                {
                    if (GetInstanceID() != target.GetInstanceID())
                    {
                        targetDirection = target.transform.position;
                        targetDirection = target.transform.position;
                    }
                    
                }
                else if (targetDirection == transform.position)
                {
                    targetDirection = target.transform.position;
                }
                
            }
            targetDirection -= transform.position;
            targetDirection = targetDirection.normalized;

        }
        catch (System.Exception)
        {

            targetDirection = Vector3.zero;
        }
        return targetDirection;
    }

    void AddContext(LineMoveSettings context)
    {
        weights += context.strength;

        direction += currentTargetDirection.normalized * context.strength;
    }
    void AddContext(CircleMoveSettings context)
    {
        weights += context.strength;

        direction += currentTargetDirection.normalized * context.strength;
    }

    void AdvancedChecks(LineMoveSettings context)
    {
        if (context.Advanced.enabled)
        {
            if (context.Advanced.activeAbove)
            {
                if (context.Advanced.baseline < Vector3.Distance(Vector3.zero, currentTargetDirection))
                {
                    AddContext(context);
                }
            }
            else if (context.Advanced.baseline > Vector3.Distance(Vector3.zero, currentTargetDirection))
            {
                AddContext(context);
            }
        }
        else
        {
            AddContext(context);
        }
        

    }
    void AdvancedChecks(CircleMoveSettings context)
    {
        if (context.Advanced.enabled)
        {
            if (context.Advanced.activeAbove)
            {
                if (context.Advanced.baseline < Vector3.Distance(Vector3.zero, currentTargetDirection))
                {
                    AddContext(context);
                }
            }
            else if (context.Advanced.baseline > Vector3.Distance(Vector3.zero, currentTargetDirection))
            {
                AddContext(context);
            }
        }
        else
        {
            AddContext(context);
        }
    }

    void DetermineTarget(LineMoveSettings context)
    {
        if (context.localTarget != string.Empty)
        {
            localTargetDirection = ClosestTarget(context.localTarget);
            currentTargetDirection = localTargetDirection;
        }
        else
        {
            currentTargetDirection = globalTargetDirection;
        }
    }
    void DetermineTarget(CircleMoveSettings context)
    {
        if (context.localTarget != string.Empty)
        {
            localTargetDirection = ClosestTarget(context.localTarget);
            currentTargetDirection = localTargetDirection;
        }
        else
        {
            currentTargetDirection = globalTargetDirection;
        }
    }
}