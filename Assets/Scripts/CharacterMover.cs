using UnityEngine;
using System.Collections;


public class CharacterMover : MonoBehaviour
{
	[SerializeField]
	float m_fAcceleration;
	
	[SerializeField]
	float m_fMaxSpeed;
	
	[SerializeField]
	float m_fJumpPower;
	
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
		
		Vector3 vecWorldSpaceMove = m_vecCurrMoveDir * m_fAcceleration;
		float fScale = Mathf.Max( (m_fMaxSpeed - m_Body.velocity.magnitude) / m_fMaxSpeed, 0.0f ); 
		
		m_Body.AddRelativeForce (vecWorldSpaceMove * fScale);
		
		Debug.Log (m_Body.velocity.magnitude);
		
		if (Input.GetKeyDown(KeyCode.Space))
		{
			m_Body.AddRelativeForce( Vector3.up * m_fJumpPower );
		}
		
		// Update animation.
		m_Animator.SetFloat ("Speed", Mathf.Max( m_Body.velocity.magnitude / m_fMaxSpeed, 0.0001f ) );
	}
	
}

