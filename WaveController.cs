using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter {
	public class WaveController : MonoBehaviour {

		[SerializeField]
		private byte num;
		private WaitForSeconds start;
		private int waveNum;
		public EnemySquadController Inimigos1;
		public EnemySquadController Inimigos2;
		public EnemySquadController Inimigos3;
		public SpecialEnemyController EspecialEnemy;
		public MiniBoss miniboss;
		public Boss boss;

		private void Awake(){
			start = new WaitForSeconds (4);
			StartCoroutine (spawnWave ());
			start = new WaitForSeconds (3);
		}

		public void nextWave(){
			waveNum += 1;
			StartCoroutine (spawnWave ());
		}

		private IEnumerator spawnWave(){
			yield return start;

			switch (waveNum) {

			case 0:
				Inimigos1.spawnAll ();
				break;

			case 1:
				Inimigos2.spawnAll ();
				break;

			case 2:
				Inimigos3.spawnAll ();
				break;

			case 3:
				num += 1;
				if (num == 3) {
					miniboss.Enable (true);
				} else if(num == 6){
					boss.Enable (true);
				}else {
					EspecialEnemy.enable (true);
					waveNum = waveNum % 3 - 1;
				}
				break;

			default:
				waveNum = 0;
				StartCoroutine (spawnWave ());
				break;
			}

		}
	}
}
