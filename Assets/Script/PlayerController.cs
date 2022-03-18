using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    private Quaternion _initRot;
   

    // Start is called before the first frame update
    void Start()
    {
        _initRot = transform.rotation;
        
    }

    
    void LateUpdate()
    {
        transform.rotation = _initRot;

        
    }
    // Update is called once per frame
    void Update()
    {
    }

    
  
}
