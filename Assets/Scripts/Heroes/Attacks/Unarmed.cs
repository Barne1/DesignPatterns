using UnityEngine;

namespace FG {
	public class Unarmed : Attack {
		//single attack
		public override int targetAmount
		{
			get => 1;
		}

		public Unarmed(int damage)
		{
			this.damage = damage;
		}
	}
}
