using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter {
    public class EnemySquadController : MonoBehaviour {
        private EnemyController[] planes;
        private int               numberOfActivePlanes;
        private WaitForSeconds spawnInterval;

		public WaveController waveControl;

        public void Awake() {
			planes = GetComponentsInChildren<EnemyController> ();
            numberOfActivePlanes = planes.Length;
            for (int i = 0; i < planes.Length; i++) planes[i].manager = this;
        }


        public void PlaneDestroyed() {
            numberOfActivePlanes--;
            if (numberOfActivePlanes <= 0) {
                numberOfActivePlanes = planes.Length;
				waveControl.nextWave ();
				gameObject.SetActive (false);
            }
        }

		public void spawnAll() {
			gameObject.SetActive (true);
            for (int i = 0; i < planes.Length; i++) planes[i].Activate();
        }

    }  
}
