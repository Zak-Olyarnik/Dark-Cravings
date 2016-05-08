using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    public Text scoreText;      // UI for score display
    static public int score = 0;

    void OnTriggerEnter(Collider other)
    {
        // pickup key
        if (other.gameObject.tag == "key")
        { PlayerController.hasKey = true; }
        
        // pickup health
        else if (other.gameObject.tag == "health")
        {
            PlayerController.health += 1;
            if (PlayerController.health > 10)
            { PlayerController.health = 10; }
            PlayerController.getInstance().UpdateHealthUI();
        }
        
        // pickup points
        else if (other.gameObject.tag == "point")
        {
            score += 10;
            scoreText.text = "" + score;
        }
        //pick up toothbrush
        else if (other.gameObject.tag == "Toothbrush")
        { Invoke("setWon", 1.5f); }
    }

    void setWon()
    { PlayerController.won = true; }
}