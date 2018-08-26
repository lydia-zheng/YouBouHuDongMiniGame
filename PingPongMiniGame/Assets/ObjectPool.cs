using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {
	//NOTE: Enum can't be used as actual type implementation; only as tag to restrict types (like restricted int)
	
	List<GameObject> circles= new List<GameObject>();
	List<GameObject> rectangles = new List<GameObject>(); //no way to keep apparent type in list and still work with dictionary???
	List<GameObject> pentagons = new List<GameObject>();
	Dictionary<string, List<GameObject>> pool;

	public Object circlePrefab;
	public Object rectanglePrefab;
	public Object pentagonPrefab;

	int cVar; //detemine # of circles
	int rVar; //		# of rectangles
	int pVar; //        # of pentagons

	public int minNumObstacle = 5; //minimum # of obstacles on screen
	Transform obstacles;
	GameObject Poolob;
	
	
	//Object Pool
//Implement dictionary: key () -> get list of type<shape>


	// Use this for initialization
	public void setUp () {
		pool = new Dictionary<string, List<GameObject>>();
		
		Poolob = new GameObject();

		pool.Add("c", circles);
		pool.Add("r", rectangles);
		pool.Add("p", pentagons);

		create();
	}
	
	
	void create() {
		cVar = 0;
		rVar = 0;
		pVar = 0;
		roll();

		
		
		while(cVar + rVar + pVar < minNumObstacle){  
			roll(); //reroll to make sure total # of objects is no smaller than 3; 
		}

		Debug.Log(string.Format("cVar: {0}, rVar: {1}, pVar: {2}", cVar, rVar, pVar));
		
			createNew("c", cVar);
			createNew("r", rVar);
			createNew("p", pVar);
	}

	void roll (){
		cVar = Random.Range(0,3);
		rVar = Random.Range(0,3);
		pVar = Random.Range(0,3);
	}

	public void CleanUp (Transform bg){
			//need to setActive(false) for all objects not used ; if not on screen
			string n = bg.gameObject.name;
				Debug.Log(string. Format("bg name: {0}", n));
			switch(n){
				case "b1":
					obstacles = bg.Find("1/obstacles1");
					Debug.Log(string. Format("obstacles1-CLEAN"));
				
				break;

				case "b2":
					obstacles = bg.Find("2/obstacles2");
					Debug.Log(string. Format("obstacles2-CLEAN"));
					
				break;

				case "b3":
					obstacles = bg.Find("3/obstacles3");
					Debug.Log(string. Format("obstacles3-CLEAN"));
				
				break;
				
			
			}

			foreach(Transform o in obstacles){
				o.gameObject.SetActive(false);
				MultipleBall buff = o.GetComponent<MultipleBall>();
				if(buff != null){
					Destroy(buff);
				}
				o.transform.SetParent(Poolob.transform); //Pool is empty object
			}
			
	}
	void addBuff(GameObject obj, List<GameObject> l){
			float possibility = Random.Range(0.0f, 1.0f);
			//Debug.Log(string.Format("possibility: {0}", possibility));
			l. Add(obj); //add to list
			Debug.Log(string.Format("object is in list"));

			if(possibility < 0.3){
				
				
				obj.GetComponent<SpriteRenderer>().color = Color.black; //should turn obj to black
				obj.AddComponent<MultipleBall>();
								
					 //l.Remove(obj); //if an existing obj in list is buffed, remove it from list
					 //Debug.Log(string.Format("object is in removed from list"));
				
				
			}

			

			
			
		}

	void createNew(string typeKey, int num){
		//function for creating new objs: if list of type<shape> has object, setActive(true), put in background
		//								else create new object, put in background
		// Destroy has high cost -- when obj not needed, setActive(false), put into list of type<shape>;
		//Need to make sure all created object names are 'GameObject', due to ball script
		
		randomize rando = gameObject.GetComponent<randomize>();
		obstacles = rando.getObstacles();
		Debug.Log(string. Format("obstacle Name: {0}", obstacles.gameObject.name));
		List<GameObject> l = pool[typeKey]; 
		// foreach(GameObject g in l){
		// 	if (g == null){
		// 		l.Remove(g);
		// 	}
		// }


		if(obstacles.childCount > 0){
			foreach (Transform gObj in obstacles){ //add existing shapes to list (by type)
				if(gObj.gameObject.CompareTag("c")){
					pool["c"].Add(gObj.gameObject);
				}
				else if(gObj.gameObject.CompareTag("r")){
					pool["r"].Add(gObj.gameObject);
				}
				else pool["p"].Add(gObj.gameObject);
		 }
		}

	
		

		//if(pool.TryGetValue(typeKey, out temp)){
		//	print("Invalid typeKey given to createNew"); //shouldn't return null
		//}
		//else
	

			if(l.Count == 0) //if list is empty, add shape num times ; This case MAY BE REDUNDANT
			{
				switch(typeKey){
				case "c": 
					for(int i = 0; i < num; ++i){
						GameObject c = (GameObject) Instantiate(circlePrefab); //instantiate clones prefab
						c.name = "GameObject";
						c.transform.SetParent(obstacles);
						addBuff(c,l);
					}
					break;

				case "r":
					for( ; num!=0; num--){ //no initialization is ok for for statement?
						GameObject r = (GameObject)Instantiate(rectanglePrefab);
						r.name = "GameObject";
						r.transform.SetParent(obstacles);
						addBuff(r, l);
					
					}

					break;

				case "p":
					for(int i = 0; i < num; ++i){
						GameObject p = (GameObject)Instantiate(pentagonPrefab);
						p.name = "GameObject";
						p.transform.SetParent(obstacles);
						addBuff(p, l);
					}
					break;

				default: break;	
			}
			

			}
			else { //there's something in list
				int count = 0 ;
				foreach (Transform gObj in obstacles){
					if(gObj.gameObject.CompareTag(typeKey)){ //find # of shape in THIS background
						count++;
					} 
						
				}
				if(count >= num){ //list has more/equal to num; excess 
					for(int i= 0; i < num; i++){ 
						GameObject g = l[i];
						MultipleBall buff = g.GetComponent<MultipleBall>();
						if(buff== null){ //only regular shapes are set to active
							g.SetActive(true);
						}
					}
				}
				
				else if (count < num){ //list has fewer elements than num
					for(int i= 0; i < count; i++){ 
						GameObject g = l[i];
						MultipleBall buff = g.GetComponent<MultipleBall>();
						if(buff== null){ //only regular shapes are set to active
							g.SetActive(true);
						}		
					}
					switch(typeKey){
					case "c": 
						for(int i = 0; i < num-count ; ++i){
							GameObject c = (GameObject)Instantiate(circlePrefab);
							c.name = "GameObject";
							c.transform.SetParent(obstacles);
							addBuff(c,l);
						}
						break;

					case "r":
						for(int i = 0; i < num-count ; ++i){
							GameObject r = (GameObject)Instantiate(rectanglePrefab);
							r.name = "GameObject";
							r.transform.SetParent(obstacles);
							addBuff(r, l);
						}

						break;

					case "p":
						for(int i = 0; i < num-count ; ++i){
							GameObject p = (GameObject)Instantiate(pentagonPrefab);
							p.name = "GameObject";
							p.transform.SetParent(obstacles);
							addBuff(p, l);
						}
						break;

						
					}
					
				}
		}
					
		}

	

	

}
