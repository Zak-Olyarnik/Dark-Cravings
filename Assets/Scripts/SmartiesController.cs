using UnityEngine;
using System.Collections;

public class SmartiesController : MonoBehaviour {

    public GameObject[] shooters;
    private Transform player;
    public float speed;

    // starts shooting when player comes into range
    IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            for (int i = 0; i < shooters.Length; i++)
            {
                yield return new WaitForSeconds(2F);
                Shoot(shooters[i]);
            }
            yield return new WaitForSeconds(4F);
            Destroy(gameObject);
        }
    }

    // individual smarties target player and shoot at them
    void Shoot(GameObject shooter)
    {
            player = PlayerController.p;
            Vector3 target = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            if (shooter != null)
            {
                // subtract target pos - current pos, make velocity that
                float x_dir = target.x - shooter.transform.position.x;
                float y_dir = target.y - shooter.transform.position.y;
                float z_dir = target.z - shooter.transform.position.z;
                shooter.GetComponent<Rigidbody>().velocity = new Vector3(x_dir, y_dir, z_dir) * speed;
                Destroy(shooter, 3f);
            }
    }
}
