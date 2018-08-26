using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleBall : MonoBehaviour {
	//bool notCol = true;

	

	void OnCollisionEnter2D(Collision2D col) {
		
		if(col.gameObject.name == "ball"){
			//if(notCol){ //haven't collided with ball once yet
			GameObject b =Instantiate(col.gameObject);
			b.name = "ball";
			gameObject.SetActive(false);
			//}
			//notCol = false; //set bool to false so it only multiples once
		}
	}
}
