using System;
using System.Collections.Generic;
using UnityEngine;

namespace Padoru.Sensors
{
	public interface ISensor
	{
		event Action<List<GameObject>> OnDetection;
	}
}
