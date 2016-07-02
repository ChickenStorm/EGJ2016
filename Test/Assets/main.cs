using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class main : MonoBehaviour {

    Personnage joueur;
    public Image imageJoueur;
    public Image listePlateformes;
    public Image virus;
    PlateformeScript[] liste_plateformes;

    public Image bille;
    public List<Biles> billesLi = new List<Biles>();

    public Text Debug;
    public Virus virusOb;
    public Text youWinText;
    public World w;
    private List<Plateform> ptemp = new List<Plateform>();
    private bool mainGameIsRunning = false;
    public Image mainScene;
    public Image mainMenu;
    private bool isInMenu = true;
    Vector3 scenePosBegin;
    public Camera cam;
    public Canvas c1;
    public Canvas c2;
    public GameObject billeParent;
    // Use this for initialization

    void Start() {
        //mainScene.gameObject.active = true;
        
        mainGameIsRunning = false;
        isInMenu = true;

        
        //w = new World(null, null, null);




        /************************************************/

        liste_plateformes = listePlateformes.transform.GetComponentsInChildren<PlateformeScript>();

        joueur = new Personnage(new Vector3(0, 300, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0), Resources.Load<Sprite>("DSC02576"), imageJoueur);
        virusOb = new Virus(new Vector3(200, 500, 0), new Vector3(2, 0, 0), new Vector3(0, 0, 0), Resources.Load<Sprite>("DSC02576"), virus,2, bille);
    
        Image b = Instantiate(bille);
        b.transform.SetParent(billeParent.transform);
        billesLi.Add(new Biles(new Vector3(300, 300, 0), new Vector3(0, 0, 0), new Vector3(10, 10, 0), Resources.Load<Sprite>("DSC02576"), b,1));

        for (int i = 0; i < liste_plateformes.Length; ++i)
        {
            ptemp.Add(liste_plateformes[i].plateform);
        }

        w = new World(joueur, ptemp, virusOb, billesLi, bille, billeParent);
        /************************************************/

        //mainScene.gameObject.SetActive(false);
        //mainMenu.gameObject.SetActive(true);
        scenePosBegin = mainScene.rectTransform.position;
        CreatGameSceneDefault();

    }

    void CreatGameSceneDefault () {
        
        isInMenu = false;
        mainScene.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);
        /*joueur = new Personnage(new Vector3(0, 500, 0), new Vector3(100, 100, 0), new Vector3(0, 0, 0), Resources.Load<Sprite>("DSC02576"), imageJoueur);
        virusOb = new Virus(new Vector3(200, 200, 0), new Vector3(100, 100, 0), new Vector3(0, 0, 0), Resources.Load<Sprite>("DSC02576"), virus);



        


        for (int i =0; i< liste_plateformes.Length;++i) {
            ptemp.Add(liste_plateformes[i].plateform);
        }



        w = new World(joueur, ptemp, virusOb);*/
        mainGameIsRunning = true;
    }

    // Update is called once per frame
    void Update() {
        //mainScene.rectTransform.position = w.getPlayer().position;
        c1. transform.position = w.getPlayer().position ;
        //c2.transform.position = new Vector3(0, -100, 0);
        if (mainGameIsRunning)
        {
            w.update(Time.deltaTime);

            if (w.hasWin)
            {
                SceneManager.LoadScene("win");
                //youWinText.rectTransform.position = new Vector3(200, 200, 0);
            }
             

        }
        

        /*

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

    */
    }
}
