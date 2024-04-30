/*
using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	private WindZone zona;
	private ParticleSystem particulas;
	private BeansEolica vectorDeVelocidade;
	private float vectorVelocit;

	// Use this for initialization
	void Start () {
		if (GameObject.Find ("Main Camera")){
			vectorDeVelocidade = GameObject.Find ("Main Camera").GetComponent<BeansEolica> ();
		}

		if (GameObject.Find ("WindZone")) {
			zona = GameObject.Find ("WindZone").GetComponent<WindZone> ();
		}

		if (GameObject.Find ("DustStorm")) {
			particulas = GameObject.Find ("DustStorm").GetComponent<ParticleSystem> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("WindZone")) {
			zona.windMain = vectorDeVelocidade.VelocidadeAr / 100f;
		}

		if (GameObject.Find ("DustStorm")) {
			particulas.startSpeed = vectorDeVelocidade.VelocidadeAr / 5f;
		}

		if (GameObject.Find ("Main Camera")) {
			vectorVelocit += vectorDeVelocidade.VelocidadeAr * Time.deltaTime;
		}

		transform.rotation = Quaternion.Euler (0f, 0f, vectorVelocit * 10f);
	}
}
*/
