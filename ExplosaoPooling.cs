using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter {
	public class ExplosaoPooling : MonoBehaviour {

		private List<ExplosionController> explosao;
		private int currentIndex;
		public GameObject explosaoPrefab;
		private Transform container;

		void Start(){

			container = GetComponent<Transform> ();
			explosao = new List<ExplosionController> ();
			explosao.Capacity = GameConstants.EXPLOSION_NUMBER;
			addExplosion ();

		}

		void addExplosion(){
			for (byte i = 0; i < GameConstants.EXPLOSION_NUMBER; i++) {

				var go = Instantiate (explosaoPrefab);
				go.SetActive (false);
				go.transform.parent = container;
				explosao.Add (go.GetComponent<ExplosionController> ());

			}
		}

		public ExplosionController getExplosion(){

			ExplosionController ex = explosao [currentIndex];
			if (ex.IsActive ()) {
				print("Number of explosion not enough");
				return null;
			}
			currentIndex = (currentIndex + 1) % explosao.Count;
			return ex;
		}
			
	}
}
