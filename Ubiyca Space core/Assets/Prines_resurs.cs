using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prines_resurs : MonoBehaviour {

	private Nakladnaya nak;

	private void Awake()
	{
		nak = GetComponent<Nakladnaya>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Pick Up"))
		{
			Destroy(other.gameObject);
			Debug.Log("Столкнулся с " + other.gameObject.name);
			nak.count++;
			nak.UpdateNakladnya();
			Debug.Log(nak.count);
		}
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Pick Up"))
		{
			Destroy(other.gameObject);
			Debug.Log("Столкнулся с " + other.gameObject.name);
			nak.count++;
			nak.UpdateNakladnya();
			Debug.Log(nak.count);
			
		}

	}

}
