using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter {
    public class EnemyController : MonoBehaviour {
        public PoolingEnemyBullet   pool;
        public EnemySquadController manager;
		public CanvasController canvas;
		public PlayerController player;

        private Transform   trans;
        private Rigidbody2D rb;
        public Transform    target;
        private float       fireFrequency;
        private float       timer;
        private bool        pause;
        private Vector3     resetPos;

		private int life;
		public byte enemyType;

		//Pegar explosao
		public ExplosaoPooling explosionPool;


        public void Awake() {
            trans         = GetComponent<Transform>();
            resetPos      = trans.position;
            rb            = GetComponent<Rigidbody2D>();
            target        = GameObject.FindWithTag(GameConstants.PLAYER_TAG).transform;
            fireFrequency = UnityEngine.Random.Range(GameConstants.ENEMEY1_MIN_FIRE_INTERVAL,
                                                     GameConstants.ENEMEY1_MAX_FIRE_INTERVAL);

			switch (enemyType) {
			case 1:
				life = 5;
				resetPos.x = Random.Range (-2f, 2f);
				trans.localPosition = resetPos;
				break;
			case 2:
				life = 2;
				break;
			case 3:
				life = 3;
				break;
			}
        }


        public void Update() {
            if (pause) return;

            timer += Time.deltaTime;
			if (timer >= fireFrequency) {
				timer = 0;

				//Variavel que faz o inimigo atirar na posição do jogador
				var dir = target.position - trans.position;
				var bullet = pool.GetEnemy1Bullet ();
				bullet.SetInMotion (trans.position, dir);
			}
        }

        public void FixedUpdate() {
			//Movimentação do inimigo
			if (rb.position.y <= -1F)
				Destroy (true, 0);

			if (enemyType == 2) {
				rb.MovePosition(trans.position + trans.up * GameConstants.ENEMY2_SPEED * Time.deltaTime);
				return;
			}

            rb.MovePosition(trans.position + trans.up * GameConstants.ENEMY1_SPEED * Time.deltaTime);
        }


        public bool IsActive() { return gameObject.activeInHierarchy; }


        public void Activate() {
			//Resetar posição dos
            trans.position = resetPos;
            timer          = 0;
            pause          = false;
            gameObject.SetActive(true);
        }


		public void Destroy(bool insta, int multiplicadorPontos) {

			life -= 1;

			if (life <= 0 || insta) {
				pause = true;

				//Explosao
				var posEnemy = explosionPool.getExplosion ();
				posEnemy.setPosition (trans.localPosition);

				switch (enemyType) {
				case 1:
					resetPos.x = Random.Range (-2f, 2f);
					trans.position = resetPos;
					life = 5 + player.numeroTiros;
					break;
				case 2:
					resetPos.x = Random.Range (-2f,2f);
					life = 2 + player.numeroTiros;
					break;
				case 3:
					for (var i = 0; i < 4; i++) {
						var bullet = pool.GetEnemy1Bullet ();
						bullet.deathShot (trans.position, 90 * i);
					}
					for (var i = 0; i < 4; i++) {
						var bullet = pool.GetEnemy1Bullet ();
						bullet.deathShot (trans.position, (90 * i) - 45);
					}
					life = 4 + player.numeroTiros;
					break;
				}

				canvas.score (life * multiplicadorPontos);
				
				manager.PlaneDestroyed ();
				gameObject.SetActive (false);
			}
        }
    }
}

