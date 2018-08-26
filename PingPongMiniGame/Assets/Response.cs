using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Response : MonoBehaviour {
	public Text displayText;
	string response;

	void Start(){
		
	}

	public void CreateText(){
		response = getResponse();
		displayText.text = "" + response;
	}


	string getResponse(){
		int curr = Random.Range(0, 7);

		switch (curr)
		{
			case 0:
			return "Uh huh";

			case 1:
			return "Game's Over, friend." ;

			case 2:
			return "What were you doing in the restart menu for so long?";

			case 3:
			return "Nice Weather, ain't it.";

			case 4:
			return "Just restart the game. Seriously.";

			case 5:
			return "The button doesn't actually do anything...";

			case 6:
			return "Congrats, you're lost in space and time!";

			default:
			return "";
			
		}
	}
	   
   
}
