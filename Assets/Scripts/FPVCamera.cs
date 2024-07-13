using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPVCamera : MonoBehaviour
{
    public GameObject FPCamera;
    public GameObject TRDCamera;

    // Start is called before the first frame update
    void Start()
    {
        FPCamera.SetActive(true);
        TRDCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown(KeyCode.C)){
            FPCamera.SetActive(false);
            TRDCamera.SetActive(true);
        }
    }
}
