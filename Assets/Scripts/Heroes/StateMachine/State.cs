using System.Collections;
using UnityEngine;

namespace FG {
	public abstract class State
	{
		protected Hero hero;
		protected HeroMovement movement;
		protected Command command;
		public State(Hero hero, Command command)
		{
			this.hero = hero;
			this.command = command;
			this.movement = command.movement;
		}
		public virtual IEnumerator StateChanged()
		{
			yield break;
		}
	}
}
