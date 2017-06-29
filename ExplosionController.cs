using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter {
	public class ExplosionController : MonoBehaviour {

		private Transform trans;
		private AudioSource som;

		private void Awake(){
			trans = GetComponent<Transform> ();
			som = GetComponent<AudioSource> ();
		}

		public void toggleActive(bool b){
			gameObject.SetActive (b);
		}

		public void setPosition(Vector2 pos){
			toggleActive (true);
			trans.localPosition = pos;
			if(pos.y > 0)
			som.Play ();
		}

		public void DisableObject() {
			gameObject.SetActive (false);
		}

		public bool IsActive() { return gameObject.activeInHierarchy; }


	}
}
