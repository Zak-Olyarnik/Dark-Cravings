using UnityEngine;
using System.Collections;


public class globMove : MonoBehaviour {

private Transform player;
public float speed;
Vector3 target;
public Sprite end;
private SpriteRenderer spriteRenderer;


	// Use this for initialization
	void Start () {
        player = PlayerController.p;  //GameObject.Find("Player").transform;
target = new Vector3(player.transform.position.x, player.transform.position.y - 0.22f, player.transform.position.z);// set target to player position
spriteRenderer = GetComponent<SpriteRenderer>();
	
	}
	
	// Update is called once per frame
	void Update () {

transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime); // go to position

if(transform.position == target){

spriteRenderer.sprite = end;

}

	
	}
}
