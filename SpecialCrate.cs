using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter {
	public class SpecialCrate : MonoBehaviour {

		private Transform trans;
		private Rigidbody2D rb;
		private Vector3 resetPos;
		public static Vector3 spawnPos;

		public void Awake(){
			rb = GetComponent<Rigidbody2D>();
			trans = GetComponent<Transform>();
			resetPos = trans.position;
		}

		public void FixedUpdate() {

			//Movimentação
			rb.MovePosition(trans.position + trans.up * -2f * Time.deltaTime);

			if (rb.position.y < -1f) {
				Activate (false);
			}

		}

		public void Activate(bool act) {
			gameObject.SetActive (act);
		}

		public void enemyPos(Vector3 pos){
			trans.position = pos;
		}
	}
}
