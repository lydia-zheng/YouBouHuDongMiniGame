using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class randomize : MonoBehaviour {
	public float adjust = 0.5f;
	public float overlapTol = 1.5f;
	Transform obstacles;
	float imageH;
	float imageW;
	float x;
	float y;
	Vector3 pos;
	float width;
	float height;
	//List<Vector3> posList= new List<Vector3>();
	int curr = 0;

	


  public void Level(){
	  
	  List<Vector3> tempList = new List<Vector3>(); 
	 //  width = transform.Find("b2/2").GetComponent<SpriteRenderer>().bounds.size.x;
	  // height = transform.Find("b2/2").GetComponent<SpriteRenderer>().bounds.size.y;
	   width = transform.Find("b2/2").GetComponent<RectTransform>().rect.width;
	  // height =transform.Find("b2/2").GetComponent<RectTransform>().rect.height;

	  // Debug.Log(string.Format("Width: {0}, Height: {1}", width, height));

	   obstacles = getObstacles();
	   
	  
	/*for (int i = 0; i < obstacles.transform.childCount; i++){
		
		Transform o = obstacles.GetChild(i);
		posList.Add(o.position);//adding positions to list
	}*/

	foreach (Transform o in obstacles)
	{
		x = Random.Range (-(width/2)+ adjust, width/2-adjust);
		y = Random.Range (0+ adjust, height/2 - adjust);
		
		imageH = o.gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
		imageW = o.gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
	
		

	//	Debug.Log(string.Format("Width/2: {0}, Height/2: {1}", width/2, height/2));
	//	Debug.Log(string.Format("X: {0}, Y: {1}", x, y));

		pos = new Vector3(x, y, o.position.z); 

 		//Debug.Log(string.Format("Pos: {0}", pos));
// checkOverLap segments
//!!
		while(CheckOverLap(pos, tempList)){
			print("entered while loop");
			x = Random.Range (-(width/2)+ adjust, width/2-adjust);
			y = Random.Range (0+ adjust, height/2 - adjust);
		
			pos = new Vector3(x, y, o.position.z); //if overlap returns true, reset position
		}
		print("exit while loop");
		
		tempList.Add(pos);
       
//!!

		o.localPosition = pos;
		
	}
	
	curr++;
   }

	public Transform getObstacles(){
		switch(curr % 3){
			case 0:
			 	return transform.Find("b1/1/obstacles1");
			
			case 1:
			 	return transform.Find("b2/2/obstacles2");

			case 2:
			 	return transform.Find("b3/3/obstacles3");
		
			default:
			print ("Failed to retrieve obstacle from b#");
			return null;
	}


	

}
 	void rerollY(){
		 print("entered rerollY");
			while((y+ (imageH/2))> height/2){ //reroll y value if y makes obstacle go outside bg
			y = Random.Range (0+ adjust, height/2 - adjust);
		}
	}

	void rerollX(){
		print("entered rerollX");
			while((x+ (imageW/2))> width/2){ //reroll y value if y makes obstacle go outside bg
			y = Random.Range (0+ adjust, height/2 - adjust);
		}
	}


	bool CheckOverLap(Vector3 pos, List<Vector3> tempList){

		/*foreach (Vector3 p in posList) {
			if(Vector3.Distance(p, pos) < overlapTol){
				print("old positions overlap");
				return true;
		}
		}*/


		if(tempList.Count != 0){ //if tempList is not empty
			foreach (Vector3 p in tempList) {
			if(Vector3.Distance(p, pos) < overlapTol){
				print("new positions overlap");
				return true;
			}
			}
		}
		print("no overlap");
		return false;
	}

	public List<GameObject> CheckGameOver(List<GameObject> balls){
		//either loads gameover if last ball falls, or updates list of balls on screen

		height =transform.Find("b2/2").GetComponent<RectTransform>().rect.height;

		List<GameObject> newBalls = new List<GameObject>();

		if(balls.Count!= 0){ //not empty list
			foreach(GameObject b in balls){
				if(balls.Count > 1){ //more than 1 ball
					if(b.transform.position.y >= -(height/2)){ //if the ball is above edge of screen
						Debug.Log(string.Format("ball y position: {0}, -height: {1}", b.transform.position.y, -height));
						print("added newBall");
						newBalls.Add(b);  //add to new list ; rn continuously adds? DEBUG
					}
				}
				else {  //last ball
					if(b.transform.position.y <= -height){ //if last ball is below edge of screen
					bgscroll bg = gameObject.GetComponent<bgscroll>();
					
					SceneManager.LoadScene("GameOver");
					}
					else {
						newBalls.Add(b); //add last ball back
					}	
				}
			}
			return newBalls;
		}		
		
		//print("no balls in list");
		return newBalls;

	}
	
}

