using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorList : MonoBehaviour {
	public GameObject[] doors;

	// Update is called once per frame
	public int getSize(){
		return doors.Length;
	}

	public string getNames(){
		string names = "\n";

		if (doors.Length != 0) {
			foreach (GameObject door in doors) {
				//Debug.Log (door.name);
				names += door.name + "\n";
			}
		}

		return names;
	}

	public Door getDoor(int index){
		return doors [index].GetComponent<Door>();	// return door that corresponds with given index
	}
}
