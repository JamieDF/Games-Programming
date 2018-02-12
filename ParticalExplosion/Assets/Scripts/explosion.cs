using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour {

	//physics and movemnt
	
	Vector3 velocity;
	Vector3[] velocityArr = new Vector3[numSelectors];	
	Vector3 cubevelocity;
	Vector3 gravity = new Vector3(0, -9.8f, 0);

	public static string selector = "particle";
	public static int numSelectors = 1000;	
	public int deteriorate  = 0;
	
 	public bool expload;
	public bool falling;

	public GameObject[] selectorArr;
	public GameObject particleObject;
	public GameObject cube;



	// Use this for initialization
	void Start () 
	{
		cube = Instantiate(Resources.Load ("cube")) as GameObject;
		
		selectorArr = new GameObject[numSelectors];
        for (int i = 0; i < numSelectors; i++)
        {
            particleObject = Instantiate(Resources.Load (selector)) as GameObject;
            particleObject.transform.localScale = Vector3.one;
			particleObject.transform.position = new Vector3(0,0,0);

			selectorArr[i] = particleObject;
			selectorArr[i].SetActive(false);
			
			velocityArr[i] = new Vector3(Random.Range(-10.0f, 20.0f),Random.Range(-5.0f, 20.0f),Random.Range(-10.0f, 20.0f));
        }
	}
	
	void FixedUpdate(){

		if (Input.GetKeyDown("space")){
				print("space key was pressed");
				falling = true;
		}

		if(falling){
			cubeFall();
		}
		//if ball is in motion
		if (expload){
			exploading();
		}

		if (deteriorate == 160){
			expload = false;
			for(int i=0; i < numSelectors; i++){
				Destroy(selectorArr[i]);
			}
		}

	}

	void cubeFall(){

		if(cube.transform.position.y <=0){
				falling = false;
				expload =true;
				cube.SetActive(false);
			}
			else{
				moveObj(ref cubevelocity, ref cube);

			}
	}
	

	void exploading(){

			deteriorate++;
			for(int i=0; i < numSelectors; i++){
				selectorArr[i].SetActive(true);
				moveObj(ref velocityArr[i], ref selectorArr[i]);
			}

	}

	void moveObj( ref Vector3 vel,ref GameObject obj){
					vel = vel + gravity * Time.fixedDeltaTime;
					obj.transform.Translate(vel * Time.fixedDeltaTime);
	}

}

