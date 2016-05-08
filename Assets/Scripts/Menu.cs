using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Text lastScore;                  // displays score
    public GameObject scoreHolder;          // GameObject parented to lastScore
	public GameObject startButton;			// start button
	public GameObject playAgainButton;       // play again button
	public GameObject youWin;       		// displays victory text
	public GameObject youLose;      		 // displays failure text
	public GameObject playerStart;      		 // the player
	public GameObject foilStart;      		 // the foil containing the player
	public GameObject playerEnd;      		 // the player
	public GameObject foilEnd;      		 // the foil containing the player
    static private bool firstTime = true;   // initial play

    void Start()
    {
        if (!firstTime)
        {
            playerStart.SetActive(false);
			playerEnd.SetActive(true);
			
			foilStart.SetActive(false);
			foilEnd.SetActive(true);
			
			startButton.SetActive(false);
			playAgainButton.SetActive(true);
			
			scoreHolder.SetActive(true);      // enabling lastScore by itself didn't work for some reason, so
            lastScore.text = "score - " + Pickup.score;     // I SetActive() its parent instead
            if (PlayerController.won == true)
            {
				youWin.SetActive(true);
                youLose.SetActive(false);
			}
            else
            {
                youWin.SetActive(false); 
				youLose.SetActive(true); 
			}
        }
    }

    // initializes and loads main level
    public void StartClick()
    {
        firstTime = false;
        PlayerController.hasKey = false;
        PlayerController.won = false;
        PlayerController.health = 10;
        Pickup.score = 0;
        Application.LoadLevel("main");
    }

}