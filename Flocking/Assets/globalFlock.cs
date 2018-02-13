using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalFlock : MonoBehaviour {

	//used for the demo to toggle 3d
	public bool threeDimensional;

	// Use this for initialization
	public GameObject BoidPrefab;

	//area of movemnt
	public static int space = 20;

	//number of spiders in this case
    static int numBoids = 10;
    public static GameObject[] allBoids = new GameObject[numBoids];
	public static Vector3 centerSpace = Vector3.zero;



	// Use this for initialization
	void Start () {

		if(threeDimensional){
			//if 3d then hide the plane so they cant walk on it
			GameObject.Find("Plane").SetActive(false);
		}

		//loops through and makes how every many instanaces of the boids,  and spawns them in a certain location
        for (int i = 0; i < numBoids; i++) {
			Vector3 pos = new Vector3(Random.Range(-space, space), 0 ,Random.Range(-space, space));
			
			if(threeDimensional){
				//if 3d then they can go on a random y cordinate too
				pos.y = Random.Range(-space, space);
			}

			//add them to and array of gameObjects and rotates them to a randrom roation
            allBoids[i] = (GameObject) Instantiate(BoidPrefab, pos, Quaternion.identity);
			allBoids[i].transform.rotation =  Quaternion.Euler(0, Random.Range(0, 360),0);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
		//if one in 10 chance the move the certer space to change the rough direction of all of them
		if (Random.Range (0, 10) < 1) {
			
			if(threeDimensional)
				centerSpace = new Vector3 (Random.Range (-space, space),Random.Range (-space, space),Random.Range (-space, space));
			else
				centerSpace = new Vector3 (Random.Range (-space, space),0,Random.Range (-space, space));	
		}
	}
}