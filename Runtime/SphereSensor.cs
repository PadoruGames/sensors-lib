using System;
using System.Collections.Generic;
using Padoru.Core.Utils;
using UnityEngine;

using Debug = Padoru.Diagnostics.Debug;

namespace Padoru.Sensors
{
	public class SphereSensor : MonoBehaviour, ISensor
	{
		[SerializeField] private float radius = 2;
		[SerializeField] private float detectionInterval = 1f;
		[SerializeField] private LayerMask detectLayers;
		[SerializeField] private bool autoDetect = true;

		private Collider[] colliders = new Collider[100];
		private List<GameObject> results = new ();
		private Timer timer;

		public event Action<List<GameObject>> OnDetection;

		private void Awake()
		{
			if (autoDetect)
			{
				timer = new Timer(detectionInterval, _ => AutoDetect());
				timer.Start();
			}
		}

		private void OnDestroy()
		{
			if (timer != null)
			{
				timer.Stop();
			}
		}

		public List<GameObject> Detect()
		{
			var count = Physics.OverlapSphereNonAlloc(transform.position, radius, colliders, detectLayers);

			results.Clear();
			for (int i = 0; i < count; i++)
			{
				results.Add(colliders[i].gameObject);
			}

			return results;
		}

		private void AutoDetect()
		{
			Detect();
			
			OnDetection?.Invoke(results);
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(transform.position, radius);
		}
	}
}
