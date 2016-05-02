using UnityEngine;
using System.Collections;

public class Gizmoforspawn : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDrawGizmos()
    {

        Gizmos.color = Color.blue;

        Gizmos.DrawSphere(this.gameObject.transform.position, 0.2f);

    }
}
