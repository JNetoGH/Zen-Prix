using UnityEngine;

public class PedestrianController : MonoBehaviour
{
    
    public const float AutoDestructionLength = 15f;
    public const float AwaitTimeToStartFlying = 0.5f;
    public const float FlyingSpeed = 6f;
    
    public bool HitByCar { get; set; }
    private bool _makeDesappearingSequence = false;
    private Rigidbody _rigidbody;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        HitByCar = false;
        _makeDesappearingSequence = false;
        Destroy(this.gameObject, AutoDestructionLength);
    }

    private void Update()
    {
        if (HitByCar)
            Invoke(nameof(StartDesapearingSequence), AwaitTimeToStartFlying);
        if (_makeDesappearingSequence)
            _rigidbody.velocity = new Vector3(
                x: _rigidbody.velocity.x,
                y: FlyingSpeed,
                z: _rigidbody.velocity.z);
    }
    
    private void StartDesapearingSequence()
    {
        _makeDesappearingSequence = true;
    }
    
    
}
