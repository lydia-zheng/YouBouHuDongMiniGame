using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bgscroll : MonoBehaviour {
	public float backgroundSize;
	
	public float speed = 0.5f;
	float start = 5.5f; 
	Vector2 offset = Vector2.zero;
	private float _timer = 0f;
	 Transform cameraTransform;
	 Transform [] layers;
	 
	private float viewZone = 2.5f; //need to adjust
	int leftIndex;
	int rightIndex;
	
	float width;
	randomize r;
	List<GameObject> balls = new List<GameObject>();

        

 public void Start(){
	r = gameObject.GetComponent<randomize>();

	cameraTransform = Camera.main.transform;
	layers = new Transform[transform.childCount];
	for(int i = 0; i < transform.childCount; i++){
		layers[i] = transform.GetChild(i);

		leftIndex = 0;
		rightIndex = layers.Length - 1;
	}	
	
	
	foreach( Ball gameObj in GameObject.FindObjectsOfType<Ball>()) //find GameObjects that are balls
		{
						
			
				//print("found ball obj");
				if(!balls.Contains(gameObj.gameObject)){			//if ball is not in list
					print("added ball to list");

					balls.Add(gameObj.gameObject);						//add them to list
				}
			
			
		}
		transform.position = Vector3.zero;
 }
	
private void ScrollLeft() //doesn't wrap around
{
	//int lastRight = rightIndex;
	layers[rightIndex].position = Vector3.right * (layers[leftIndex].position.x -backgroundSize);
	leftIndex = rightIndex;
	rightIndex--;
	if(rightIndex < 0)
		rightIndex =layers.Length -1;
}

private void ScrollRight() 
{   
	ObjectPool pool = gameObject.GetComponent<ObjectPool>();
	

	pool.setUp();
	r.Level();

	//int lastLeft = leftIndex;
	layers[leftIndex].position = Vector3.right * (layers[rightIndex].position.x + backgroundSize);
	rightIndex = leftIndex;
	leftIndex++;
	if(leftIndex == layers.Length)
		leftIndex =0;
	pool.CleanUp(layers[leftIndex]); //clean left bg;
	
}



private void Update () {
		
	
		balls = r.CheckGameOver(balls); //get new list of balls from function; only game over when all balls leave screen

	    if(cameraTransform.position.x > (layers[rightIndex].transform.position.x + viewZone)){
		
			ScrollRight();
			
		}
	    _timer += Time.deltaTime;
		
		// if(restarted){     //DEBUG
		// 	print("entered restarted = true case");
		// 	transform.position = new Vector3 (0,0,0); //reset bg position to beginning of scene; vector3 zero
		// 	restarted = false;
		// }
		
			offset.x =start - _timer * speed;

			transform.position = new Vector3 (offset.x, offset.y, transform.position.z);
	
	}
}
