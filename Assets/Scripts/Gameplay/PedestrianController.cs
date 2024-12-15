using UnityEngine;
using UnityEngine.Serialization;

public class PedestrianController : MonoBehaviour
{
    
    public const float AutoDestructionLength = 15f;
    public const float AwaitTimeToStartFlying = 0.5f;
    public const float FlyingSpeed = 6f;
    public const float FlyingDuration = 4f;
    
    public bool HitByCar { get; set; }
    private bool _makeDesapearingSequence = false;
    private Rigidbody _rigidbody;

    [SerializeField] private Renderer _renderer;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        HitByCar = false;
        _makeDesapearingSequence = false;
        Invoke(nameof(TryAutoDestruct), AutoDestructionLength);
    }

    private void Update()
    {
        if (HitByCar)
        {
            
            _renderer.material.color = new Color32(255,0,0, 150);
            Invoke(nameof(StartDesapearingSequence), AwaitTimeToStartFlying);
        }

        if (_makeDesapearingSequence)
        {
            _rigidbody.velocity = new Vector3(
                x: _rigidbody.velocity.x,
                y: FlyingSpeed,
                z: _rigidbody.velocity.z);
            Destroy(this.gameObject, FlyingDuration);
        }
    }
    
    private void StartDesapearingSequence()
    {
        _makeDesapearingSequence = true;
    }

    private void TryAutoDestruct()
    {
        if (!HitByCar)
        {
            Destroy(this.gameObject);
        }
    }
    
    
}
