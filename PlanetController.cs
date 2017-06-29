using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter {
	public class PlanetController : MonoBehaviour {

		private Transform trans;
		private Vector2 pos;

		void Awake(){
			trans = GetComponent<Transform> ();
			pos = new Vector2 (trans.position.x, trans.position.y);
		}

		public void setVisible(bool visibility){

			if (visibility) {
				gameObject.SetActive (true);
				/*
				var randomNum = Random.Range (-3f, 3);
				trans.position = new Vector2 (randomNum, trans.position.y);
				*/
				pos.x = Random.Range (-3f, 3);
				trans.position = pos;

			} else {
				gameObject.SetActive (false);
			}
		}


	}
}
