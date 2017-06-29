using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter {
	public class Bolinhas : MonoBehaviour {
		public PoolingEnemyBullet pool;
		private Boss boss;
		public Transform target;

		private int life;
		private Transform trans;
		public ExplosaoPooling explosionPool;

		private void Start () {
			life = Random.Range (25, 40);
			boss = GetComponentInParent<Boss> ();
			trans = GetComponent<Transform> ();
		}
		
		public void damaged(){
			life -= 1;

			var dir = target.position - trans.position;
			var bullet = pool.GetEnemy1Bullet ();
			bullet.SetInMotion (trans.position, dir, 250);

			if (life <= 0) {
				var posEnemy = explosionPool.getExplosion ();
				posEnemy.setPosition (trans.position);
				boss.vida ();
				gameObject.SetActive (false);
			}
		}

	}
}
