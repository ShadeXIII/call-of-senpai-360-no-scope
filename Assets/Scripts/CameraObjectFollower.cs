using UnityEngine;
using System.Collections;

public class CameraObjectFollower : MonoBehaviour {

	[SerializeField]
	GameObject m_gobTarget;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = m_gobTarget.transform.position;
	}
}
