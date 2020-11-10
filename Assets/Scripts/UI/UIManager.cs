using System;
using UnityEngine;
using UnityEngine.UI;

namespace FG {
	public class UIManager : MonoBehaviour
	{
		[SerializeField] private HeroManager heroManager;
		[SerializeField] private Text heroNameText;
		[SerializeField] Button[] buttons;
		private Text[] buttonText;
		public Camera camera { get; protected set; }

		private void Awake()
		{
			buttonText = new Text[buttons.Length];
			for (int i = 0; i < buttons.Length; i++)
			{
				buttonText[i] = buttons[i].GetComponentInChildren<Text>();
			}
			camera = Camera.main;
		}

		private void Start()
		{
			heroManager.GameStateChanged.AddListener(GameStateChanged);
			heroManager.OnHeroChanged.AddListener(ChangeText);
			//First time setup
			ChangeText(heroManager.currentHero);
		}

		public void OnClickOption(int option)
		{
			heroManager.currentHero.QueueOption(option);
		}

		//disables and enables buttons based on GameState in HeroManager
		public void GameStateChanged(HeroManager.GameState state)
		{
			ChangeButtonInteractable(state == HeroManager.GameState.CHOOSE_ATTACKS);
		}

		void ChangeButtonInteractable(bool interactable)
		{
			foreach (Button button in buttons)
			{
				button.interactable = interactable;
			}
		}
		
		//todo hook up to hero change event
		void ChangeText(Hero hero)
		{
			heroNameText.text = hero.heroname;
			
			//todo fix when updating command amount to be variable
			buttonText[0].text = hero.option1.commandName;
			buttonText[1].text = hero.option2.commandName;
		}
	}
}
