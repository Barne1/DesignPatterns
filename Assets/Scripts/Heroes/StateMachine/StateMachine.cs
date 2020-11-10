using UnityEngine;

namespace FG {
	public abstract class StateMachine : MonoBehaviour
	{
		public State state { get; protected set; }

		public void SetState(State newState)
		{
			state = newState;
			StartCoroutine(state.StateChanged());
		}
	}
}
