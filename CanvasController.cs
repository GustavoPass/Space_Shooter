using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SpaceShooter {
	public class CanvasController : MonoBehaviour {

		private int pontuacao;
		public Text pontosTX;
		public Text highScore;
		public Slider power;
		private WaitForSeconds sec;
		public GameObject gameOverScreen;
		private bool trava;

		private void Awake () {
			sec = new WaitForSeconds (1);
			StartCoroutine (loadPower ());
		}

		public void score(int ponto){
			pontuacao += ponto;
			pontosTX.text = pontuacao.ToString();
		}

		public void pause(){

			if(!trava)
			switch((int)Time.timeScale){
			case 1:
				Time.timeScale = 0;
				break;

			case 0:
				Time.timeScale = 1;
				break;
			}
		}

		public void menu(){
			SceneManager.LoadScene ("Menu");
			Time.timeScale = 1;
		}

		public void gameOver(){

			if (PlayerPrefs.GetInt ("HighScore") < pontuacao) {
				PlayerPrefs.SetInt ("HighScore", pontuacao);
			}

			highScore.text = "High Score: \n" + PlayerPrefs.GetInt ("HighScore").ToString();

			gameOverScreen.SetActive (true);
			Time.timeScale = 0;
			trava = true;
		}

		public IEnumerator loadPower(){
			yield return sec;
			if (power.value < power.maxValue) {
				power.value += 1;
			}
			StartCoroutine (loadPower ());
		}
	}
}
