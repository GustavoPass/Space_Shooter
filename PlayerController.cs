using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;

namespace SpaceShooter {
    public class PlayerController : MonoBehaviour {
        public PoolingPlayerBullet bulletPool;
		public CanvasController cvct;

        public Transform Saida1;
		public Transform Saida2;
		public Transform Saida3;
		public Transform Saida4;
		public Transform Saida5;

        private WaitForSeconds waveInterval;
        private WaitForSeconds specialCoolDownInterval;

        private TapGesture tapGesture;
		private PressGesture pressGesture;
		private ReleaseGesture releaseGesture;

        private Transform  trans;
        private float      timer;
        private bool       pause;
        private bool       specialAttackOn;
        private int        specialAttackWaveNumber;

		private AudioSource tiroSom;
		private Vector3 posRaio;
		public byte numeroTiros;

		public bool invencibilidade;

        public void Awake() {
            trans                   = GetComponent<Transform>();
            timer                   = 0;
			pause                   = true;
            specialAttackOn         = false;
            waveInterval            = new WaitForSeconds(GameConstants.PLAYER_SPECIAL_INTERVAL);
            specialCoolDownInterval = new WaitForSeconds(GameConstants.PLAYER_SPECIAL_COOL_DOWN_INTERVAL);
            specialAttackWaveNumber = 0;
			tiroSom = GetComponent<AudioSource> ();
			tapGesture = GetComponent<TapGesture>();
			pressGesture = GetComponent<PressGesture> ();
			releaseGesture = GetComponent<ReleaseGesture> ();
			numeroTiros = 1;
        }


        public void OnEnable() {
            tapGesture.Tapped += SpecialAttack;
			pressGesture.Pressed += basicAttackOn;
			releaseGesture.Released += basicAttackOff;
        }


        public void OnDisable() {
            tapGesture.Tapped -= SpecialAttack;
			pressGesture.Pressed -= basicAttackOn;
			releaseGesture.Released -= basicAttackOff;
        }

		public void basicAttackOn(object sender, System.EventArgs e){
			pause = false;
		}

		public void basicAttackOff(object sender, System.EventArgs e){
			pause = true;
		}


        public void Update() {
            if (pause) return;

			timer += Time.deltaTime;
			if (timer >= GameConstants.PLAYER_SHOOT_INTERVAl) {
				timer = 0;
				tiroSom.Play ();

				switch (numeroTiros) {
				case 1:
					saida1shot ();
					break;

				case 2:
					saida2shot ();
					saida3shot ();
					break;

				case 3:
					saida1shot ();
					saida2shot ();
					saida3shot ();
					break;

				case 4:
					saida2shot ();
					saida3shot ();
					saida4shot ();
					saida5shot ();
					break;

				case 5:
					saida1shot ();
					saida2shot ();
					saida3shot ();
					saida4shot ();
					saida5shot ();
					break;

				default:
					saida1shot ();
					break;
				}
			}
        }

		private void saida1shot(){
			var saida1 = bulletPool.GetBullet ();
			saida1.SetInMotion (Saida1.position);
		}

		private void saida2shot(){
			var saida2 = bulletPool.GetBullet ();
			saida2.SetInMotion (Saida2.position,GameConstants.PLAYER_SPECIAL_START_ANGLE + 0.05f * GameConstants.PLAYER_SPECIAL_ANGLE);
		}

		private void saida3shot(){
			var saida3 = bulletPool.GetBullet ();
			saida3.SetInMotion (Saida3.position,GameConstants.PLAYER_SPECIAL_START_ANGLE + 0.05f * -GameConstants.PLAYER_SPECIAL_ANGLE);
		}

		private void saida4shot(){
			var saida4 = bulletPool.GetBullet ();
			saida4.SetInMotion (Saida4.position,GameConstants.PLAYER_SPECIAL_START_ANGLE + 0.1f * GameConstants.PLAYER_SPECIAL_ANGLE);
		}

		private void saida5shot(){
			var saida5 = bulletPool.GetBullet ();
			saida5.SetInMotion (Saida5.position,GameConstants.PLAYER_SPECIAL_START_ANGLE + 0.1f * -GameConstants.PLAYER_SPECIAL_ANGLE);
		}



        public void SpecialAttack(object sender, System.EventArgs e) {
			
			if(cvct.power.value == cvct.power.maxValue)
            if (!specialAttackOn) {
                specialAttackOn = true;
                StartCoroutine(SpecialAttackBattery());
				cvct.power.value = 0;
            }
        }

        public IEnumerator SpecialAttackBattery() {
            while (true) {
                var b = bulletPool.GetSpecial();
				posRaio = new Vector3 (0, trans.position.y, 0);
                b.SetInMotion(posRaio);

                specialAttackWaveNumber++;
                if (specialAttackWaveNumber >= GameConstants.PLAYER_SPECIAL_NUMBER_OF_WAVES) break;
                yield return waveInterval;
            }
            yield return specialCoolDownInterval;
            specialAttackOn         = false;
            specialAttackWaveNumber = 0;
        }

        public void OnTriggerEnter2D(Collider2D hit) {
            if (hit.gameObject.CompareTag(GameConstants.ENEMY_TAG)) {
				hit.gameObject.GetComponent<EnemyController> ().Destroy (true, 1);
				if(!invencibilidade)
				cvct.gameOver ();
                return;
            }

            if (hit.gameObject.CompareTag(GameConstants.ENEMY_BULLET_TAG)) {
                hit.gameObject.GetComponent<EnemyBulletController>().ToggleActive(false);
				if(!invencibilidade)
				cvct.gameOver ();
                return;
            }

			if (hit.gameObject.CompareTag(GameConstants.SPECIAL_COLLECT_TAG)) {
				hit.gameObject.GetComponent<SpecialCrate>().Activate(false);
				if(numeroTiros < 5){
					numeroTiros += 1;
				}
				return;
			}

			if (hit.gameObject.CompareTag(GameConstants.ASTEROID_TAG)) {
				if(!invencibilidade)
				cvct.gameOver ();
				return;
			}
        }

        public void Reset() {
            pause                   = false;
            specialAttackOn         = false;
            specialAttackWaveNumber = 0;
        }
    }
}
