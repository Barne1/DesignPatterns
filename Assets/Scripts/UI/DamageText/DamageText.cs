using System;
using TMPro;
using UnityEngine;

namespace FG {
	public class DamageText : MonoBehaviour
	{
		private TextMeshProUGUI text;

		private void Awake()
		{
			text = GetComponent<TextMeshProUGUI>();
		}

		public void SetUp(int damage, float timeOnScreen = 1f)
		{
			text.text = damage.ToString();
			Destroy(this.gameObject, timeOnScreen);
		}
	}
}
