using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public GameObject menu;
	public GameObject creditos;
	public GameObject load;
	public GameObject seila;

	public void iniciar(){
		menu.SetActive (false);
		load.SetActive (true);
		SceneManager.LoadScene ("Jogo");
	}

	public void menuShow(){
		menu.SetActive (true);
		seila.SetActive (true);
		creditos.SetActive (false);
	}

	public void creditosShow(){
		menu.SetActive (false);
		seila.SetActive (false);
		creditos.SetActive (true);
	}
}
