using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter {
	public class Asteroid : MonoBehaviour {

		private Transform   trans;
		private Rigidbody2D rb;
		private Vector3 rotacao;

		private void Start () {
			rb    = GetComponent<Rigidbody2D>();
			trans = GetComponent<Transform>();
			rotacao = new Vector3 (0, 0, Random.Range(0.1f,1f));
			ToggleActive (false);
		}

		public void SetInMotion(Vector3 pos) {
			ToggleActive(true);
			trans.localPosition = pos;
			rb.AddForce(Vector2.up * -GameConstants.ASTEROID_SPEED * Time.deltaTime);
		}

		public void ToggleActive(bool b) {
			gameObject.SetActive(b);
		}

		private void Update(){
			if (Time.timeScale > 0)
			trans.Rotate (rotacao);
			
			if (trans.position.y < -1) {
				ToggleActive (false);
			}
		}

		public void OnTriggerEnter2D(Collider2D hit) {
			if (hit.gameObject.CompareTag(GameConstants.ENEMY_TAG)) {
				hit.gameObject.GetComponent<EnemyController> ().Destroy (true, 0);
				return;
			}

			if (hit.gameObject.CompareTag(GameConstants.SPECIAL_ENEMY_TAG)) {
				ToggleActive (false);
				return;
			}

		}
	}
}
