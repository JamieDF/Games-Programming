using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
 public int numSelectors = 5;
    public GameObject[] selectorArr;
    public GameObject selector; //selected in the editor
	public bool expload;

    void Start()
    {
        selectorArr = new GameObject[numSelectors];
        for (int i = 0; i < numSelectors; i++)
        {
            GameObject go = Instantiate(Resources.Load ("star_prefab")) as GameObject;
            go.transform.localScale = Vector3.one;
            selectorArr[i] = go;
        }
		
	}

			// Update is called once per frame
	void Update () {
			if (Input.GetKeyDown("space")){
				print("space key was pressed");
				expload = true;
			}

			if (expload){
				for(int i=0; i < numSelectors; i++){
					selectorArr[i].transform.Translate(Vector3.up * Time.deltaTime, Space.World);
				}

			}

	}

	void fixedUpdate(){

		
	}
		


}

