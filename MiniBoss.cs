using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter {
	public class MiniBoss : MonoBehaviour {

		public PoolingEnemyBullet   pool;
		public ExplosaoPooling explosionPool;
		public WaveController wave;
		public CanvasController canvas;

		public Transform frenteDireita;
		public Transform frenteEsquerda;
		public Transform trasDireita;
		public Transform trasEsquerda;

		private WaitForSeconds shootTime;
		private WaitForSeconds waveShoot;
		private WaitForSeconds tempoEscudo;
		private WaitForSeconds moveTime;
		private byte waveNumber;

		public Shield escudo;
		public Transform target;
		private Transform trans;
		private Vector2 inicialPos;
		private Vector2 toPos;
		private byte posMove;
		private short life;

		private bool trava;

		private void Awake(){
			trans = GetComponent<Transform> ();

			shootTime = new WaitForSeconds (1.5f);
			waveShoot = new WaitForSeconds (0.2f);
			tempoEscudo = new WaitForSeconds (3f);
			moveTime = new WaitForSeconds (5f);

			inicialPos = new Vector2 (0, 7.9f);
			toPos = new Vector2 (0, 7f);
			life = 100;

			StartCoroutine (shootControl ());
			//StartCoroutine (shieldOn ());
			//StartCoroutine (moviment ());

		}

		private void Update(){

			if (!trava)
			if (trans.position.y > 7.9f) {
				trans.position = Vector2.MoveTowards (trans.position, inicialPos, 0.9f * Time.deltaTime);
				return;
			} else {
				StartCoroutine (shieldOn ());
				StartCoroutine (moviment ());
				trava = true;
			}

			switch (posMove) {

			case 0:
				trans.position = Vector2.MoveTowards (trans.position, inicialPos, 0.5f * Time.deltaTime);
				break;

			case 1:
				toPos.x = 0.75f;
				trans.position = Vector2.MoveTowards (trans.position, toPos, 0.5f * Time.deltaTime);
				break;

			case 2:
				trans.position = Vector2.MoveTowards (trans.position, inicialPos, 0.5f * Time.deltaTime);
				break;

			case 3:
				toPos.x = -0.75f;
				trans.position = Vector2.MoveTowards (trans.position, toPos, 0.5f * Time.deltaTime);
				break;
			}
		}

		public IEnumerator shootControl(){

			yield return waveShoot;

			if (waveNumber > 3) {
				StartCoroutine (sobrecarga ());
			} else {
				
				var dir = target.position - frenteDireita.position;
				var bullet = pool.GetEnemy1Bullet ();
				bullet.SetInMotion (frenteDireita.position, dir);

				dir = target.position - frenteEsquerda.position;
				var bullet2 = pool.GetEnemy1Bullet ();
				bullet2.SetInMotion (frenteEsquerda.position, dir);

				dir = target.position - trasDireita.position;
				var bullet3 = pool.GetEnemy1Bullet ();
				bullet3.SetInMotion (trasDireita.position, dir);

				dir = target.position - trasEsquerda.position;
				var bullet4 = pool.GetEnemy1Bullet ();
				bullet4.SetInMotion (trasEsquerda.position, dir);

				waveNumber += 1;
				StartCoroutine (shootControl ());
			}
		}

		public IEnumerator sobrecarga(){
			yield return shootTime;
			waveNumber = 0;
			StartCoroutine (shootControl ());
		}

		public void Enable(bool b){
			gameObject.SetActive (b);
		}

		public void lifeCount(byte damage){

			life -= damage;

			if (life <= 0) {
				wave.nextWave ();
				canvas.score (50);
				var posEnemy = explosionPool.getExplosion ();
				posEnemy.setPosition (trans.localPosition);
				Enable (false);
			}
		}

		public IEnumerator shieldOn(){
			yield return tempoEscudo;
			escudo.Enable (true);
			StartCoroutine (shieldOff ());
		}

		public IEnumerator shieldOff(){
			yield return tempoEscudo;
			escudo.Enable(false);
			StartCoroutine (shieldOn ());
		}

		public IEnumerator moviment(){
			yield return moveTime;

			if (posMove < 3) {
				posMove +=1;
			} else {
				posMove = 0;
			}

			StartCoroutine (moviment ());
		}

	}
}
