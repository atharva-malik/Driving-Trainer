using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightManager : MonoBehaviour
{
    public static lightManager Instance { get; set; }
    [SerializeField] private Light[] leftLights;
    [SerializeField] private GameObject leftArrow;
    [SerializeField] private Light[] rightLights;
    [SerializeField] private GameObject rightArrow;
    [SerializeField] private Light[] rearLights;
    [SerializeField] private Light[] frontMainLights;

    private int leftLightState = 0;
    private int rightLightState = 0;
    private int frontMainLightState = 0;

    private float blinkerCooldown = 0f;

    private void Awake() {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        DisableAllLights();
    }

    void Update()
    {
        if (blinkerCooldown >= 0.75f){
            if (leftLightState == 1){
                if (leftLights[0].enabled){
                    leftLights[0].enabled = false;
                    leftLights[1].enabled = false;
                    leftArrow.SetActive(false);
                }else{
                    leftLights[0].enabled = true;
                    leftLights[1].enabled = true;
                    leftArrow.SetActive(true);
                }
            }else if (rightLightState == 1){
                if (rightLights[0].enabled){
                    rightLights[0].enabled = false;
                    rightLights[1].enabled = false;
                    rightArrow.SetActive(false);
                }else{
                    rightLights[0].enabled = true;
                    rightLights[1].enabled = true;
                    rightArrow.SetActive(true);
                }
            }
            blinkerCooldown = 0f;
        }else{
            blinkerCooldown += Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.L)){
            EnableFrontLights();
        }else if (Input.GetKeyDown(KeyCode.Q)){
            EnableLeftLights();
        }else if (Input.GetKeyDown(KeyCode.E)){
            EnableRightLights();
        }
        // EnableRearLights();
        // DisableAllLights();
    }

    private void EnableRightLights()
    {
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
        foreach (Light light in leftLights)
            light.enabled = false;
        leftLightState = 0;
        if (rightLightState == 1){
            foreach (Light light in rightLights)
                light.enabled = false;
            rightLightState = 0;
            return;
        }
        rightArrow.SetActive(true);
        foreach (Light light in rightLights)
            light.enabled = true;
        rightLightState = 1;
    }

    private void EnableLeftLights()
    {
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
        foreach (Light light in rightLights)
            light.enabled = false;
        rightLightState = 0;
        if (leftLightState == 1){
            foreach (Light light in leftLights)
                light.enabled = false;
            leftLightState = 0;
            return;
        }
        leftArrow.SetActive(true);
        foreach (Light light in leftLights)
            light.enabled = true;
        leftLightState = 1;
    }

    private void EnableFrontLights()
    {
        if (frontMainLightState == 1){
            foreach (Light light in frontMainLights)
                light.intensity = 2;
            frontMainLightState = 2;
            return;
        }else if (frontMainLightState == 2){
            foreach (Light light in frontMainLights)
                light.enabled = false;
            frontMainLightState = 0;
            return;
        }
        foreach (Light light in frontMainLights){
            light.enabled = true;
            light.intensity = 1;
        }
        frontMainLightState = 1;
    }

    private void DisableAllLights(){
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
        foreach (Light l in leftLights){
            l.enabled = false;
        }
        foreach (Light l in rightLights){
            l.enabled = false;
        }
        foreach (Light l in rearLights){
            l.enabled = false;
        }
        foreach (Light l in frontMainLights){
            l.enabled = false;
        }
    }
}
