using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _positionOffset;
    [SerializeField] private Vector3 _rotationOffset;
    [Tooltip("How fast the camera is going after the object we are trying to follow")]
    [SerializeField] private float _followSpeed = 10;
    [Tooltip("How fast the camera is looking for the object we are trying to follow")]
    [SerializeField] private float _lookSpeed = 15;
    
    private void FixedUpdate()
    {
        LookAtTarget();
        MoveTowardTarget();
    }
    
    private void LookAtTarget()
    {
        Vector3 lookDirection = _target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, _lookSpeed * Time.fixedDeltaTime);
        
        // offset appliance
        Vector3 curRot = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(curRot.x + _rotationOffset.x, curRot.y + _rotationOffset.y, curRot.z + _rotationOffset.z);
    }

    private void MoveTowardTarget()
    {
        // based on the offset of the object we are following
        Vector3 targetOffsetPos = _target.position;
        targetOffsetPos += _target.forward * _positionOffset.z + _target.right * _positionOffset.x + _target.up * _positionOffset.y;
        transform.position = Vector3.Lerp(transform.position, targetOffsetPos, _followSpeed * Time.fixedDeltaTime);
    }
    
}
