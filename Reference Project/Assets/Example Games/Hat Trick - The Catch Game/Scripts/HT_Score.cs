using UnityEngine;
using System.Collections;

public class HT_Score : MonoBehaviour {

	public GUIText scoreText;
	public GUIText highScoreText;
	public int ballValue;
	private int highScore;
	private int score;

	void Start () {
		if (PlayerPrefs.HasKey ("High Score")) {
			highScoreText.text = "High Score: " + PlayerPrefs.GetInt ("High Score");
		} 
		score = 0;
		UpdateScore ();

	}

	void OnTriggerEnter2D (Collider2D other) {
		score += ballValue;
		UpdateScore ();
	}
	
	void OnCollisionEnter2D (Collision2D collision) {
		if (collision.gameObject.tag == "Bomb") {
			score -= ballValue * 2;
			UpdateScore ();
		}
	}

	void UpdateScore () {
		scoreText.text = "SCORE:\n" + score;
		if (score > highScore)
		{
			highScore = score;
		}
		PlayerPrefs.SetInt("High Score", highScore);
		highScoreText.text = "HIGHSCORE:\n " + highScore;

	}
}
