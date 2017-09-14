using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upravlenie_predpriyatiem : MonoBehaviour {
	public GameObject[] Rabochie ;
	public Transform Basa;
	public Text Korobok_na_base;
	private int count_na_Base;

	private void Awake()
	{
		count_na_Base = 0;
		Korobok_na_base.text = "Коробок на базе: " + count_na_Base;
	}

	public void UpdateBase()
	{
		Korobok_na_base.text = "Коробок на базе: " + count_na_Base;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Picked"))
		{
			Destroy(other.gameObject);
			count_na_Base++;
			UpdateBase();
		}
	}
}
