using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour {

	//physics and movemnt
	public GameObject[] particleObjArr;
	public GameObject particleObject;
	public GameObject cube;
	
	Vector3 velocity;
	Vector3[] velocityArr = new Vector3[particleNo];	
	Vector3 cubevelocity;
	Vector3 gravity = new Vector3(0, -9.8f, 0);

	public static int particleNo = 1000;	
	public int timeOut  = 0;
	
 	public bool explode;
	public bool dropCube;


	// Use this for initialization
	void Start () 
	{
		cube = Instantiate(Resources.Load ("cube")) as GameObject;
		particleObjArr = new GameObject[particleNo];

        for (int i = 0; i < particleNo; i++)
        {
			//loads in the picture as the game object
			particleObject = Instantiate(Resources.Load ("particle")) as GameObject;
			particleObject.transform.position = new Vector3(0,0,0);

			//adds the obj to the array and hides it on screen
			particleObjArr[i] = particleObject;
			particleObjArr[i].SetActive(false);
			
			//arr of random vectors for each partical
			velocityArr[i] = new Vector3(Random.Range(-10.0f, 20.0f),Random.Range(-5.0f, 20.0f),Random.Range(-10.0f, 20.0f));
        }
	}
	
	void FixedUpdate(){
		//press to being scene
		if (Input.GetKeyDown("space")){
				dropCube = true;
		}
		//cube is falling
		if(dropCube){
			cubeFall();
		}
		//partical is exploading
		if (explode){
			exploading();
		}
		//after explosion happens for a while destroy all particals to save memory
		if (timeOut == 160){
			explode = false;
			for(int i=0; i < particleNo; i++){
				Destroy(particleObjArr[i]);
			}
		}

	}

	//drops the cube at the start of the scene into positon then sets it to false and begins the explosion
	void cubeFall(){

		if(cube.transform.position.y <=0){
				dropCube = false;
				explode =true;
				cube.SetActive(false);
			}
			else{
				moveObj(ref cubevelocity, ref cube);

			}
	}
	
	//sets all particals to active then is continually called which then mvoes them
	void exploading(){
			timeOut++;
			for(int i=0; i < particleNo; i++){
				particleObjArr[i].SetActive(true);
				moveObj(ref velocityArr[i], ref particleObjArr[i]);
			}

	}

	//applies gravity and changes velocity then moves obj its passed
	void moveObj(ref Vector3 vel,ref GameObject obj){
					vel = vel + gravity * Time.fixedDeltaTime;
					obj.transform.Translate(vel * Time.fixedDeltaTime);
	}

}

