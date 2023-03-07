using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CurveMoveSettings
{
    public AnimationCurve pull;
    public AnimationCurve weight;
    public AnimationCurve behavior;

    public string targetGroup;

    public Vector2 aproachDirection;
}

public class MovmentCurveLogic : MonoBehaviour
{
    public int behaviorID;
    public List<CurveMoveSettings> movementConfigs;
    float strength; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var config in movementConfigs)
        {
            
        }
    }

    GameObject ClosestObject(string tag, GameObject localObject)
    {
        GameObject closestObject = null;

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag(tag))
        {
            float distance = Vector3.Distance(obj.transform.position, localObject.transform.position);

            if (closestObject == null || distance < Vector3.Distance(closestObject.transform.position, localObject.transform.position))
            {
                closestObject = obj;
            }
        }
        return closestObject;
    }
    
    float ObjectDistance(GameObject object1,GameObject object2)
    {
        float distance = Vector3.Distance(object1.transform.position, object2.transform.position);
        return distance;
    }

}
