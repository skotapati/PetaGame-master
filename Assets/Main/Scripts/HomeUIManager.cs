using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeUIManager : MonoBehaviour {

	public Image levelChooserDisplay;
	public Button playBtn;
	public Button extrasBtn;

	// Use this for initialization
	void Start () {
		levelChooserDisplay.enabled = false;
		print ("score"+PlayerPrefs.GetInt ("bestScore")); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void goToZone1()
	{
		print ("Zone1");
		SceneManager.LoadScene ("Scene2");
		//PlayerPrefs.SetInt ("character", 2);
	}
}
