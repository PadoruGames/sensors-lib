using System;
using System.Collections.Generic;
using UnityEngine;
using Padoru.Core.Utils;

namespace Padoru.Sensors
{
	public class CircleSensor2D : MonoBehaviour, ISensor
	{
		[SerializeField] private float radius = 2;
		[SerializeField] private float detectionInterval = 1f;
		[SerializeField] private LayerMask detectLayers;

		private Collider2D[] colliders = new Collider2D[100];
		private List<GameObject> results = new List<GameObject>();
		private Timer timer;

		public event Action<List<GameObject>> OnDetection;

		private void Awake()
		{
			timer = new Timer(detectionInterval, Detect);
			timer.Start();
		}

		private void OnDestroy()
		{
			timer.Stop();
		}

		private void Detect(float deltaTime)
		{
			var count = Physics2D.OverlapCircleNonAlloc(transform.position, radius, colliders, detectLayers);

			results.Clear();
			for (int i = 0; i < count; i++)
			{
				results.Add(colliders[i].gameObject);
			}

			OnDetection?.Invoke(results);
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(transform.position, radius);
		}
	}
}