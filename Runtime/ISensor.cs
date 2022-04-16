using System;
using UnityEngine;

namespace Padoru.Sensors
{
	public interface ISensor
	{
		event Action<GameObject[]> OnDetection;
	}
}
