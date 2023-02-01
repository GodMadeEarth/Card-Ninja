using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLogic : MonoBehaviour
{
    public GameObject owner;
    public float speed;
    public AnimationCurve AnimationCurvecurve = new AnimationCurve();


    // Start is called before the first frame update
    public void Initiate(GameObject ownerObject)
    {
        owner = ownerObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
