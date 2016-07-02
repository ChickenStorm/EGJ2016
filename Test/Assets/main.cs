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

    public Image listePlateformes2;
    public Image listePlateformes3;
    public Image groundI;

    PlateformeScript[] liste_plateformes;
    PlateformeScript[] liste_plateformes2;
    PlateformeScript[] liste_plateformes3;
    PlateformeScript[] ground;


    public Image bille;
    public List<Biles> billesLi = new List<Biles>();

    public Text Debug;
    public Virus virusOb;
    public Text youWinText;
    public Image BG;
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
    // Use this for initialization$
    Animation AnimVirus = new Animation("AnimVirus", 0.05f, 10);
    Animation joueurAnim = new Animation("joueurAnim", 0.05f, 12);
    Animation billeAnim = new Animation("Bille", 0.05f, 1);
    Animation platAnim = new Animation("Platform", 0.05f, 1);
    Animation platAnim2 = new Animation("Platform2", 0.05f, 1);
    Animation platAnim3 = new Animation("Platform3", 0.05f, 1);
    Animation platAnimGr = new Animation("PlatformGr", 0.05f, 1);


    public Text speed;

    void Start() {
        //mainScene.gameObject.active = true;
        
        mainGameIsRunning = false;
        isInMenu = true;

        
        //w = new World(null, null, null);




        /************************************************/

        liste_plateformes = listePlateformes.transform.GetComponentsInChildren<PlateformeScript>();
        liste_plateformes2 = listePlateformes2.transform.GetComponentsInChildren<PlateformeScript>();
        liste_plateformes3 = listePlateformes3.transform.GetComponentsInChildren<PlateformeScript>();
        ground = groundI.transform.GetComponentsInChildren<PlateformeScript>();


        joueur = new Personnage(new Vector3(0, 300, 0), new Vector3(imageJoueur.rectTransform.rect.width, imageJoueur.rectTransform.rect.height, 0), new Vector3(0, 0, 0), Resources.Load<Sprite>("DSC02576"), imageJoueur, joueurAnim);
        virusOb = new Virus(new Vector3(200, 1000, 0), new Vector3(2, 0, 0), new Vector3(virus.rectTransform.rect.width, virus.rectTransform.rect.height, 0), Resources.Load<Sprite>("DSC02576"), virus,2, bille, AnimVirus,billeAnim);
    
        Image b = Instantiate(bille);
        b.transform.SetParent(billeParent.transform);
        billesLi.Add(new Biles(new Vector3(300, 300, 0), new Vector3(0, 0, 0), new Vector3(10, 10, 0), Resources.Load<Sprite>("DSC02576"), b,1, billeAnim));

        for (int i = 0; i < liste_plateformes.Length; ++i)
        {
            liste_plateformes[i].plateform.anim = platAnim;
            ptemp.Add(liste_plateformes[i].plateform);
        }


        for (int i = 0; i < liste_plateformes2.Length; ++i)
        {
            liste_plateformes[i].plateform.anim = platAnim2;
            ptemp.Add(liste_plateformes2[i].plateform);
        }


        for (int i = 0; i < liste_plateformes3.Length; ++i)
        {
            liste_plateformes[i].plateform.anim = platAnim3;
            ptemp.Add(liste_plateformes3[i].plateform);
        }


        for (int i = 0; i < ground.Length; ++i)
        {
            liste_plateformes[i].plateform.anim = platAnimGr;
            ptemp.Add(ground[i].plateform);
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
        speed.text = "";
        for (int i = 0; i < w.getPlayer().facteurVitesse; ++i)
        {
            speed.text += "|";
        }

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
