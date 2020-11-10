using UnityEngine;
using System;

namespace FG {
	[CreateAssetMenu(fileName = "New HeroMovement", menuName = "ScriptableObjects/HeroMovements")]
	public class HeroMovement : ScriptableObject
	{
		public Vector3 finalPosition = Vector3.right;
		public float secondsToWalkForward = 1f;
		public float secondsToWait = 1f;
		public float secondsToReturn = 1f;
		public MovementType movementType = MovementType.LERP;

		/// <summary>
		/// Start, end, t, return
		/// </summary>
		public Func<Vector3, Vector3, float, Vector3> MovementMethod
		{
			get
			{
				switch (movementType)
				{
					default: return Vector3.Lerp;
					case MovementType.LERP: return Vector3.Lerp;
					//case MovementType.SLERP: return Vector3.Slerp;
				}
			}
		}

		//Other movement types not working as intended yet
		//todo review options
		public enum MovementType
		{
			LERP,
			//SLERP,
			//MOVETOWARD
		}
	}
}
