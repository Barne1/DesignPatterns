using UnityEngine;

namespace FG {
	public interface IDamageable
	{
		int hp { get; set; }
		int maxHP { get; set; }
		void Heal(int healAmount);
		void TakeDamage(int damageAmount);
	}
}
