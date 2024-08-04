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

    public float wheelSensitivity;
    public float steerTrim;
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
    public float steerAnglePercentage = 0f;

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
                if (acceleratorPressure > -1 && acceleratorPressure < 1 && !Input.GetKey(KeyCode.Space)) // Player is not braking
                    acceleratorPressure = 4f;
            }
            moveInput = acceleratorPressure/100;
            //? Failed experiment, maybe I'll come back to it at a later date.
            // if (Input.GetKey(KeyCode.D)){
            //     if(steerAnglePercentage < 100){
            //         steerAnglePercentage += 10f * Time.deltaTime * wheelSensitivity;
            //     }else{
            //         steerAnglePercentage = 100f;
            //     }
            // }else if (Input.GetKey(KeyCode.A)){
            //     if(steerAnglePercentage > -100f){
            //         steerAnglePercentage -= 10f * Time.deltaTime * wheelSensitivity;
            //     }else{
            //         steerAnglePercentage = -100f;
            //     }
            // }else{
            //     if (steerAnglePercentage > -1 && steerAnglePercentage < 1 && !Input.GetKey(KeyCode.Space)) // Player is not braking
            //         steerAnglePercentage = steerTrim;
            //     else if (steerAnglePercentage > 0)
            //         steerAnglePercentage -= 100f * Time.deltaTime * wheelSensitivity;
            //     else
            //         steerAnglePercentage += 100f * Time.deltaTime * wheelSensitivity;
            // }
            // steerInput = steerAnglePercentage/100;
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
                // wheel.wheelCollider.motorTorque = moveInput * 600 * maxAcceleration * Time.deltaTime;
                wheel.wheelCollider.motorTorque = moveInput * maxAcceleration * Time.deltaTime * 100;
            }else
            {
                if (carRb.velocity.magnitude > maxSpeed*power && ((transform.eulerAngles.x > -8 && transform.eulerAngles.x < 5)|| 
                (convert(transform.eulerAngles.x) > -8 && convert(transform.eulerAngles.x) < 5))) // Checking if player is not on a slope
                {
                    // ! Instead of hard assigning the velocity, I want to soft assign it.
                    // float[] vals = getValues(carRb.velocity.x, carRb.velocity.z, maxSpeed*power);
                    // float x = vals[0];
                    // float z = vals[1];
                    // carRb.velocity = new Vector3(x, carRb.velocity.y, z);
                    wheel.wheelCollider.motorTorque = 0;
                    carRb.velocity = new Vector3(carRb.velocity.x - (carRb.velocity.x * Time.deltaTime * 0.1f), carRb.velocity.y, carRb.velocity.z - (carRb.velocity.z * Time.deltaTime * 0.1f));
                }else{
                    wheel.wheelCollider.motorTorque = 0;
                }
            }
        }
    }

    private float convert(float x)
    {
        return 360 - x;
    }

    private float[] getValues(float x, float z, float v)
    {
        // Formula:
        // int a = -16**2 + 8**2
        // int b = (v**2)/a
        // int rat_num = b**0.5
        // return new float[a*rat_num, b*rat_num]
        float[] ans = new float[2];
        float rat_num = Mathf.Sqrt(v*v/(x*x + z*z));
        ans[0] = x*rat_num;
        ans[1] = z*rat_num;
        return ans;
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
            lightManager.Instance.isBraking = true;
            foreach (var wheel in wheels)
            {
                acceleratorPressure = 0f;
                wheel.wheelCollider.brakeTorque = 300 * brakeAcceleration * Time.deltaTime;
            }

        }
        else
        {
            lightManager.Instance.isBraking = false;
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

    internal void resetWheels()
    {
        foreach (var wheel in wheels){
            wheel.wheelCollider.motorTorque = 0;
            wheel.wheelCollider.brakeTorque = 0;
        }
    }
}
