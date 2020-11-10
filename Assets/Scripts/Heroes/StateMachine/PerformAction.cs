using System.Collections;
using UnityEngine;

namespace FG
{
	public class PerformAction : State
	{
		public PerformAction(Hero hero, Command command) : base(hero, command)
		{
		}

		public override IEnumerator StateChanged()
		{
			yield return new WaitForSeconds(movement.secondsToWait * 0.5f);
			//executes the action in the middle of the wait time
			command.Action();
			yield return new WaitForSeconds(movement.secondsToWait * 0.5f);
			hero.SetState(new WalkingBack(hero, command));
		}
	}
}
