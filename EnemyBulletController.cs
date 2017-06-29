using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter {
    public class EnemyBulletController : MonoBehaviour {
        private static Quaternion zero = Quaternion.identity;

        private Transform   trans;
        private Rigidbody2D rb;

        public void Awake() {
            trans = GetComponent<Transform>();
            rb    = GetComponent<Rigidbody2D>();
        }


        public void SetInMotion(Vector3 pos, Vector3 dir) {
            ToggleActive(true);
            trans.up       = dir;
            trans.position = pos;
            rb.AddForce(trans.up * GameConstants.ENEMY1_BULLET_FORCE);
        }

		public void SetInMotion(Vector3 pos, Vector3 dir, int force) {
			ToggleActive(true);
			trans.up       = dir;
			trans.position = pos;
			rb.AddForce(trans.up * force);
		}

		public void deathShot(Vector3 pos, float angle) {
			ToggleActive(true);
			trans.rotation = zero;
			trans.Rotate(0, 0, angle);
			Vector3 p = new Vector3(pos.x + Mathf.Cos(Mathf.Deg2Rad * angle) * GameConstants.PLAYER_SPECIAL_RADIUS,
				pos.y + Mathf.Sin(Mathf.Deg2Rad * angle) * GameConstants.PLAYER_SPECIAL_RADIUS);
			trans.position = p;
			rb.AddForce(trans.up * GameConstants.ENEMY1_BULLET_FORCE);
		}

        public bool IsActive() { return gameObject.activeInHierarchy; }


        public void ToggleActive(bool b) {
            gameObject.SetActive(b);
        }


        public void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.CompareTag(GameConstants.BORDER_TAG)) {
                ToggleActive(false);
                return;
            }

			if (collision.gameObject.CompareTag(GameConstants.ASTEROID_TAG)) {
				ToggleActive(false);
				return;
			}

        }
    }
}