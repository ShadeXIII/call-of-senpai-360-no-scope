using UnityEngine;
using System.Collections;

public class TempWeaponFire : MonoBehaviour {

	[SerializeField]
	GameObject m_gobProjectile;

	[SerializeField]
	GameObject m_gobOrigin;

	[SerializeField]
	float m_fFireRate;

	[SerializeField]
	float m_fProjectileSpeed;

	float m_fFireTimer;

	// Use this for initialization
	void Start () {
		m_fFireTimer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate() {

		m_fFireTimer += Time.fixedDeltaTime;
		
		if (Input.GetMouseButton (0) && m_fFireTimer > ( 1.0f / m_fFireRate ) ) {

			if( m_gobProjectile != null ) {
				GameObject newProjectile = GameObject.Instantiate( m_gobProjectile, m_gobOrigin.transform.position, m_gobOrigin.transform.rotation ) as GameObject;
				
				Rigidbody newBody = newProjectile.GetComponent< Rigidbody >();
				if( newBody != null ) 
				{
					newBody.velocity = newProjectile.transform.forward * m_fProjectileSpeed;
				}
				
				m_fFireTimer = 0.0f;
			}
			else {
				RaycastHit hitInfo = new RaycastHit();
				Physics.Raycast( m_gobOrigin.transform.position, m_gobOrigin.transform.forward, out hitInfo );
				if( hitInfo.collider != null ) {
					Health currHealth = GetComponent<Collider>().gameObject.GetComponent<Health>();
					if( currHealth != null )
						currHealth.TakeDamage(10);
				}
			}
		}
	}
}
