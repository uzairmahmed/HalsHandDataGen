using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject cameraDolly;
    public GameObject boundingBox;
    CameraDolly cd;
    BoundingBox bb;
    // Start is called before the first frame update
    void Start(){
        cd = cameraDolly.GetComponent<CameraDolly>();
        bb = boundingBox.GetComponent<BoundingBox>();
    }

    // Update is called once per frame
    void Update(){
        if(cd.rotation <=89){
            Debug.Log("AT ANGLE: " + cd.rotation + " DEGREES CELSIUS");
            bb.getBBCoords();
            cd.Rotate(10);
        }
        
    }
}
