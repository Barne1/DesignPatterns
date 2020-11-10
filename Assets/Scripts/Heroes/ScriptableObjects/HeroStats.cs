using UnityEngine;

namespace FG {
	[CreateAssetMenu(fileName = "New Stats", menuName = "ScriptableObjects/HeroStats")]
	public class HeroStats : ScriptableObject
	{
		public string name = "Hero Name";
		public int hp = 10;
		public int damage = 1;
		
		public Command option1;
		public Command option2;
	}
}
