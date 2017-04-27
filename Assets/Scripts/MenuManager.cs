using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	[SerializeField] private AudioSource menuMusic;

	// Use this for initialization
	void Start () {
		menuMusic.Play();
	}
	
	// quits the game
	public void exit() {
		Application.Quit();
	}

	public void stage1() {
		SceneManager.LoadScene(1);
	}

	public void stage2() {
		SceneManager.LoadScene(2);
	}

}
