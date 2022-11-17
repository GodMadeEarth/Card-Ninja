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
    //public LineMoveSettings Aproach;

    public List<LineMoveSettings> Flee;

    public List<CircleMoveSettings> Circle;

    private Vector3 velocity;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
