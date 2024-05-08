using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jobs
{
	public class RotateJobs : MonoBehaviour
	{
		public float rotateSpeed;
		// Update is called once per frame
		void Update()
		{
			transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
		}
	}
}