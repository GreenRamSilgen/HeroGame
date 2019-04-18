using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform predict;
    public bool moveOK;
    
    void Start() {
        moveOK = false;
    }

    

    void Update()
    {


        // Here do the calculations? do it separately
        if (moveOK) {
            transform.position = predict.position;
            moveOK = false;
        }
    }

}