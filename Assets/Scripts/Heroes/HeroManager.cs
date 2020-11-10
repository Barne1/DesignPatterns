using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FG {
	public class HeroManager : MonoBehaviour
	{
		//the 3 modes of the game
		//This is not an implementation of a state machine, see Hero.cs and StateMachine.cs for that
		public enum GameState
		{
			CHOOSE_ATTACKS,
			ATTACKING,
			BEING_ATTACKED,
			COMBAT_OVER
		}
		
		//list of heroes and selected commands for them
		public List<Hero> heroes;
		public Queue<Command> commandQueue;

		//Keeping track of current hero
		public Hero currentHero { get; private set; }
		private int currentHeroIndex = 0;

		//Event and logic for changing the current state
		[System.NonSerialized] 
		public UnityEvent<GameState> GameStateChanged;
		private GameState gameState;

		[SerializeField] private DamageTextFactory damageTextFactory;
		[SerializeField] private HealthBarFactory healthBarFactory;

		public UnityEvent<Hero> OnHeroChanged;

		private Boss enemy;
		
		public GameState E_CurrentGameState //E_ is there to remind us that the assignment invokes an event
		{
			get => gameState;
			private set => GameStateChanged.Invoke(gameState = value);
		}

		private void Awake()
		{
			GameStateChanged = new UnityEvent<GameState>();
			OnHeroChanged = new UnityEvent<Hero>();
			commandQueue = new Queue<Command>();
			currentHero = heroes[0];
		}

		private void Start()
		{
			E_CurrentGameState = GameState.CHOOSE_ATTACKS;
			foreach (Hero hero in heroes)
			{
				//+= since selectionDone is an action
				hero.SelectionDone += SelectionPerformed;
				hero.option1.OnCompletion += ExecuteNextCommand;
				hero.option2.OnCompletion += ExecuteNextCommand;
			}
			//Boss class is hardcoded for demonstration purposes
			Boss.singleton.EnemyTurnDone += CheckDead;
			Boss.singleton.EnemyTurnDone += EnemyTurnOver;
			Boss.singleton.EnemyDead += Victory;
			enemy = Boss.singleton;
		}

		//Invoked by currentHero after enqueueing command
		private void SelectionPerformed()
		{
			if (heroes.Count < 1)
			{
				E_CurrentGameState = GameState.COMBAT_OVER;
				return;
			}
			currentHeroIndex++;
			if (currentHeroIndex < heroes.Count)
			{
				currentHero = heroes[currentHeroIndex];
				OnHeroChanged.Invoke(currentHero);
			}
			else //No more heroes left to select attacks for
			{
				currentHeroIndex = 0;
				currentHero = heroes[currentHeroIndex];
				OnHeroChanged.Invoke(currentHero);
				
				E_CurrentGameState = GameState.ATTACKING;
				//Begin attack phase manually by executing first command
				ExecuteNextCommand();
			}
		}
		
		public void ExecuteNextCommand()
		{
			if (gameState == GameState.COMBAT_OVER)
			{
				return;
			}
			if(commandQueue.Count < 1)
			{
				commandQueue.Clear();
				E_CurrentGameState = GameState.BEING_ATTACKED;
				return;
			}

			Command c = commandQueue.Dequeue();
			StartCoroutine(c.Execute(enemy));
		}

		public void EnQueue(Command c)
		{
			commandQueue.Enqueue(c);
		}

		//loops over heroes and only keeps the live ones
		public void CheckDead()
		{
			List<Hero> aliveHeroes = new List<Hero>();
			foreach (Hero hero in heroes)
			{
				if (hero.hp > 0)
				{
					aliveHeroes.Add(hero);
				}
				else
				{
					Destroy(hero.gameObject);
				}
			}

			if (aliveHeroes.Count < 1)
			{
				E_CurrentGameState = GameState.COMBAT_OVER;
				return;
			}

			heroes = aliveHeroes;
			currentHero = heroes[0];
			OnHeroChanged.Invoke(currentHero);
		}

		public void Victory()
		{
			E_CurrentGameState = GameState.COMBAT_OVER;
		}

		public void EnemyTurnOver()
		{
			E_CurrentGameState = GameState.CHOOSE_ATTACKS;
		}

		//todo refactor into uimanager
		public void SpawnDamageText(int value, Vector3 position)
		{
			DamageText text = damageTextFactory.GetNewOnUI(position);
			text.SetUp(value);
		}

		[SerializeField] private Vector3 healthbarOffset;
		
		public void SpawnHealthBar(Hero hero)
		{
			HealthBar hb = healthBarFactory.GetNewOnUI(hero.transform.position);
			hb.transform.position += healthbarOffset;
			hb.SetUp(hero.OnDamaged);
		}
	}
}
