using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class FaceCam : MonoBehaviour {
	// Update is called once per frame
	void Update()
	{
		transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, 
				Camera.main.transform.rotation * Vector3.up);
	}

	private void LateUpdate()
	{
		transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
			Camera.main.transform.rotation * Vector3.up);
	}
}
