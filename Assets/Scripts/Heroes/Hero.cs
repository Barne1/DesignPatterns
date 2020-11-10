using System;
using UnityEngine;
using UnityEngine.Events;

namespace FG {
	public class Hero : StateMachine, IDamageable
	{
		public enum Weapon
		{
			UNARMED,
			SWORD
		}

		[SerializeField] private Weapon weaponOnStart = Weapon.UNARMED;
		public Attack weaponEquipped { get; protected set; }
		public string heroname { get; protected set; } = "";
		public int hp { get; set; }
		public int maxHP { get; set; }
		public int damage { get; protected set; } = 0;

		[SerializeField] public HeroManager heroManager;

		[SerializeField]
		private HeroStats statsInit;

		public UnityAction SelectionDone;
		[System.NonSerialized]
		public UnityEvent<IDamageable> OnDamaged;
		
		public Command option1 { get; private set; }
		public Command option2 { get; private set; }


		protected virtual void Awake()
		{
			if(heroManager == null)
				heroManager = GetComponentInParent<HeroManager>();

			heroname = statsInit.name;
			maxHP = statsInit.hp;
			hp = maxHP;
			damage = statsInit.damage;
			
			EquipWeapon(weaponOnStart);
			
			//todo move movement to serializedObject
			option1 = statsInit.option1.GetNew(this);
			option2 = statsInit.option2.GetNew(this);
			
			OnDamaged = new UnityEvent<IDamageable>();
		}

		private void Start()
		{
			heroManager.SpawnHealthBar(this);
		}

		//todo merge options into array with adjustable amount of attacks and updating UI
		public void QueueOption(int option)
		{
			switch (option)
			{
				default: Debug.LogError("No option available for option " + option);
					break;
				case 1: 
					heroManager.EnQueue(option1);
					break;
				case 2: 
					heroManager.EnQueue(option2);
					break;
			}
			SelectionDone.Invoke();
		}

		public void TakeDamage(int damageTaken)
		{
			hp -= damageTaken;
			heroManager.SpawnDamageText(damageTaken, transform.position);
			OnDamaged.Invoke(this);
		}

		public void Heal(int healing)
		{
			int hpToHeal = hp + healing > statsInit.hp ? statsInit.hp - hp : healing;
			hp += hpToHeal;
			heroManager.SpawnDamageText(hpToHeal, transform.position);
			OnDamaged.Invoke(this);
		}

		public void EquipWeapon(Weapon toEquip)
		{
			switch (toEquip)
			{
				default: weaponEquipped = new Unarmed(damage);
					return;
				case Weapon.SWORD: weaponEquipped = new Sword();
					return;
			}
		}
	}
}
