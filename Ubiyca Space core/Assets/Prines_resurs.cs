using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prines_resurs : MonoBehaviour {

	private Nakladnaya nak;
	private Prorab prorab;
	public GameObject Korobka;
	public Transform Zavod;

	private void Awake()
	{
	    nak = GetComponent<Nakladnaya>();
		prorab = GetComponent<Prorab>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Pick Up"))
		{
			Destroy(other.gameObject);
//			Debug.Log("Столкнулся с " + other.gameObject.name);
			nak.count++;
			nak.UpdateNakladnya();
//			Debug.Log(nak.count);
			NovayaKorobka();
			prorab.DobavilStroyMaterial();
		}
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Pick Up"))
		{
			Destroy(other.gameObject);
//			Debug.Log("Столкнулся с " + other.gameObject.name);
			nak.count++;
			nak.UpdateNakladnya();
//			Debug.Log(nak.count);
			NovayaKorobka();
			prorab.DobavilStroyMaterial();
		}

	}

	private void NovayaKorobka()
	{
		Instantiate(Korobka, Zavod.transform.position, Zavod.transform.rotation);
	}
}
