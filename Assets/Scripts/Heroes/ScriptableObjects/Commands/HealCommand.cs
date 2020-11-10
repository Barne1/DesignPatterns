using System.Collections;
using UnityEngine;

namespace FG {
	[CreateAssetMenu(menuName = "NO DOUBLES/HealCommand")]
	public class HealCommand : Command {
		public HealCommand(Hero hero, HeroMovement movement, string commandName) : base(hero, movement, commandName)
		{
		}

		public int healAmount = 5;
		
		public override IEnumerator Execute(IDamageable target)
		{
			this.target = target;
			hero.SetState(new WalkingForward(hero, this));
			Debug.Log($"{hero.heroname} heals for {healAmount}");
			yield return new WaitUntil(() => hero.state.GetType() == typeof(Idle));
			OnCompletion.Invoke();
		}

		public override void Action()
		{
			hero.Heal(healAmount);
		}
	}
}
