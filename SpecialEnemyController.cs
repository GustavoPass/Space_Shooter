using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter {
	public class SpecialEnemyController : MonoBehaviour {
		public PoolingEnemyBullet   pool;
		public ExplosaoPooling explosionPool;
		public WaveController wave;
		public CanvasController canvas;

		private Transform trans;
		private Rigidbody2D rb;
		private SpecialCrate especial;
		public GameObject especialObj;

		private byte life;
		private byte spawnNumber;
		private Vector2 resetPos;

		public GameObject lookToObj;
		public Transform saida1;
		public Transform saida2;

		public Transform target;
		private WaitForSeconds waitTime;
		private WaitForSeconds shootTime;
		private byte shootWave;

		private void Awake(){
			rb = GetComponent<Rigidbody2D> ();
			trans = GetComponent<Transform> ();
			especial = especialObj.GetComponent<SpecialCrate> ();

			waitTime = new WaitForSeconds (3);
			shootTime = new WaitForSeconds (0.2f);

			life = 20;
			resetPos = trans.position;
		}

		private void Update(){
			if (trans.position.x < -4f) {
				destroy (true);
			}
		}

		public void destroy(bool insta){

			life -= 1;

			if (life <= 0 || insta) {
				especial.Activate (true);
				especial.enemyPos (trans.position);
				wave.nextWave ();
				gameObject.SetActive (false);
				spawnNumber += 1;

				switch (spawnNumber) {
				case 0:
					life = 20;
					break;
				case 1:
					life = 35;
					break;
				case 2:
					life = 45;
					break;
				case 3:
					life = 60;
					break;
				case 4:
					life = 75;
					break;
				default:
					life = 90;
					break;
				}
				var posEnemy = explosionPool.getExplosion ();
				posEnemy.setPosition (trans.localPosition);
			}
		}

		public void enable(bool b){
			gameObject.SetActive (b);
			trans.position = resetPos;
			Moviment ();
			StartCoroutine (startShoot ());
		}
			
		private void Moviment(){
			trans.up = lookToObj.transform.position - trans.position;
			rb.AddForce (trans.up * GameConstants.ENEMY1_BULLET_FORCE * 6f * Time.deltaTime);
		}

		private IEnumerator startShoot(){
			shootWave = 0;
			yield return waitTime;
			StartCoroutine (shooter ());
		}

		private IEnumerator shooter(){
			yield return shootTime;
			if(shootWave < 8){
				
				var dir    = target.position - saida1.position;
				var bullet = pool.GetEnemy1Bullet();
				bullet.SetInMotion(saida1.position, dir);

				var dir2    = target.position - saida2.position;
				var bullet2 = pool.GetEnemy1Bullet();
				bullet2.SetInMotion(saida2.position, dir);

				shootWave += 1;
				StartCoroutine (shooter ());
			}
		}

	}
}
