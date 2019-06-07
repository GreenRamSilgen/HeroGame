using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform predict;
    public PlayerPositionPredictor ppp;

    public Material mat;

    public bool moveOK;
    public float[] dashPosX;
    public float[] dashPosY;
    public float[] dashDiffX;
    public float[] dashDiffY;
    public int dashXTag;
    public int dashYTag;

    void Start() {
        dashPosX = new float[4];
        dashPosY = new float[4];
        dashDiffX = new float[3];
        dashDiffY = new float[3];

        dashXTag = Shader.PropertyToID("_DashDiffX");
        dashYTag = Shader.PropertyToID("_DashDiffY");
        mat.SetFloatArray(dashXTag, dashDiffX);
        mat.SetFloatArray(dashYTag, dashDiffY);
    }

    void FixedUpdate()
    {
        // Figure out how to have collision check first before move
        // edit: no need when can just recorrect position further
        transform.position = predict.position;

        if (ppp.dashing == true) {
            for (int i = dashPosX.Length - 1; i > 0; i--) {
                dashPosX[i] = dashPosX[i - 1];
                dashPosY[i] = dashPosY[i - 1];
            }

            dashPosX[0] = transform.position.x;
            dashPosY[0] = transform.position.y;

            for (int i = 0; i < dashDiffX.Length; i++) {
                dashDiffX[i] = dashPosX[0] - dashPosX[i + 1];
                dashDiffY[i] = dashPosY[0] - dashPosY[i + 1];
            }
        }
        else {
            for (int i = dashPosX.Length - 1; i > 0; i--) {
                dashPosX[i] = 0;
                dashPosY[i] = 0;
            }

            dashPosX[0] = 0;
            dashPosY[0] = 0;

            for (int i = 0; i < dashDiffX.Length; i++) {
                dashDiffX[i] = 0;
                dashDiffY[i] = 0;
            }
        }

        mat.SetFloatArray(dashXTag, dashDiffX);
        mat.SetFloatArray(dashYTag, dashDiffY);

    }

    void Update() {
        
    }

}