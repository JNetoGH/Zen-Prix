using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Instantiatior : MonoBehaviour
{
    
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _objectToSpawn;
    [SerializeField] private float y;
    [SerializeField] private float _rateOfSpawn = 1;
    private float _nextSpawn = 0;
    
    void Start()
    {
        
    }

    void Update()
    {
        TrySpawn(_objectToSpawn);
    }

    private void TrySpawn(GameObject gameObject)
    {
        if (!(Time.time > _nextSpawn))
            return;
        
        _nextSpawn = Time.time + _rateOfSpawn;
        // Random position within this transform
        Vector3 rndPosWithin;
        rndPosWithin = new Vector3(Random.Range(-1f, 1f), this.y, Random.Range(-1f, 1f));
        rndPosWithin = transform.TransformPoint(rndPosWithin * .5f);
        Instantiate(gameObject, rndPosWithin, transform.rotation);
    }
}


/*
 *
 *
 *
 * using UnityEngine;
using System.Collections;
 
/// <summary>
/// Spawns a prefab randomly throughout the volume of a Unity transform. Attach to a Unity cube to visually scale or rotate. For best results disable collider and renderer.
/// </summary>
public class SpawningArea : MonoBehaviour {
 
public GameObject ObjectToSpawn;   
public float RateOfSpawn = 1;
   
private float nextSpawn = 0;
 
    // Update is called once per frame
    void Update () {           
       
        if(Time.time > nextSpawn)
        {
            nextSpawn = Time.time + RateOfSpawn;
           
            // Random position within this transform
            Vector3 rndPosWithin;
            rndPosWithin = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            rndPosWithin = transform.TransformPoint(rndPosWithin * .5f);
            Instantiate(ObjectToSpawn, rndPosWithin, transform.rotation);      
        }
    }
}
 
 */