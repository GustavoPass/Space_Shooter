using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter {
	public class Raio : MonoBehaviour {

		private Transform   trans;
		private Rigidbody2D rb;
		private AudioSource som;

		void Awake(){
			trans = GetComponent<Transform>();
			rb    = GetComponent<Rigidbody2D>();
			som = GetComponent<AudioSource> ();
		}

		public void SetInMotion(Vector3 pos) {
			ToggleActive(true);
			som.Play ();
			trans.position = pos;
			rb.AddForce(Vector2.up * GameConstants.PLAYER_BULLET_FORCE);
		}

		public void ToggleActive(bool b) {
			gameObject.SetActive(b);
		}
		

		void Update () {
			if (trans.position.y > 10) {
				ToggleActive (false);
			}
		}

		public bool IsActive() { return gameObject.activeInHierarchy; }

		public void OnTriggerEnter2D(Collider2D hit) {
			if (hit.gameObject.CompareTag(GameConstants.BORDER_TAG)) {
				return;
			} 

			if (hit.gameObject.CompareTag(GameConstants.ENEMY_BULLET_TAG)) {
				hit.gameObject.GetComponent<EnemyBulletController>().ToggleActive(false);
				return;
			}

			if (hit.gameObject.CompareTag(GameConstants.ENEMY_TAG)) {
				hit.gameObject.GetComponent<EnemyController> ().Destroy (true, 2);
				//GameController.pontuacao += 10;
				return;
			}

			if (hit.gameObject.CompareTag(GameConstants.SPECIAL_ENEMY_TAG)) {
				hit.gameObject.GetComponent<SpecialEnemyController> ().destroy (true);
				//GameController.pontuacao += 15;
				//GameController.quantDead += 1;
				return;
			}
		}
	}
}
