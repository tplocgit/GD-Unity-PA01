using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMain : MonoBehaviour
{
    Vector3 VECTOR_CAMERA_POSITION = new Vector3(0, 30, -40);
    Vector3 VECTOR_CAMERA_ROTATE = new Vector3(30, 0, 0);
    string OBJ_A_TAG = "obj_a";
    
    GameObject obj_a;
    void Awake () {
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 60;
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.position = VECTOR_CAMERA_POSITION;
        transform.Rotate(VECTOR_CAMERA_ROTATE);
        obj_a = GameObject.FindWithTag(OBJ_A_TAG);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = obj_a.transform.position + VECTOR_CAMERA_POSITION;
    }
}
