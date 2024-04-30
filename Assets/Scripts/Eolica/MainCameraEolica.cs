/*
 * 
using UnityEngine;
using System.Collections;

public class MainCameraEolica : MonoBehaviour {

	private Beans Energiza;
	private float horizontalBar = 1f;

	// Use this for initialization
	void Start () {
		Energiza = GetComponent<Beans> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("Motor")) {
			Quaternion game = Quaternion.LookRotation (GameObject.Find ("Motor").transform.position - transform.position);
			transform.rotation = Quaternion.Slerp (game, transform.rotation, Time.deltaTime * 10f);
		}
	}

	void OnGUI(){
		#region BOTOES RESTAR E SAIR
		if (GUI.Button (new Rect(Screen.width/1.22f,
			Screen.height/1.35f,
			Screen.width/6f,
			Screen.height/20f), "..Restart.."))
		{
			Application.LoadLevel (0);
		}


		if (GUI.Button (new Rect(Screen.width/1.22f,
			Screen.height/1.22f,
			Screen.width/6f,
			Screen.height/20f), "..Sair.."))
		{
			Application.Quit ();
		}
		#endregion

		#region GUI PARA EXIBIR E ALTERAR O POTENCIAL n;
		GUI.Box (new Rect (Screen.width / 100f,
			Screen.height / 30f,
			Screen.width / 6f,
			Screen.height / 20f), "Energia Útil..." + (int)Energiza.EnergiaPotencialUtil() + " w");

		GUI.Box (new Rect (Screen.width / 100f,
			Screen.height / 10f,
			Screen.width / 6f,
			Screen.height / 20f), "Energia Total..." + (int)Energiza.EnergiaTotal() + " w");

		GUI.Box (new Rect (Screen.width / 100f,
			Screen.height / 6f,
			Screen.width / 6f,
			Screen.height / 20f), "Potencial Eólico..." + (int)horizontalBar + "%");
		

		horizontalBar = GUI.HorizontalSlider (new Rect (Screen.width / 100f,
							Screen.height / 4.5f,
							Screen.width / 6f,
							Screen.height / 20f), horizontalBar, 1f, 80f);

		Energiza.Potencial = (horizontalBar/100f);
		#endregion

		#region VELOCIDADE DO AR E RAIO DA PÁ
		GUI.Box (new Rect (Screen.width / 1.22f,
			Screen.height / 30f,
			Screen.width / 6f,
			Screen.height / 20f), "Velocidade Do Ar..." + (int)Energiza.VelocidadeAr + "m/s");

		Energiza.VelocidadeAr = GUI.HorizontalSlider (new Rect (Screen.width / 1.22f,
			Screen.height / 11f,
			Screen.width / 6f,
			Screen.height / 20f), (int)Energiza.VelocidadeAr, 1, 20);

		GUI.Box (new Rect (Screen.width / 1.22f,
			Screen.height / 8f,
			Screen.width / 6f,
			Screen.height / 20f), "Raio da Pá..." + (int)Energiza.Raiopa + "m");

		Energiza.Raiopa = GUI.HorizontalSlider (new Rect (Screen.width / 1.22f,
			Screen.height / 5.5f,
			Screen.width / 6f,
			Screen.height / 20f), (int)Energiza.Raiopa, 2, 100);
		
		#endregion
	}
}
*
*
*/

