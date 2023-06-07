using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestruction : MonoBehaviour
{

    [SerializeField] private float _lifeTime = 4;
    void Start()
    {
        Destroy(this.gameObject, _lifeTime);
    }
    
}
