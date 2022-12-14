using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class BillboardUI : MonoBehaviour
{
	private Transform camTransform;

	private void Awake()
	{
		if (Camera.main)
		{
			camTransform = Camera.main.transform;
			if (TryGetComponent(out Canvas canvas) && !canvas.worldCamera)
				canvas.worldCamera = Camera.main;
		}
		else
			Debug.LogError("Main Camera is empty!");
	}

	private void LateUpdate()
	{
		transform.LookAt(transform.position + camTransform.rotation * Vector3.forward, camTransform.rotation * Vector3.up);
	}
}
