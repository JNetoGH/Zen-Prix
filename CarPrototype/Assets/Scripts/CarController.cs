using UnityEngine;

public class CarController : MonoBehaviour
{
    
    [Header("Car Settings")]
    [SerializeField] private float _maxSteerAngle = 30;
    [SerializeField] private float _motorForce = 50;

    [Header("Front Left Wheel")]
    [SerializeField] private WheelCollider _wheelColliderFrontLeft;
    [SerializeField] private Transform _wheelTransformFrontLeft;
    
    [Header("Front Right Wheel")]
    [SerializeField] private WheelCollider _wheelColliderFrontRight;
    [SerializeField] private Transform _wheelTransformFrontRight;
    
    [Header("Back Left Wheel")]
    [SerializeField] private WheelCollider _wheelColliderBackLeft;
    [SerializeField] private Transform _wheelTransformBackLeft;
    
    [Header("Back Right Wheel")]
    [SerializeField] private WheelCollider _wheelColliderBackRight;
    [SerializeField] private Transform _wheelTransformBackRight;

    private float _horizontalInput;
    private float _verticalInput;
    private float _currentSteerAngle;
    
    private void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        UpdateAllWheelPoses();
    }

    private void GetInput()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
    }

    private void Steer()
    {
        _currentSteerAngle = _maxSteerAngle * _horizontalInput;
       
        // updates only the front wheels steer angle
        _wheelColliderFrontLeft.steerAngle = _currentSteerAngle;
        _wheelColliderFrontRight.steerAngle = _currentSteerAngle;
    }
    
    private void Accelerate()
    {
        float torque = _motorForce * _verticalInput;
        
        // gives torque to the front wheels
        _wheelColliderFrontLeft.motorTorque = torque;
        _wheelColliderFrontRight.motorTorque = torque;
        
        _wheelColliderBackLeft.motorTorque = torque;
        _wheelColliderBackRight.motorTorque = torque;
    }
    
    private void UpdateAllWheelPoses()
    {
        // Front
        UpdateWheelPose(_wheelColliderFrontLeft, _wheelTransformFrontLeft);
        UpdateWheelPose(_wheelColliderFrontRight, _wheelTransformFrontRight);
        
        // Back
        UpdateWheelPose(_wheelColliderBackLeft, _wheelTransformBackLeft);
        UpdateWheelPose(_wheelColliderBackRight, _wheelTransformBackRight);
    }
    
    // syncs the pose information from the wheelCollider to the transform
    private void UpdateWheelPose(WheelCollider wheelCollider, Transform transform)
    {
        Vector3 currentPosition = transform.position;
        Quaternion currentRotation = transform.rotation;
        
        wheelCollider.GetWorldPose(out currentPosition, out currentRotation);
        transform.position = currentPosition;
        transform.rotation = currentRotation;
    }
    
}
