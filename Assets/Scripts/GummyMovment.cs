using UnityEngine;
using System.Collections;

public class GummyMovment : MonoBehaviour {

    public float speed;
    public float health;
    private GameObject player;
    private float px;
    private float pz;
    public float space;
    private Vector3 start;
    private Vector3 end;
    bool inRange = false;

    public SpriteRenderer rend;

    // variables controlling which collectible to instantiate
    public int points;
    public bool key;
    public bool heart;

    // instances of collectibles to instantiate
    public GameObject Point;
    public GameObject Key;
    public GameObject Health;


	// Use this for initialization
	void Start () {
	}

IEnumerator OnCollisionEnter(Collision other)
{
    if (other.gameObject.tag == "pAttack") // when hit by palse
    {
        health -= 2;
        Destroy(other.gameObject); // destroy the palse
        
        // sprite flashes red when hit
        rend.color = Color.red;
        yield return new WaitForSeconds(.1F);
        rend.color = Color.white;
    }
    else if (other.gameObject.tag == "bAttack") // when hit by burst
    {
        health -= 1;
        Destroy(other.gameObject, .2f); //destry burst

        // sprite flashes red when hit
        rend.color = Color.red;
        yield return new WaitForSeconds(.1F);
        rend.color = Color.white;
    }
}

	// Update is called once per frame
void Update()
{
    player = PlayerController.getInstance().gameObject;
    
    // drop collectibles and die
    if (health <= 0)
    {
        for (int i = 0; i < points; i++)
        {
            Vector3 placeDrop = transform.position + Random.onUnitSphere;
            placeDrop.y = transform.position.y;
            Instantiate(Point, placeDrop, transform.rotation);
        }

        if (key)
        { Instantiate(Key, transform.position, transform.rotation); }

        if (heart)
        { Instantiate(Health, transform.position, transform.rotation); } 
        
        Destroy(gameObject);
    }

    if (inRange) //if close to player
    {
        // rotate sprite to face player
        if (player.transform.position.x > transform.position.x)
        { transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); }
        else if (player.transform.position.x < transform.position.x)
        { transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); }

        // find player position and move to player
        float dist = Vector3.Distance(player.transform.position, transform.position);
        px = player.transform.position.x;
        pz = player.transform.position.z;
        end = new Vector3(px, transform.position.y, pz);

        if (dist > Mathf.Abs(space))
        {
            transform.position = Vector3.MoveTowards(transform.position, end, speed * Time.deltaTime);
            start = transform.position;
        }
        else if (dist > 0.1)
        { transform.position = Vector3.MoveTowards(transform.position, end, speed * Time.deltaTime); }
        else
        { transform.position = start; }
    }
}
	

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        { inRange = true; }
    }
}