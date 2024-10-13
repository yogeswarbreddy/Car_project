using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider rearLeftWheelCollider;
    public WheelCollider rearRightWheelCollider;

    public Transform frontLeftWheelTransform;
    public Transform frontRightWheelTransform;
    public Transform rearLeftWheelTransform;
    public Transform rearRightWheelTransform;

    public float motorForce = 1500f; // Torque applied to the wheels
    public float brakeForce = 3000f;
    public float maxSteeringAngle = 30f; // Maximum angle the wheels can turn

    private float currentMotorForce;
    private float currentBrakeForce;
    private float currentSteerAngle;

    private void FixedUpdate()
    {
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    // Handle the motor input and apply forces to the wheels
    private void HandleMotor()
    {
        currentMotorForce = Input.GetAxis("Vertical") * motorForce;
        currentBrakeForce = Input.GetKey(KeyCode.Space) ? brakeForce : 0f;

        frontLeftWheelCollider.motorTorque = currentMotorForce;
        frontRightWheelCollider.motorTorque = currentMotorForce;

        ApplyBraking();
    }

    // Handle the steering input
    private void HandleSteering()
    {
        currentSteerAngle = maxSteeringAngle * Input.GetAxis("Horizontal");
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    // Apply the brakes
    private void ApplyBraking()
    {
        rearLeftWheelCollider.brakeTorque = currentBrakeForce;
        rearRightWheelCollider.brakeTorque = currentBrakeForce;
    }

    // Update the visual position of the wheels to match physics
    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
    }

    // Synchronize the WheelCollider with the visual wheel
    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 position;
        Quaternion rotation;
        wheelCollider.GetWorldPose(out position, out rotation);
        wheelTransform.position = position;
        wheelTransform.rotation = rotation;
    }
}
