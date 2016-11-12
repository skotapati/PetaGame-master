using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeUIManager : MonoBehaviour {

	public Image levelChooserDisplay;
	public Button playBtn;
	public Button extrasBtn;
	public Canvas main;

	// Use this for initialization
	void Start () {
		levelChooserDisplay.enabled = false;
		print ("score"+PlayerPrefs.GetInt ("bestScore")); 
		print ("character"+PlayerPrefs.GetInt("player"));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void switchScreens()
	{
		main.enabled = !main.enabled;
	}

	public void goToZone1()
	{
		print ("Zone1");
		SceneManager.LoadScene ("Scene2");
		//PlayerPrefs.SetInt ("character", 2);
	}
}
