using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDolly : MonoBehaviour
{
    Camera camera;
    public float rotation;
    public float length;

    // Start is called before the first frame update
    void Start(){
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update(){
        rotation = transform.rotation.eulerAngles.x;
        length = camera.transform.position.z;
    }
    public void Rotate(float angle){
        transform.Rotate(angle, 0f, 0f);
    }

    public void ExtendDolly(float length){
        camera.transform.Translate(0, 0, -length);
    }
}
