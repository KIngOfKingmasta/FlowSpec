using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabochiy : MonoBehaviour {

	public Transform GpsBase;
	public Transform Korobka;
	public Transform PickedUp;
	public bool FreeHand = true;
	public float maxRaydistance;

	private void Update()
	{
		Poisk_korobki();

		if (FreeHand == true && Korobka != null)
		{
			transform.position = Vector2.MoveTowards(transform.position, Korobka.position, Time.deltaTime);
		}
		if (FreeHand == false)
		{
			transform.position = Vector2.MoveTowards(transform.position, GpsBase.position, Time.deltaTime);
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Pick Up"))
		{
			other.gameObject.transform.position = PickedUp.transform.position;
			other.gameObject.transform.parent = gameObject.transform;
			other.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
			other.gameObject.GetComponent<Collider2D>().isTrigger = true;
			other.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
			other.gameObject.tag = ("Picked");
			FreeHand = false;
		}
	}
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Picked"))
		{
			FreeHand = true;
		}
	}

	void Poisk_korobki()
	{
		Ray2D rayRight = new Ray2D(PickedUp.position, Vector2.right);
		Ray2D rayLeft = new Ray2D(PickedUp.position, Vector2.left);
		RaycastHit2D[] hits = Physics2D.RaycastAll(PickedUp.position, Vector2.right);
		Debug.DrawRay(PickedUp.position, Vector2.right);
		foreach (RaycastHit2D hit in hits)
		{
			if (hit.collider.gameObject.tag == "Pick Up")
			{
//				float distans = Mathf.Abs(transform.position.x-hit.distance);
//				Debug.Log("Расстояние до :" + hit.collider.name + distans);
				Destroy(hit.collider.gameObject);
			}
		}
	}

}
