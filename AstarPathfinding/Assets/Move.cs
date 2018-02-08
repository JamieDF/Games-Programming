using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

 private float movement = 1;

  void Update()
  {
	  transform.position = new Vector3(Mathf.PingPong(Time.time*movement, 10), transform.position.y, transform.position.z);
  }
	
}
