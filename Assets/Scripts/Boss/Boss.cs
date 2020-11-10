using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace FG {
	public class Boss : MonoBehaviour, IDamageable {
		//this class is hardcoded due to debug and demonstration purposes
		//it is not intended to be representative of good coding practices or good usage of design patterns
		public static Boss singleton;
		[SerializeField] private HeroManager heroManager;
		[SerializeField] private DamageTextFactory damageTextFactory;
		[SerializeField] private HealthBarFactory healthBarFactory;

		public UnityEvent<IDamageable> OnDamaged;
		
		public int damage = 2;
		public int hp { get; set; }
		public int maxHP { get; set; }

		public UnityAction EnemyDead;
		public UnityAction EnemyTurnDone;

		public BossAttack bossAttack;

		private Hero target;

		private void Awake()
		{
			singleton = this;
			maxHP = 30;
			hp = maxHP;
			OnDamaged = new UnityEvent<IDamageable>();
		}

		private void Start()
		{
			bossAttack = GetComponent<BossAttack>();
			heroManager.GameStateChanged.AddListener(BossAttack);
			HealthBar hb = healthBarFactory.GetNewOnUI(transform.position + Vector3.down);
			hb.SetUp(OnDamaged);
		}

		public void Heal(int healAmount)
		{
			//not needed in this specific instance
		}

		public void TakeDamage(int damageToTake)
		{
			hp -= damageToTake;
			if (hp < 1)
			{
				Debug.Log("enemy dead");
				EnemyDead.Invoke();
			}

			DamageText text = damageTextFactory.GetNewOnUI(transform.position);
			text.SetUp(damageToTake);
			OnDamaged.Invoke(this);
		}

		public void BossAttack(HeroManager.GameState state)
		{
			if (state == HeroManager.GameState.BEING_ATTACKED)
			{
				StartCoroutine(StartAttack());
			}
		}

		public IEnumerator StartAttack()
		{
			foreach (Hero hero in heroManager.heroes)
			{
				StartCoroutine(bossAttack.Begin(hero, damage));
				yield return new WaitUntil(() => bossAttack.done);
				bossAttack.done = false;
			}
			
			EnemyTurnDone.Invoke();
		}
	}
}
