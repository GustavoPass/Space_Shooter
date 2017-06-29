using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter {
	public class Planets : MonoBehaviour {

		private PlanetController[] planet;

		private int planetVisible;
		private Transform trans;
		private bool trava;

		private void Awake(){
			planet = GetComponentsInChildren<PlanetController>();
			trans = GetComponent<Transform> ();
			planetVisible = 0;
			trava = false;
		}

		private void Start () {
			setPlanet ();
		}
		

		private void Update () {
			
			if (trava) {
				if (trans.position.y == 16) {
					planetVisible = (planetVisible + 1) % planet.Length;

					setPlanet ();
					trava = false;
				}
			}else if (trans.position.y < 0) {
				trava = true;
			}

		}

		private void setPlanet(){

			for (var i = 0; i < planet.Length; i++) {
				planet [i].setVisible (false);
			}
			planet [planetVisible].setVisible(true);
		}

	}
}
