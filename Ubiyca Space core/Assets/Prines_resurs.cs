using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prines_resurs : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Pick Up"))
			Destroy(other.gameObject);
	}
}
