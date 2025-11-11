using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float maxSpeed = 10f;
	bool facingRight = true;

	Rigidbody2D r2d;
	Animator anim;

	public bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float jumpForce = 700.0f;

	// Use this for initialization
	void Start () {
		r2d = GetComponent<Rigidbody2D>	();
		anim = GetComponent<Animator>();
	}
	
	void Update(){
		if( grounded && Input.GetKeyDown(KeyCode.Space) ){
			anim.SetBool("Ground", false);
			r2d.AddForce(new Vector2(0, jumpForce));
		}
	}

	void FixedUpdate () {

		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool("Ground", grounded);

		anim.SetFloat("vSpeed", r2d.linearVelocity.y);

		float move = Input.GetAxis("Horizontal");

		anim.SetFloat("Speed", Mathf.Abs(move));

		r2d.linearVelocity = new Vector2(move*maxSpeed, r2d.linearVelocity.y);

		if(move > 0 && !facingRight)
			Flip();
		else if( move < 0 && facingRight )
			Flip();
	}

	void Flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
