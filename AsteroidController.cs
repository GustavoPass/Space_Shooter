using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter {
	public class AsteroidController : MonoBehaviour {

		private Asteroid[] asts;
		private WaitForSeconds tempo;
		private Vector2 pos;
		private int indexAst;

		private void Awake () {
			asts = GetComponentsInChildren<Asteroid> ();
			tempo = new WaitForSeconds (4);
			StartCoroutine (asteroidSet ());
		}

		private IEnumerator asteroidSet(){
			yield return tempo;

			pos.x = Random.Range (-2.3f,2.3f);
			asts [indexAst].SetInMotion (pos);

			indexAst = (indexAst + 1) % asts.Length;

			tempo = new WaitForSeconds (Random.Range (2.5f, 6f));
			StartCoroutine (asteroidSet ());
		}
	}
}