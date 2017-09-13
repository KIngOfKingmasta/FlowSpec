using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prorab : MonoBehaviour {
	private Nakladnaya nakl;
	public int StroyMaterial;
	public static Prorab prorab1;


	private void Awake()
	{
		nakl = GetComponent<Nakladnaya>();
	}

	public void DobavilStroyMaterial()
	{
		StroyMaterial = nakl.count;
		Debug.Log("Стройматериала на складе: " + nakl.count);
		if (StroyMaterial >= 5)
		{
			Destroy(gameObject);
		}
	}


}
