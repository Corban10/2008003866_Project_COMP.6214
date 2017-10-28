using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosionscript : MonoBehaviour 
{
	void Start () 
	{
		StartCoroutine("WaitBeforeDestroy");
	}
	IEnumerator WaitBeforeDestroy()
	{
		yield return new WaitForSeconds(2);
		Destroy(gameObject);
	}
}
