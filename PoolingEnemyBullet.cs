using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter {
    public class PoolingEnemyBullet : MonoBehaviour {
        public  GameObject               enemy1BulletPrefab;
        public  Transform                enemy1BulletContainer;
        private int                      currentEnemy1BulletIndex;

		//List = Array que você pode alterar o tamanho em tempo real
		//Em List - Count equivale a Length em Array
        private List<EnemyBulletController> enemy1Bullets;

        public void Awake() {
            enemy1Bullets          = new List<EnemyBulletController>();
            enemy1Bullets.Capacity = GameConstants.ENEMY1_BULLET_NUMBER;
            AddEnemy1BulletsToThePool();
        }

        private void AddEnemy1BulletsToThePool() {
            for (int i = 0; i < GameConstants.ENEMY1_BULLET_NUMBER; i++) {
                var go = Instantiate(enemy1BulletPrefab);
                go.SetActive(false);
                go.transform.parent = enemy1BulletContainer;
                enemy1Bullets.Add(go.GetComponent<EnemyBulletController>());
            }
        }


        public EnemyBulletController GetEnemy1Bullet() {
            EnemyBulletController b = enemy1Bullets[currentEnemy1BulletIndex];
            if (b.IsActive()) {
                print("Number of Enemy1 Bullets not enough");
                return null;
            }

            currentEnemy1BulletIndex = (currentEnemy1BulletIndex + 1) % enemy1Bullets.Count;
            return b;
        }
    }
}