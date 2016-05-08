using UnityEngine;
using System.Collections;

public class SmartiesContactDestroy : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    { Destroy(gameObject); }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "pAttack" || other.gameObject.tag == "bAttack")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
