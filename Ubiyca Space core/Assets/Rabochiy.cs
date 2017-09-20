using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabochiy : MonoBehaviour {

	public Transform GpsBase;
	public Transform Korobka;
	public Transform PickedUp;
	public bool FreeHand = true;
	private Animator AnimRab;

	private bool isFacingRight = true;
	List<GameObject> Korobki = new List<GameObject>();
	public bool naIIIelKorobku = false;
	


	private void Awake()
	{
		AnimRab = GetComponent<Animator>();
	}

	private void Update()
	{

		if (naIIIelKorobku == false && Korobki.Count <= 0)
		{
			Debug.Log("Ищу коробку");
			Poisk_korobki();
		}
		if (FreeHand == true && Korobki.Count >= 0)
		{
			for (int i = 0; Korobki[i]  ; i++)
			{
				transform.position = Vector2.MoveTowards(transform.position, Korobki[i].transform.position, Time.deltaTime);

				if (Korobki[i].transform.position.x > transform.position.x && isFacingRight == false)
				{
					Debug.Log("Иду вправо");
					Flip();
				}
				
				if (Korobki[i] != null)
				{ break; }
			}
		
		}
		if (FreeHand == false)
		{
			transform.position = Vector2.MoveTowards(transform.position, GpsBase.position, Time.deltaTime);
			if (GpsBase.transform.position.x < transform.position.x && isFacingRight == true)
			{
				Debug.Log("Иду влево");
				Flip();
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Pick Up"))
		{
			FreeHand = false;
			other.gameObject.transform.position = PickedUp.transform.position;
			other.gameObject.transform.parent = gameObject.transform;
			other.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
			other.gameObject.GetComponent<Collider2D>().isTrigger = true;
			other.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
			other.gameObject.tag = ("Picked");
			
		}
	}
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Picked"))
		{
			FreeHand = true;
			Korobki.Remove(other.gameObject);
			Debug.Log(Korobki.Count);
			if (Korobki.Count <= 0)
			{
				naIIIelKorobku = false;
			}
		}
	}


	void Poisk_korobki()
	{

	//	Ray2D rayRight = new Ray2D(PickedUp.position, Vector2.right);
	//	Ray2D rayLeft = new Ray2D(PickedUp.position, Vector2.left);
		RaycastHit2D[] hits = Physics2D.RaycastAll(PickedUp.position, Vector2.right);
		Debug.DrawRay(PickedUp.position, Vector2.right);
		foreach (RaycastHit2D hit in hits)
		{
			if (hit.collider.gameObject.tag == "Pick Up")
			{
				naIIIelKorobku = true;
				Korobki.Add(hit.collider.gameObject);
				Debug.Log("Луч попал в : " + hit.collider.gameObject.name);
				Debug.Log("Всего в масиве " + Korobki.Count);
				//				float distans = Mathf.Abs(transform.position.x-hit.distance);
				//				Debug.Log("Расстояние до :" + hit.collider.name + distans);

			}
		}
	}

	private void Flip()
	{
		isFacingRight = !isFacingRight;
		Vector3 theScale = transform.localScale;
		theScale.z *= -1;
		transform.localScale = theScale;
	}



}
