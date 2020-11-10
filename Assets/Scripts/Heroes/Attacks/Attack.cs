using UnityEngine;

namespace FG {
	public abstract class Attack
	{
		protected int damage;

		//Can be used when selecting attack to choose multiple targets in the future
		public abstract int targetAmount { get; }

		public void TriggerAttack(IDamageable[] targets)
		{
			foreach (IDamageable target in targets)
			{
				AttackEnemy(target);
			}
		}

		protected void AttackEnemy(IDamageable target)
		{
			target.TakeDamage(damage);
		}
	}
}
