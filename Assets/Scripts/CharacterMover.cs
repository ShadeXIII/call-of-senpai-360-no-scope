using UnityEngine;
using System.Collections;


public class CharacterMover : MonoBehaviour
{
	[SerializeField]
	float m_fAcceleration;
	
	[SerializeField]
	float m_fMaxSpeed;
	
	[SerializeField]
	float m_fJumpSpeed;

	Rigidbody m_Body;
	Animator m_Animator;

	private Vector3 m_vecCurrMoveDir;

	void Start() {
		m_Body = gameObject.GetComponentInChildren<Rigidbody> ();
		m_Animator = gameObject.GetComponentInChildren<Animator> ();
	}

	void FixedUpdate() {
		
		// Since moving can affect physics, only do it on FixedUpdate()
		ReadInput ();
		HandleTranslation ();
		
	}

	private void ReadInput() {
		
		m_vecCurrMoveDir = Vector3.zero;
		
		if (Input.GetKey(KeyCode.D))
		{
			//move right
			m_vecCurrMoveDir.x = 1.0f;
		}
		if (Input.GetKey(KeyCode.A))
		{
			//move left	
			m_vecCurrMoveDir.x = -1.0f;
		}
		if (Input.GetKey(KeyCode.W))
		{
			m_vecCurrMoveDir.z = 1.0f;
		}
		
		if (Input.GetKey(KeyCode.S))
		{
			m_vecCurrMoveDir.z = -1.0f;
		}

		m_vecCurrMoveDir.Normalize ();
		
	}
	
	private void HandleTranslation() {

		Vector3 worldMoveDir = transform.TransformDirection (m_vecCurrMoveDir);
		Vector3 horizontalVelocity = new Vector3 (m_Body.velocity.x, 0.0f, m_Body.velocity.z);

		float fVelInDir = Vector3.Dot (worldMoveDir, horizontalVelocity);
		float fVelCap = m_fMaxSpeed - fVelInDir;

		m_Body.velocity += worldMoveDir * Mathf.Min( fVelCap, m_fAcceleration * Time.fixedDeltaTime );

		bool bGrounded = Physics.Raycast (transform.position, Vector3.down, 1.1f);
		Debug.DrawLine (transform.position, transform.position + Vector3.down * 1.1f);

		if (Input.GetKeyDown(KeyCode.Space) && bGrounded)
		{
			m_Body.velocity = new Vector3( m_Body.velocity.x, m_fJumpSpeed, m_Body.velocity.z );
		}
		
		// Update animation.
		m_Animator.SetBool ("Grounded", bGrounded);
		m_Animator.SetFloat ("Speed", horizontalVelocity.magnitude / m_fMaxSpeed );
	}

}

