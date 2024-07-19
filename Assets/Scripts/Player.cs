using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum ControlMode
    {
        Keyboard,
        Buttons
    };

    public enum Axel
    {
        Front,
        Rear
    }

    [Serializable]
    public struct Wheel
    {
        public GameObject wheelModel;
        public WheelCollider wheelCollider;
        public Axel axel;
    }

    public ControlMode control;

    public float maxAcceleration = 30.0f;
    public float brakeAcceleration = 50.0f;
    public bool doAnimate = true;
    public float turnSensitivity = 1.0f;
    public float maxSteerAngle = 30.0f;

    public float maxSpeed = 70.0f;

    public Vector3 _centerOfMass;

    public List<Wheel> wheels;

    float moveInput;
    float steerInput;

    private Rigidbody carRb;


    public float acceleratorPressure = 0f;

    void Start()
    {
        carRb = GetComponent<Rigidbody>();
        carRb.centerOfMass = _centerOfMass;

    }

    void Update()
    {
        GetInputs();
        if (doAnimate)
            AnimateWheels();
    }

    void LateUpdate()
    {
        Move();
        Steer();
        Brake();
    }

    public void MoveInput(float input)
    {
        moveInput = input;
    }

    public void SteerInput(float input)
    {
        steerInput = input;
    }

    void GetInputs()
    {
        if(control == ControlMode.Keyboard)
        {
            if (Input.GetKey(KeyCode.W)){
                if(acceleratorPressure < 100){
                    acceleratorPressure += 10f * Time.deltaTime;
                }else{
                    acceleratorPressure = 100f;
                }
            }else if (Input.GetKey(KeyCode.S)){
                if(acceleratorPressure > -5f){
                    acceleratorPressure -= 10f * Time.deltaTime;
                }else{
                    acceleratorPressure = -5f;
                }
            }else{
                if (acceleratorPressure > -1 && acceleratorPressure < 1)
                    acceleratorPressure = 5f;
            }
            moveInput = acceleratorPressure/100;
            steerInput = Input.GetAxis("Horizontal");
        }
    }

    void Move()
    {
        foreach(var wheel in wheels)
        {
            float power = Math.Abs(moveInput);
            if (carRb.velocity.magnitude < maxSpeed*power)
            {
                wheel.wheelCollider.motorTorque = moveInput * 600 * maxAcceleration * Time.deltaTime;
            }else
            {
                wheel.wheelCollider.motorTorque = 0;
                if (carRb.velocity.magnitude > maxSpeed*power)
                {
                    carRb.velocity = carRb.velocity.normalized * maxSpeed*power;
                }
            }
        }
    }

    void Steer()
    {
        foreach(var wheel in wheels)
        {
            if (wheel.axel == Axel.Front)
            {
                var _steerAngle = steerInput * turnSensitivity * maxSteerAngle;
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.6f);
            }
        }
    }

    void Brake()
    {
        if (Input.GetKey(KeyCode.Space) || moveInput == 0)
        {
            foreach (var wheel in wheels)
            {
                if (acceleratorPressure > 0f)
                    acceleratorPressure -= 5f * Time.deltaTime;
                wheel.wheelCollider.brakeTorque = 300 * brakeAcceleration * Time.deltaTime;
            }

        }
        else
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 0;
            }

        }
    }

    void AnimateWheels()
    {
        foreach(var wheel in wheels)
        {
            Quaternion rot;
            Vector3 pos;
            wheel.wheelCollider.GetWorldPose(out pos, out rot);
            wheel.wheelModel.transform.position = pos;
            wheel.wheelModel.transform.rotation = rot;
        }
    }
}
