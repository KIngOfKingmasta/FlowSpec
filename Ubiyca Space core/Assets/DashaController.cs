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

	void Update ()
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
			animator.Play ("Player_JumpHold");
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

		if (Input.GetKeyDown(KeyCode.E) && PickUp_rdy == true)
		{
			PickUP_object();
			animator.SetTrigger("use");
		}
		else if (Input.GetKeyDown(KeyCode.E)) 
		{
			animator.SetTrigger("use");
		}
	}
	private void FixedUpdate()
	{

	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag("Ground"))
		{
			Debug.Log("collide with " + other.gameObject.name);
			Grounded = true;
		}
		else if (other.gameObject.CompareTag("Pick Up") && PickUp_rdy == false)
		{
			Debug.Log("collide with " + other.gameObject.name);
			PickUp_rdy = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Ground"))
		{
			Debug.Log("collide with " + other.gameObject.name);
			Grounded = false;
		}
		else if (other.gameObject.CompareTag("Pick Up") && PickUp_rdy == true)
		{
			PickUp_rdy = false;
		}
	}


	private void Flip()
	{
		isFacingRight = !isFacingRight;
		Vector2 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	private void PickUP_object()
	{

	}
}
