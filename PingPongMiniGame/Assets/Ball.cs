using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	 public float speed = 8.0f;
	 Vector2 dir = Vector2.zero;
	 public float maxSpeed = 9.0f;	
	  
	// Use this for initialization
	void Start () {
	GetComponent<Rigidbody2D>().velocity = Vector2.down * speed;	
	}
	
	// Update is called once per frame
	//void Update () {
		
	//}
	float hitFactor(Vector2 ballPos, Vector2 boardPos,
                float boardWidth) {
  
    return (ballPos.x - boardPos.x) / boardWidth;
}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.name == "player board") {
			if(transform.position.y >= col.transform.position.y){ //only if ball is colliding from above
				// Calculate hit Factor
        		float x=hitFactor(transform.position,
                          col.transform.position,
                          col.collider.bounds.size.x);

        		// Calculate direction, set length to 1
        	 	dir = new Vector2(x, 1).normalized;

       		 	// Set Velocity with dir * speed
				if(speed >= maxSpeed){
				GetComponent<Rigidbody2D>().velocity = dir * maxSpeed;
				}
				else
				{
				GetComponent<Rigidbody2D>().velocity = dir * speed;
				}
			}
    	}


		if(col.gameObject.name == "wall") {
			
			//if(transform.position.y <= col.gameObject.transform.position.y){ //ball is below wall
				if(speed >= maxSpeed){
					GetComponent<Rigidbody2D>().velocity = new Vector2(dir.x ,-dir.y).normalized * maxSpeed;
				}
				else{
					GetComponent<Rigidbody2D>().velocity = new Vector2(dir.x ,-dir.y).normalized * speed;
				}
				
			//}
			
			
		}
		if(col.gameObject.name == "wallSide") {
			if(speed >= maxSpeed){
				GetComponent<Rigidbody2D>().velocity = new Vector2(-dir.x, Mathf.Min(dir.y, -dir.y)).normalized * maxSpeed;
				}
			else{
				GetComponent<Rigidbody2D>().velocity = new Vector2(-dir.x, Mathf.Min(dir.y, -dir.y)).normalized * speed;
			}
			
		}

		if(col.gameObject.name  == "GameObject"){
			if(transform.position.y > col.gameObject.transform.position.y){ 
				 if(speed >= maxSpeed){
					 GetComponent<Rigidbody2D>().velocity = new Vector2(dir.x, -dir.y).normalized * maxSpeed;
				 }
				 else{
					 GetComponent<Rigidbody2D>().velocity = new Vector2(dir.x, -dir.y).normalized * speed;
				 }
				
			}
			else{
				 if(speed >= maxSpeed){
					GetComponent<Rigidbody2D>().velocity = new Vector2(-dir.x ,-dir.y).normalized * maxSpeed;
				}
				else{
					GetComponent<Rigidbody2D>().velocity = new Vector2(-dir.x ,-dir.y).normalized * speed;
				}
			 }
				
				
			
		
		}
		if(col.gameObject.name  == "ball"){
			if(speed > maxSpeed){ //control collision speed
				speed = maxSpeed;
			}
		}
	}
}
