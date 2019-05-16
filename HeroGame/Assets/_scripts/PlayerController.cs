using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform predict;
    public bool moveOK;
    
    void Start() {
        
    }

    void FixedUpdate()
    {
        // Figure out how to have collision check first before move
        // edit: no need when can just recorrect position further
        transform.position = predict.position;
    }

    private void Update() {
        
    }

}