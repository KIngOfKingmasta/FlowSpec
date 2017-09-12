using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Podnimu_Korobku : MonoBehaviour {

	public Podnimu_Korobku korobka;

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{ }

	}


}
