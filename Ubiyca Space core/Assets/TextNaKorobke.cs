using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextNaKorobke : MonoBehaviour {

	public Text PressEtoPickUp;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player"))
		PressEtoPickUp.gameObject.SetActive(true);
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
			PressEtoPickUp.gameObject.SetActive(false);
	}
}
