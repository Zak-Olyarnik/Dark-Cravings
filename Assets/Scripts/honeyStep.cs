using UnityEngine;
using System.Collections;

public class honeyStep : MonoBehaviour {

private float temp;
public float newSpeed;

void Start(){
temp  = gameObject.GetComponent<PlayerController>().speed;
}

void OnTriggerEnter(Collider other){

Debug.Log(other.gameObject);

if(other.gameObject.tag == "glob"){ // if tuching the glob reduce speed of player


gameObject.GetComponent<PlayerController>().speed = newSpeed;

}


}

void OnTriggerExit(Collider other){

if(other.gameObject.tag == "glob"){ // if no longer tuching the glob go back to orignal speed
gameObject.GetComponent<PlayerController>().speed = temp;

}


}

}
