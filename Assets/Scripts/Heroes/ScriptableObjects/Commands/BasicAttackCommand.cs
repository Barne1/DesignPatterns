using System.Collections;
using UnityEngine;

namespace FG {
	[CreateAssetMenu(menuName = "NO DOUBLES/BasicAttack")]
	public class BasicAttackCommand : Command {
		public BasicAttackCommand(Hero hero, HeroMovement movement, string commandName) : base(hero, movement, commandName)
		{
		}
		public override IEnumerator Execute(IDamageable target)
		{
			this.target = target;
			hero.SetState(new WalkingForward(hero, this));
			yield return new WaitUntil(() => hero.state.GetType() == typeof(Idle));
			OnCompletion.Invoke();
		}

		public override void Action()
		{
			//single attack
			hero.weaponEquipped.TriggerAttack(new []{target}); 
		}
	}
}
