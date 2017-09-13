using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashaController : MonoBehaviour {

	private SpriteRenderer spriteRenderer;
	private Animator animator;

	public bool Grounded = false;
	private int speed = 10;
	private Rigidbody2D rb;
	private float Jump = 50f;
	private bool isFacingRight = true;
	public bool Free_hand = true;
	public bool PickUp_rdy;

	public Transform PickUp;



	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		PickUp_rdy = false;
	}

	void Update()
	{

		float moveHorizontal = Input.GetAxis("Horizontal");

		Vector2 movement = new Vector2(moveHorizontal, 0.0f);
		rb.velocity = movement * speed;

		if (Grounded == true)
		{
			animator.SetTrigger("grounded");
		}
		else
		{
			animator.Play("Player_JumpHold");
		}
		if (Input.GetKeyDown("space") && Grounded == true)
		{
			Debug.Log("jump");
			rb.AddForce(Vector2.up * 50000);
		}
		if (moveHorizontal > 0 && !isFacingRight)
			Flip();
		else if (moveHorizontal < 0 && isFacingRight)
			Flip();

		if (Input.GetKeyDown(KeyCode.E) && Free_hand == true)
		{
			animator.SetTrigger("use");
		}

	}
	private void FixedUpdate()
	{

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag ("Pick Up") && Free_hand == true)
		{
//			Debug.Log("collide with " + other.gameObject.name);
			Grounded = true;
		}
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Pick Up"))
			PickUp_rdy = true;
//			Debug.Log("Столкнулся с  " + collision.gameObject.name);		
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Pick Up"))
		{
			if ((Input.GetKeyDown(KeyCode.E)) && Free_hand == true && PickUp_rdy == true)
			{
	//			Debug.Log("Беру Ящик " + collision.gameObject.name);
				animator.SetTrigger("use");
				collision.gameObject.transform.position = PickUp.transform.position;
				collision.gameObject.transform.parent = gameObject.transform;
				collision.rigidbody.isKinematic = true;
				collision.rigidbody.GetComponent<Collider2D>().isTrigger = true;
				collision.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
				collision.gameObject.tag = ("Picked");
				Free_hand = false;
				PickUp_rdy = false;
			}
		}
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Picked"))
		{
			if ((Input.GetKeyDown(KeyCode.Q)) && Free_hand == false)
			{
				other.gameObject.transform.parent = null;
				other.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
				other.gameObject.GetComponent<Collider2D>().isTrigger = false;
				other.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
				other.gameObject.tag = ("Pick Up");
				Free_hand = true;
				if (Grounded == false)
				{
					Grounded = true;
				}

			}
		}
	}

	private void OnCollisionExit2D(Collision2D other)
	{ 
	  if (other.gameObject.CompareTag("Pick Up") && PickUp_rdy == true)
		{
			PickUp_rdy = false;
		}
	}


	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Pick Up"))
		{
//			Debug.Log("collide with " + other.gameObject.name);
			Grounded = false;
		}
	}


	private void Flip()
	{
		isFacingRight = !isFacingRight;
		Vector2 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

}
