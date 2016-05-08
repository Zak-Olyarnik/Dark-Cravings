using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    public int thisArea;
    public GameObject[] areas;


    // door disappears if player approaches while carrying a key
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" && PlayerController.hasKey == true)
        {
            if (thisArea < 18)
            { areas[thisArea + 2].SetActive(true); }
            if (thisArea > 3)
            { areas[thisArea - 2].SetActive(false); }

            PlayerController.hasKey = false;
            Destroy(gameObject);
        }

    }
}
