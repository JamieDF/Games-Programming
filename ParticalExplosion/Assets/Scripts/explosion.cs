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
	
 	public bool expload;
	public bool dropCube;


	// Use this for initialization
	void Start () 
	{
		cube = Instantiate(Resources.Load ("cube")) as GameObject;

		
		particleObjArr = new GameObject[particleNo];
        for (int i = 0; i < particleNo; i++)
        {
			particleObject = Instantiate(Resources.Load ("particle")) as GameObject;
			
			particleObject.transform.position = new Vector3(0,0,0);

			particleObjArr[i] = particleObject;
			particleObjArr[i].SetActive(false);
			
			velocityArr[i] = new Vector3(Random.Range(-10.0f, 20.0f),Random.Range(-5.0f, 20.0f),Random.Range(-10.0f, 20.0f));
        }
	}
	
	void FixedUpdate(){

		if (Input.GetKeyDown("space")){
				dropCube = true;
		}

		if(dropCube){
			cubeFall();
		}
		
		if (expload){
			exploading();
		}

		if (timeOut == 160){
			expload = false;
			for(int i=0; i < particleNo; i++){
				Destroy(particleObjArr[i]);
			}
		}

	}

	void cubeFall(){

		if(cube.transform.position.y <=0){
				dropCube = false;
				expload =true;
				cube.SetActive(false);
			}
			else{
				moveObj(ref cubevelocity, ref cube);

			}
	}
	

	void exploading(){

			timeOut++;
			for(int i=0; i < particleNo; i++){
				particleObjArr[i].SetActive(true);
				moveObj(ref velocityArr[i], ref particleObjArr[i]);
			}

	}

	void moveObj(ref Vector3 vel,ref GameObject obj){
					vel = vel + gravity * Time.fixedDeltaTime;
					obj.transform.Translate(vel * Time.fixedDeltaTime);
	}

}

