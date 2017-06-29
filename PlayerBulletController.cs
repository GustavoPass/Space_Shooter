using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter {
    public class PlayerBulletController : MonoBehaviour {
        private static Quaternion zero = Quaternion.identity;
		private SpecialCrate especialCrate;
		private int quantDead;

        private Transform   trans;
        private Rigidbody2D rb;


        public void Awake() {
            trans = GetComponent<Transform>();
            rb    = GetComponent<Rigidbody2D>();
        }


        public void SetInMotion(Vector3 pos) {
            ToggleActive(true);
            trans.position = pos;
            rb.AddForce(Vector2.up * GameConstants.PLAYER_BULLET_FORCE);
        }


		// This function is only for the Special Attack
		public void SetInMotion(Vector3 pos, float angle) {
			ToggleActive(true);
			trans.rotation = zero;
			trans.Rotate(0, 0, angle - GameConstants.PLAYER_SPECIAL_START_ANGLE);
			Vector3 p = new Vector3(pos.x + Mathf.Cos(Mathf.Deg2Rad * angle) * GameConstants.PLAYER_SPECIAL_RADIUS,
				pos.y + Mathf.Sin(Mathf.Deg2Rad * angle) * GameConstants.PLAYER_SPECIAL_RADIUS);
			trans.position = p;
			rb.AddForce(trans.up * GameConstants.PLAYER_SPECIAL_FORCE);
		}
			
        public bool IsActive() { return gameObject.activeInHierarchy; }


        public void ToggleActive(bool b) {
            gameObject.SetActive(b);
        }

        public void OnTriggerEnter2D(Collider2D hit) {
            if (hit.gameObject.CompareTag(GameConstants.BORDER_TAG)) {
                ToggleActive(false);
                return;
            } 
            
			/*
            if (hit.gameObject.CompareTag(GameConstants.ENEMY_BULLET_TAG)) {
                hit.gameObject.GetComponent<EnemyBulletController>().ToggleActive(false);
                ToggleActive(false);
                return;
            }
            */

            if (hit.gameObject.CompareTag(GameConstants.ENEMY_TAG)) {
				hit.gameObject.GetComponent<EnemyController> ().Destroy (false, 1);
                ToggleActive(false);
                return;
            }

			if (hit.gameObject.CompareTag(GameConstants.SPECIAL_ENEMY_TAG)) {
				hit.gameObject.GetComponent<SpecialEnemyController> ().destroy (false);
				ToggleActive(false);
				return;
			}

			if (hit.gameObject.CompareTag(GameConstants.ENEMY_SHIELD_TAG)) {
				hit.gameObject.GetComponent<Shield> ().color ();
				ToggleActive(false);
				return;
			}

			if (hit.gameObject.CompareTag(GameConstants.MINI_BOSS_TAG)) {
				hit.gameObject.GetComponent<MiniBoss> ().lifeCount (1);
				ToggleActive(false);
				return;
			}

			if (hit.gameObject.CompareTag(GameConstants.BOLINHA_TAG)) {
				hit.gameObject.GetComponent<Bolinhas> ().damaged ();
				ToggleActive(false);
				return;
			}

			if (hit.gameObject.CompareTag(GameConstants.ASTEROID_TAG)) {
				ToggleActive(false);
				return;
			}

        }
    }
}
