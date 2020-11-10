using System;
using UnityEngine;

namespace FG {
	//"where" means that we can only make instances of this if the type inherits from monobehaviour
	public abstract class Factory<T> : MonoBehaviour where T : MonoBehaviour
	{
		[SerializeField] private T prefab;
		[SerializeField] private UIManager ui;

		private void Awake()
		{
		}

		public virtual T GetNew(Vector3 position, Transform parent)
		{
			return Instantiate(prefab, position, Quaternion.identity, parent);
		}
		
		//For use with UIObjects
		public virtual T GetNewOnUI(Vector3 position)
		{
			Vector3 cameraPosition = ui.camera.WorldToScreenPoint(position);
			return GetNew(cameraPosition, ui.transform);
		}
	}
}
