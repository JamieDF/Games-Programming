using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalFlock : MonoBehaviour {

	// Use this for initialization
	public GameObject BoidPrefab;

	public static int space = 20;

    static int numBoids = 10;
    public static GameObject[] allBoids = new GameObject[numBoids];

	public static Vector3 centerSpace = Vector3.zero;


	// Use this for initialization
	void Start () {

        for (int i = 0; i < numBoids; i++) {
            Vector3 pos = new Vector3(Random.Range(-space, space), 0,Random.Range(-space, space));
            allBoids[i] = (GameObject) Instantiate(BoidPrefab, pos, Quaternion.identity);
			allBoids[i].transform.rotation =  Quaternion.Euler(0, Random.Range(0, 360),0);
        }
	}
	
	// Update is called once per frame
	void Update () {

		if (Random.Range (0, 100000) < 50) {
		
			centerSpace = new Vector3 (Random.Range (-space, space),Random.Range (0, 0),Random.Range (-space, space));	
		}
	}
}