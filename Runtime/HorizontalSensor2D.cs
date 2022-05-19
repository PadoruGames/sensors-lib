using System;
using System.Collections.Generic;
using UnityEngine;
using Padoru.Core.Utils;

namespace Padoru.Sensors
{
	public class HorizontalSensor2D : MonoBehaviour, ISensor
	{
		[SerializeField] private Vector2 boxSize = new Vector2(2, 2);
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
			var count = Physics2D.OverlapBoxNonAlloc(transform.position, boxSize, 0, colliders, detectLayers);

			results.Clear();
			for (int i = 0; i < count; i++)
			{
				results.Add(colliders[i].gameObject);
			}

			OnDetection?.Invoke(results);
		}

		private void OnDrawGizmosSelected()
		{
			var center = transform.position;
			var pointA = new Vector2(center.x - boxSize.x / 2f, center.y - boxSize.y / 2f);
			var pointB = new Vector2(center.x - boxSize.x / 2f, center.y + boxSize.y / 2f);
			var pointC = new Vector2(center.x + boxSize.x / 2f, center.y + boxSize.y / 2f);
			var pointD = new Vector2(center.x + boxSize.x / 2f, center.y - boxSize.y / 2f);

			Gizmos.color = Color.yellow;
			Gizmos.DrawLine(pointA, pointB);
			Gizmos.DrawLine(pointB, pointC);
			Gizmos.DrawLine(pointC, pointD);
			Gizmos.DrawLine(pointD, pointA);
		}
	}
}
