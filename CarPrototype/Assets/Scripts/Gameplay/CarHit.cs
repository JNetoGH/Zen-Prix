using UnityEngine;


public class CarHit : MonoBehaviour
{

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.tag.Equals("Pedestrian"))
            return;
        
        Debug.Log("Hit a bugs");
        other.gameObject.GetComponent<PedestrianController>().HitByCar = true;
        ZenBarController.DecrementBonusTime();
    }
    
}
