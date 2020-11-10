using System;
using System.Collections;
using UnityEngine;

namespace FG {
	[CreateAssetMenu(menuName = "NO DOUBLES/BasicAttack")]
	public class BossAttack : MonoBehaviour {
		//this class is hardcoded due to debug and demonstration purposes
		//it is not intended to be representative of good coding practices or good usage of design patterns

		private Boss boss;
		private HeroMovement movement;
		private Vector3 o_position;
		public float timePerStep = 0.3f;
		public bool done = false;
		
		public void Awake()
		{
			o_position = transform.position;
			boss = this.GetComponent<Boss>();
		}

		//once again placeholder code for debug and demonstration purposes
		public IEnumerator Begin(Hero target, int damage)
		{
			Vector3 vectorToBetweenTarget = (target.transform.position - o_position);
			Vector3 desiredPosition = (transform.position + vectorToBetweenTarget) * 0.3f;
			Vector3 finalPosition = new Vector3(desiredPosition.x, o_position.y, desiredPosition.z);

			float currentTime = 0f;
			while (currentTime < timePerStep)
			{
				currentTime += Time.deltaTime;
				float t = Mathf.InverseLerp(0, timePerStep, currentTime);
				transform.position = Vector3.Lerp(o_position, finalPosition, t);
				yield return null;
			}

			StartCoroutine(DoAttack(target, damage));
		}

		IEnumerator DoAttack(Hero target, int damage)
		{
			target.TakeDamage(damage);
			yield return new WaitForSeconds(timePerStep);
			StartCoroutine(WalkBack());
		}
		
		public IEnumerator WalkBack()
		{
			Vector3 currentPos = transform.position;
			float currentTime = 0f;
			while (currentTime < timePerStep)
			{
				currentTime += Time.deltaTime;
				float t = Mathf.InverseLerp(0, timePerStep, currentTime);
				transform.position = Vector3.Lerp(currentPos, o_position, t);
				yield return null;
			}

			done = true;
		}
	}
}
