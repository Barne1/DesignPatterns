using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace FG {
	public abstract class Command : ScriptableObject
	{
		public string commandName;
		protected Hero hero;
		public HeroMovement movement;

		public UnityAction OnCompletion;
		
		protected IDamageable target;

		public Command(Hero hero, HeroMovement movement, string commandName)
		{
			this.hero = hero;
			this.movement = movement;
			this.commandName = commandName;
		}
		public abstract IEnumerator Execute(IDamageable target);

		public Command GetNew(Hero hero)
		{
			Type type = this.GetType();
			object[] constructorArgs = {hero, movement, commandName};
			return Activator.CreateInstance(type, constructorArgs) as Command;
		}

		public abstract void Action();
	}
}
