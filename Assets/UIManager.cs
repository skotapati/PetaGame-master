using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Text scoreKeeper;

	// Use this for initialization
	void Start () {
		updateScoreText ();
	}
	
	// Update is called once per frame
	void Update () {
		updateScoreText ();
	
	}

	public void updateScoreText()
	{
		scoreKeeper.text = GameController.score.ToString();
	}
}
