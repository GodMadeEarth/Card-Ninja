using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCardThrow : MonoBehaviour
{
    public bool isAimingCard = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( isAimingCard == false && Input.GetMouseButtonDown(0))
        {
            isAimingCard = true;
        }
        if (isAimingCard && Input.GetMouseButtonUp(0))
        {
            isAimingCard = false;
        }

        Debug.LogError("aiming = " + isAimingCard);
    }
}
