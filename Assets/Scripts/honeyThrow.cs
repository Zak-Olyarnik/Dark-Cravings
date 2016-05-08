using UnityEngine;
using System.Collections;

public class honeyThrow : MonoBehaviour
{
    public GameObject glob;
    public float speed;
    public float fireRate;
    private float nextFire;
    
    void OnTriggerStay(Collider other)
    {
        // shoot the player at the rate of fire rate
        if (other.gameObject.tag == "Player" && Time.time > nextFire)
        {
            Shoot(glob);
            nextFire = Time.time + fireRate;
        }
    }
    
    void Shoot(GameObject glob)
    { Instantiate(glob, transform.position, transform.rotation); }
}
