using UnityEngine;
using System.Collections;

public class PulseContactDestroy : MonoBehaviour {

	public float downSlope;
	public float downRaycast;

    void Update ()
	{
        // pulses fly up and down slopes, but continue as normal off the edge of the map
        //if (IsOnCliff())
        //{
            while (IsNearGround() == false) //&& IsOnCliff() == true)
            {
                //if (!IsOnCliff())
                   // Debug.Log("true");
                Vector3 newPos = new Vector3(transform.position.x, transform.position.y - downSlope, transform.position.z);
                transform.position = newPos;
            }
        //}
	}
	
    // pulses are destroyed immediately when they contact walls
	void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Wall")
        { Destroy(gameObject); }
    }
	
	private bool IsNearGround()
    { return Physics.Raycast(transform.position, Vector3.down, downRaycast); }

    private bool IsOnCliff()
    { return Physics.Raycast(transform.position, new Vector3(0,-3,0), downRaycast); }
}
