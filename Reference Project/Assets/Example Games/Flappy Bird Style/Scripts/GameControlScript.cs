using UnityEngine;
using System.Collections;

public class GameControlScript : MonoBehaviour 
{
	public static GameControlScript current;	//a reference to our game control so we can access it statically
	public ColumnSpawnScript columnSpawner;		//a reference to the column spawner
	public GUIText scoreText;					//a reference to text that shows the player's score
	public GUIText highScoreText;
	public GameObject gameOvertext;				//a reference to the object that contains the text that appears when the player dies

	int score = 0;							//the player's score
	int highScore = 0;
	//if(PlayerPrefs.HasKey ("High Score")) {			//the player's highscore
		//highScore = PlayerPrefs.GetInt ("High Score");
	//}
							
	bool isGameOver = false;					//is the game over?


	void Awake()
	{
		//if we don't currently have a game control...
		if (current == null)
			//...set this one to be it...
			current = this;
		//...otherwise...
		else if(current != this)
			//...destroy this one because it is a duplicate
			Destroy (gameObject);
	}

	void Update()
	{
		if (PlayerPrefs.HasKey ("High Score"))
		{
			highScoreText.text = "High Score: " + PlayerPrefs.GetInt("High Score");
		}
		//if the game is over and the player has pressed some input...
		if (isGameOver && Input.anyKey) 
		{
			//...start a new game.
			Application.LoadLevel(Application.loadedLevel);		
		}
	}

	public void BirdScored()
	{
		//the bird can't score if the game is over
		if (isGameOver){
			if(score > PlayerPrefs.GetInt("High Score"))
				PlayerPrefs.SetInt ("High Score", score);
			return;
		}
		//increase score
		score++;
		//adjust the score text
		scoreText.text = "Score: " + score;
		if (PlayerPrefs.HasKey ("High Score")) {
			highScore = PlayerPrefs.GetInt ("High Score");
			if (score > highScore) {
				highScore++;
				PlayerPrefs.SetInt ("High Score", highScore);
				highScoreText.text = "High Score: " + highScore;
			} else
			{
				highScoreText.text = "High Score: " + highScore;
			}
		} 
		else
		{
			highScore = score;
			PlayerPrefs.SetInt ("High Score", highScore);
			highScoreText.text = "High Score: " + highScore;
		}
	}

	public void BirdDied()
	{
		//don't spawn new columns
		columnSpawner.StopSpawn ();
		//show the game over text
		gameOvertext.SetActive (true);
		//set the game to be over
		isGameOver = true;
	}
//	void updateScore()
//	{
//		highScoreText = "High score: " + highScore;
//	}
}
