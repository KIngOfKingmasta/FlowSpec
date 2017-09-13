using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveer : MonoBehaviour {

	public static Conveer zavod;
	public GameObject Korobka;

	public void novaya_korobka()
	{
	   Instantiate(Korobka, gameObject.transform.position, gameObject.transform.rotation);
	}


}
