using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour {
	public Text displayText;
	float timeleft ;
	// Use this for initialization
	void Start () {
		Timer t = gameObject.GetComponent<Timer>();
		timeleft = t.getDelay();
		displayText.text = "Countdown: " + timeleft;
	}
	
	// Update is called once per frame
	void Update () {
		timeleft -= Time.deltaTime;

		displayText.text = "Countdown: " + timeleft;
	}
}
