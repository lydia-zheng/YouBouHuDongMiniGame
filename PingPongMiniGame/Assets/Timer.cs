using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {
	public float delay = 50;
	
	
	// Use this for initialization
	void Start () {
		StartCoroutine(LoadLevelAfterDelay(delay));
	
	}

	public float getDelay(){
		return delay;
	}
	
	
	
	IEnumerator LoadLevelAfterDelay(float delay){
		yield return new WaitForSeconds(delay);
		SceneManager.LoadScene("TimesUp");
	}
}
