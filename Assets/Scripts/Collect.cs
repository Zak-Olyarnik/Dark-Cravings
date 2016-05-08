using UnityEngine;
using System.Collections;

public class Collect : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")//destroy if picked up by player
        { Destroy(gameObject); }
    }
}
