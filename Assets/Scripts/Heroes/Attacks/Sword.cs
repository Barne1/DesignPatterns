using UnityEngine;

namespace FG {
	public class Sword : Attack {
		//single target
		public override int targetAmount
		{
			get => 1;
		}

		private const int swordDamage = 4;
		public Sword()
		{
			damage = swordDamage;
		}
	}
}
