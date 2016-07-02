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
        //deplacer joueur
        imageJoueur.rectTransform.position = joueur.position;
        
        if (Input.GetKeyDown(KeyCode.D))
            joueur.toucheEnfoncerD = true;
        if (Input.GetKeyUp(KeyCode.D))
            joueur.toucheEnfoncerD = false;
        if (Input.GetKeyDown(KeyCode.A))
            joueur.toucheEnfoncerA = true;
        if (Input.GetKeyUp(KeyCode.A))
            joueur.toucheEnfoncerA = false;
        if (Input.GetKeyDown(KeyCode.W))
            joueur.toucheEnfoncerD = true;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (PlateformeScript p in liste_plateformes)
                joueur.saut(p.plateform);
        }

        joueur.deplacer();
        foreach(PlateformeScript p in liste_plateformes)
            joueur.collision(p.plateform);
        joueur.validerDeplacement();
        Debug.text = joueur.position.x + ","+ joueur.position.y;
    }
}
