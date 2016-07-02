using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class main : MonoBehaviour {

    Personnage joueur;
    public Image imageJoueur;
    public Image listePlateformes;
    public Image virus;
    PlateformeScript[] liste_plateformes;
    public Text Debug;
    public Virus virusOb;

    // Use this for initialization
    void Start () {
        joueur = new Personnage(new Vector3(0, 500, 0), new Vector3(100, 100, 0), new Vector3(0, 0, 0), Resources.Load<Sprite>("DSC02576"), imageJoueur);
        virusOb = new Virus(new Vector3(20, 100, 0), new Vector3(100, 100, 0), new Vector3(0, 0, 0), Resources.Load<Sprite>("DSC02576"), virus);


        liste_plateformes = listePlateformes.transform.GetComponentsInChildren<PlateformeScript>();

	}
	
	// Update is called once per frame
	void Update () {
        
    }
}
