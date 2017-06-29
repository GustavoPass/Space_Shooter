using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter {
    public class PoolingPlayerBullet : MonoBehaviour {
        public  GameObject                   bulletPrefab;
        public  Transform                    bulletsContainer;
        private int                          currentBulletIndex;
        private List<PlayerBulletController> bullets;

        public  GameObject                   specialPrefab;
        public  Transform                    specialContainer;
        private int                          currentSpecialIndex;
		private List<Raio> specials;


        public void Start() {
            bullets          = new List<PlayerBulletController>();
            bullets.Capacity = GameConstants.PLAYER_BULLET_NUMBER;
            AddBulletsToThePool();

			specials          = new List<Raio>();
            specials.Capacity = GameConstants.PLAYER_SPECIAL_NUMBER_OF_WAVES *
                                GameConstants.PLAYER_SPECIAL_NUMBER_OF_SINGLE_SHOTS;
            AddSpecialsToThePool();
        }

        #region Regular Bullets
        private void AddBulletsToThePool() {
            for (int i = 0; i < GameConstants.PLAYER_BULLET_NUMBER; i++) {
                var go = Instantiate(bulletPrefab);
                go.SetActive(false);
                go.transform.parent = bulletsContainer;
                bullets.Add(go.GetComponent<PlayerBulletController>());
            }
        }

        public PlayerBulletController GetBullet() {
            PlayerBulletController b = bullets[currentBulletIndex];
            if (b.IsActive()) {
                print("Number of PlayerBullets not enough");
                return null;
            }

            currentBulletIndex = (currentBulletIndex + 1) % bullets.Count;
            return b;
        }
        #endregion

        #region Special Attack
        private void AddSpecialsToThePool() {
            var count = GameConstants.PLAYER_SPECIAL_NUMBER_OF_WAVES *
                        GameConstants.PLAYER_SPECIAL_NUMBER_OF_SINGLE_SHOTS;
            for (int i = 0; i < count; i++) {
                var go = Instantiate(specialPrefab);
                go.SetActive(false);
                go.transform.parent = specialContainer;
				specials.Add(go.GetComponent<Raio>());
            }
        }

		public Raio GetSpecial() {
			Raio b = specials[currentSpecialIndex];
            if (b.IsActive()) {
                print("Number of PlayerBullets not enough");
                return null;
            }

            currentSpecialIndex = (currentSpecialIndex + 1) % specials.Count;
            return b;
        }
        #endregion

    }
}
