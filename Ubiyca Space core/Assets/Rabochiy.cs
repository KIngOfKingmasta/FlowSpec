using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabochiy : MonoBehaviour {

	public Transform GpsBase;
	public Transform PickedUp;
	public bool FreeHand = true;
	private Animator AnimRab;

	private bool isFacingRight = true;
//	public List<GameObject> Korobki = new List<GameObject>();
	public bool naIIIelKorobku = false;
	private Upravlenie_predpriyatiem na4alnik;
	private ManagerKorobok manager;
	


	private void Awake()
	{
		AnimRab = GetComponent<Animator>();
		na4alnik = GetComponentInParent<Upravlenie_predpriyatiem>();
		manager = GetComponentInParent<ManagerKorobok>();
	}

	private void Update()
	{

		if (naIIIelKorobku == false && manager.Korobki.Count <= 0 && na4alnik.NujnoBolIIIeKorobok == true)
		{
			Debug.Log("Ищу коробку");
			Poisk_korobki();
		}
		if (FreeHand == true && manager.Korobki.Count > 0 && na4alnik.NujnoBolIIIeKorobok == true)
		{

			for (int i = 0; manager.Korobki[i]; i++)
			{
				if (manager.Korobki[i].gameObject.CompareTag("Pick Up"))
				{
					transform.position = Vector2.MoveTowards(transform.position, manager.Korobki[i].transform.position, Time.deltaTime);



					if (manager.Korobki[i].transform.position.x > transform.position.x && isFacingRight == false)
					{
						Debug.Log("Иду вправо");
						Flip();
					}
					else if (manager.Korobki[i].transform.position.x < transform.position.x && isFacingRight == true)
					{
						Debug.Log("Иду влево");
						Flip();
					}

					if (manager.Korobki[i] != null)
					{
						break;
					}

				}
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
			else if (GpsBase.transform.position.x > transform.position.x && isFacingRight == false)
			{
				Debug.Log("Иду влево");
				Flip();
			}
		}
		if (transform.position == null)
		{
			Debug.Log("Мою коробку уже спиздили!");
			transform.position = Vector2.MoveTowards(transform.position, GpsBase.position, Time.deltaTime);
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
			manager.Korobki.Remove(other.gameObject);
			Debug.Log("Осталось принести коробок :" + manager.Korobki.Count);
			if (manager.Korobki.Count <= 0)
			{
				naIIIelKorobku = false;
			}
		}
	}


	void Poisk_korobki()
	{

		//	Ray2D rayRight = new Ray2D(PickedUp.position, Vector2.right);
		//	Ray2D rayLeft = new Ray2D(PickedUp.position, Vector2.left);
		Debug.Log("Нужно принести коробок на базу: " + na4alnik.NujnoKorobok);
		RaycastHit2D[] hitsR = Physics2D.RaycastAll(PickedUp.position, Vector2.right);
		RaycastHit2D[] hitsL = Physics2D.RaycastAll(PickedUp.position, Vector2.left);
		Debug.DrawRay(PickedUp.position, Vector2.left);
		Debug.DrawRay(PickedUp.position, Vector2.right);
		foreach (RaycastHit2D hit in hitsR)
		{
			if (hit.collider.gameObject.tag == "Pick Up")
			{
				naIIIelKorobku = true;
				manager.Korobki.Add(hit.collider.gameObject);
				//				float distans = Mathf.Abs(transform.position.x-hit.distance);
				//				Debug.Log("Расстояние до :" + hit.collider.name + distans);

			}
		}
		foreach (RaycastHit2D hit in hitsL)
		{
			if (hit.collider.gameObject.tag == "Pick Up")
			{
				naIIIelKorobku = true;
				manager.Korobki.Add(hit.collider.gameObject);
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
