using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter {
	public class Boss : MonoBehaviour {
		
		public ExplosaoPooling explosionPool;
		public WaveController wave;
		public CanvasController canvas;

		private WaitForSeconds explosionTime;
		private WaitForSeconds endTime;
		private byte explosionLocation;
		public Transform[] explosions;

		private Transform trans;
		private Vector2 inicialPos;
		private byte posMove;
		private short life;

		private bool trava;

		private void Awake(){
			trans = GetComponent<Transform> ();
			endTime = new WaitForSeconds (2f);
			explosionTime = new WaitForSeconds (0.2f);

			inicialPos = new Vector2 (0, 7f);
			life = 10;
		}

		private void Update(){

			if (!trava)
			if (trans.position.y > 7f) {
				trans.position = Vector2.MoveTowards (trans.position, inicialPos, 0.9f * Time.deltaTime);
				return;
			} else {
				trava = true;
			}
		}

		public void vida(){
			life -= 1;

			if (life <= 0) {
				StartCoroutine (end ());
			}
		}

		public void Enable(bool b){
			gameObject.SetActive (b);
		}

		private IEnumerator explosionShow(){
			yield return explosionTime;
			if (explosionLocation < explosions.Length) {
				var posEnemy = explosionPool.getExplosion ();
				posEnemy.setPosition (explosions [explosionLocation].position);
				StartCoroutine (explosionShow ());
			}
			explosionLocation += 1;
		}

		private IEnumerator end(){
			StartCoroutine (explosionShow ());
			yield return endTime;
			wave.nextWave ();
			canvas.score (200);
			Enable (false);
		}

	}
}
