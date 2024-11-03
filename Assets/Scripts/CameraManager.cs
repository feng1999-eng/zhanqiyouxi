using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager
{
    private Transform camTf; //摄像机
    private Vector3 prePos; //相机之前的位置
    
    public CameraManager()
    {
        camTf = Camera.main.transform;
        prePos = camTf.position;
    }

    public void SetPos(Vector3 pos)
    {
        pos.z = camTf.position.z;
        camTf.transform.position = pos;
    }

    public void ResetPos()
    {
        camTf.transform.position = prePos;
    }
}
