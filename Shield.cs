using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter {
	public class Shield : MonoBehaviour {

		private Color cor;
		private WaitForSeconds tempo;
		private SpriteRenderer renderColor;

		void Awake(){
			tempo = new WaitForSeconds (0.1f);
			cor = new Color (1, 1, 1, 1);
			renderColor = GetComponent<SpriteRenderer> ();
		}

		public void color(){
			StartCoroutine(shieldColor());
		}

		public void Enable(bool b){
			gameObject.SetActive (b);
			cor.g =	1;
			cor.b = 1;
			renderColor.color = cor;
		}

		public IEnumerator shieldColor(){

			cor.g = 0.4f;
			cor.b = 0.4f;
			renderColor.color = cor;

			yield return tempo;

			cor.g =	1;
			cor.b = 1;
			renderColor.color = cor;
		}
	}
}