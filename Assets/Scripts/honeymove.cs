using UnityEngine;
using System.Collections;

public class honeymove : MonoBehaviour {

public float start;
public float end;
public float dist;
public float speed;
private bool turn = false;
public float health;
public SpriteRenderer rend;

void Start () {

start = transform.position.x; //figure out start and end positions
end = start + dist;


}


IEnumerator OnCollisionEnter(Collision other)
{
if (other.gameObject.tag == "pAttack" || other.gameObject.tag == "bAttack") //take damage if hit
{
health -= 1;
Debug.Log(health);
Destroy(other.gameObject);

// sprite flashes red when hit
rend.color = Color.red;
yield return new WaitForSeconds(.1F);
rend.color = Color.white;
}
}

void Update () {

if(health <= 0){
Destroy(gameObject);
}

//figure out which way to go
if(transform.position.x <= start)
{
turn = true;
}
if(transform.position.x >= end - 1)
{
turn = false;
}

if(turn) //go between start and end
{
transform.Translate(speed  * Time.deltaTime, 0, 0);
}
else
{
transform.Translate(-speed * Time.deltaTime, 0, 0);
}

}


}


