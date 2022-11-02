using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_XY_Movement : MonoBehaviour
{
    public float speed = 3;
    public float playerDistance = 3;

    Vector3 targetVect = Vector3.zero;
    Vector3 velocityVect = Vector3.zero;

    Vector3 tempIntrest = Vector3.zero;
    List<Vector3> intrestVects = new List<Vector3>();



    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        intrestVects = new List<Vector3>();
        targetVect = Vector3.zero;

        //Follow Player
        tempIntrest = GameObject.Find("Player").transform.position - transform.position;
        if (Vector3.Distance(tempIntrest, Vector3.zero) < playerDistance)
        {
            tempIntrest = -tempIntrest;
            if (Vector3.Distance(tempIntrest, Vector3.zero) > playerDistance-(speed*0.15f))
            {
                tempIntrest = Vector3.zero;
            }
        }
        intrestVects.Add(tempIntrest);

        // Circle Player
        tempIntrest = GameObject.Find("Player").transform.position - transform.position;
        tempIntrest = new Vector3(tempIntrest.y, -tempIntrest.x);
        intrestVects.Add(tempIntrest);


        foreach (var intrests in intrestVects)
        {
                targetVect += intrests;
        }



        velocityVect = SmoothAxisMovement.Calculate(targetVect.normalized, velocityVect);


        transform.position += velocityVect * speed * Time.deltaTime;
    }
}
