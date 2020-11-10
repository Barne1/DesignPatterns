using System.Collections;
using UnityEngine;

namespace FG {
	public class WalkingBack : State {
		public WalkingBack(Hero hero, Command command) : base(hero, command)
		{
		}

		public override IEnumerator StateChanged()
		{
			Vector3 startPos = hero.transform.position;
			
			float timePassed = 0f;
			float finalTime = movement.secondsToReturn;
			
			while (timePassed < finalTime)
			{
				timePassed += Time.deltaTime;
				float t = Mathf.InverseLerp(0, finalTime, timePassed);
				hero.transform.position = movement.MovementMethod(startPos, startPos - movement.finalPosition, t);
				yield return null;
			}
			//Movement done
			hero.SetState(new Idle(hero, command));
		}
	}
}
