﻿using System;
using UnityEngine;

namespace Actors.NastyUFO.Buildings
{
	[Serializable]
	public struct BuildingData
	{
		[Tooltip("Модуль дома")]
		public BuildingFloor GroundFloorElement, MiddleFloorElement, RoofFloorElement;
		//Вставь нужные модули сверху и число элементов снизу 
		private const byte MODULES_COUNT = 3;
		
		public Bounds GetMaxRenderBoxSize()
		{
			Bounds best = GroundFloorElement.GetComponent<Renderer>().bounds;

			for (var i = 0; i < MODULES_COUNT - 1; i++)
			{
				Bounds temp = GroundFloorElement.GetComponent<Renderer>().bounds;

				if (temp.size.x > best.size.x ||
				    temp.size.z > best.size.z ||
				    temp.size.y > best.size.y)
				{
					best = temp;
				}
			}

			return best;
		}
	}
}