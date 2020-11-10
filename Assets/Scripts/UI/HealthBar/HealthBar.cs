using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace FG {
	public class HealthBar : MonoBehaviour
	{
		[SerializeField]private Image bar;

		public void SetUp(UnityEvent<IDamageable> enemyTakeDamage)
		{
			enemyTakeDamage.AddListener(UpdateBar);
		}

		public void UpdateBar(IDamageable target)
		{
			float t = (float)target.hp / target.maxHP;
			bar.fillAmount = t;
		}
	}
}
