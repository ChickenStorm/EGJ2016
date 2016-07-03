using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class main : MonoBehaviour {

    Personnage joueur;
    public Image imageJoueur;
    public Image imageJoueurF;
    public Image imageJoueurTr;


    public Image listePlateformes;
    public Image virus;

    public Image listePlateformes2;
    public Image listePlateformes3;
    public Image listePlateformesSchack;
    public Image listePlateformesMvt;
    public Image groundI;

    
    public Image iconeVirus;
    public Image iconeVirusAn;
    PlateformeScript[] liste_plateformes;
    PlateformeScript[] liste_plateformes2;
    PlateformeScript[] liste_plateformes3;
    PlateformeScript[] liste_plateformesMvt;
    PlateformeScript[] liste_plateformesShack;

    PlateformeScript[] ground;


    public Image bille;
    public List<Biles> billesLi = new List<Biles>();

    public Text Debug2;
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
    Animation platAnimSch = new Animation("PlatformSchack", 0.05f, 1);



    private Vector3 lastPosCam;
    private Vector3 lastDepCam;
    public Text timerT;

    public Text speed;
    float scale = Screen.width / 1600.0f;



    public AudioSource BG_sound;
    public AudioSource jumpSound;
    public AudioSource fallUnderMapSound;
    public AudioSource hitWallSound;
    public AudioSource boostSound;
    public AudioSource pickupSound;


    void Start() {
        //mainScene.gameObject.active = true;

        Debug.Log("b");

        mainGameIsRunning = false;
        isInMenu = true;


        //w = new World(null, null, null);

        BG_sound.PlayOneShot(BG_sound.clip, 0.6f);


        /************************************************/

        liste_plateformes = listePlateformes.transform.GetComponentsInChildren<PlateformeScript>();
        liste_plateformes2 = listePlateformes2.transform.GetComponentsInChildren<PlateformeScript>();
        liste_plateformes3 = listePlateformes3.transform.GetComponentsInChildren<PlateformeScript>();
        liste_plateformesShack = listePlateformesSchack.transform.GetComponentsInChildren<PlateformeScript>();
        liste_plateformesMvt = listePlateformesMvt.transform.GetComponentsInChildren<PlateformeScript>();
        ground = groundI.transform.GetComponentsInChildren<PlateformeScript>();


        joueur = new Personnage(new Vector3(imageJoueur.transform.position.x, imageJoueur.transform.position.y, 0), 
            new Vector3(imageJoueur.rectTransform.rect.width, imageJoueur.rectTransform.rect.height, 0), new Vector3(0, 0, 0),
            Resources.Load<Sprite>("DSC02576"), imageJoueur, joueurAnim, imageJoueurF, imageJoueurTr, 
            new Vector3(imageJoueur.transform.position.x, imageJoueur.transform.position.y, 0),
            jumpSound,fallUnderMapSound,hitWallSound,boostSound
            );

        virusOb = new Virus(new Vector3(virus.transform.position.x, virus.transform.position.y, 0), 
            new Vector3(13*scale, 0*scale, 0), new Vector3(virus.rectTransform.rect.width, virus.rectTransform.rect.height, 0), 
            Resources.Load<Sprite>("DSC02576"), virus, 0.8f, bille, AnimVirus,billeAnim, pickupSound);
    
        /*Image b = Instantiate(bille);
        b.transform.SetParent(billeParent.transform);
        billesLi.Add(new Biles(new Vector3(300, 300, 0), new Vector3(0, 0, 0), new Vector3(10, 10, 0), Resources.Load<Sprite>("DSC02576"), b,1, billeAnim));
        */

        for (int i = 0; i < liste_plateformes.Length; ++i)
        {
            liste_plateformes[i].plateform.anim = platAnim;
            ptemp.Add(liste_plateformes[i].plateform);
        }


        for (int i = 0; i < liste_plateformes2.Length; ++i)
        {
            liste_plateformes2[i].plateform.anim = platAnim2;
            ptemp.Add(liste_plateformes2[i].plateform);
        }


        for (int i = 0; i < liste_plateformes3.Length; ++i)
        {
            liste_plateformes3[i].plateform.anim = platAnim3;
            ptemp.Add(liste_plateformes3[i].plateform);
        }


        for (int i = 0; i < ground.Length; ++i)
        {
            ground[i].plateform.anim = platAnimGr;
            ptemp.Add(ground[i].plateform);
        }

        for (int i = 0; i < liste_plateformesShack.Length; ++i)
        {
            liste_plateformesShack[i].plateform.anim = platAnimSch;
            liste_plateformesShack[i].plateform.isSchaky = true;
            ptemp.Add(liste_plateformesShack[i].plateform);
        }
        for (int i = 0; i < liste_plateformesMvt.Length; ++i)
        {
            liste_plateformesMvt[i].plateform.anim = platAnimSch;
            liste_plateformesMvt[i].plateform.isSchaky = true;
            //liste_plateformesMvt[i].plateform.amplitudeY = 100;
            //liste_plateformesMvt[i].plateform.periodeX = 3;
            ptemp.Add(liste_plateformesMvt[i].plateform);
        }

        w = new World(joueur, ptemp, virusOb, billesLi, bille, billeParent);
        /************************************************/

        //mainScene.gameObject.SetActive(false);
        //mainMenu.gameObject.SetActive(true);
        scenePosBegin = mainScene.rectTransform.position;
        lastPosCam = w.getPlayer().position;
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
    void Update()
    {
        if (w != null)
        {
            float dt = Time.deltaTime;

            iconeVirusAn.sprite = AnimVirus.image;

            if (Mathf.Abs(w.getPlayer().position.x + 400 - w.virus.position.x) > Screen.width / 2f || Mathf.Abs(w.getPlayer().position.y - w.virus.position.y) > Screen.height / 2f)
            {
                iconeVirus.gameObject.SetActive(true);
                float positonX;
                float positonY;
                Vector3 diffPos = -(w.getPlayer().position + new Vector3(400, 0, 0) - w.virus.position);
                //  Debug.Log(diffPos);
                if (diffPos.x < 0)
                {
                    positonX = Mathf.Max(diffPos.x, -Screen.width / 3.4f);
                }
                else
                {
                    positonX = Mathf.Min(diffPos.x, Screen.width / 3.4f);
                }

                if (diffPos.y < 0)
                {
                    positonY = Mathf.Max(diffPos.y, -Screen.height / 2.2f);
                }
                else
                {
                    positonY = Mathf.Min(diffPos.y, Screen.height / 2.2f);
                }

                iconeVirus.rectTransform.position = new Vector3(positonX + Screen.width / 2f, positonY + Screen.height / 2f, 0);

            }
            else
            {
                iconeVirus.gameObject.SetActive(false);
            }

            timerT.text = Mathf.Floor(w.timer) + " / " + w.maxTime;
            //mainScene.rectTransform.position = w.getPlayer().position;

            float cible = w.getPlayer().position.y + 200.0f * scale;
            float direction = cible - c1.transform.position.y;

            Vector3 diffPosCamPlayer = w.getPlayer().position + new Vector3(400, 0, 0) * scale - lastPosCam;


            Vector3 diffPOsDep = diffPosCamPlayer * dt * 5;
            Vector3 maxDiff = lastDepCam * dt * 6;

            if (diffPOsDep.magnitude > maxDiff.magnitude)
            {
                lastPosCam += maxDiff;
            }
            else
            {
                lastPosCam += diffPOsDep;
            }


            c1.transform.position = new Vector3(w.getPlayer().position.x, lastPosCam.y, 0) + new Vector3(400, 0, 0) * scale;
            //c1.transform.position = new Vector3(w.getPlayer().position.x, w.getPlayer().position.y, 0) + new Vector3(400, 0, 0) * scale;

            //c1.transform.position = lastPosCam;
            BG.rectTransform.position = new Vector3(c1.transform.position.x, c1.transform.position.y, 0) / 5;

            lastDepCam = diffPosCamPlayer;

            //Debug.Log(direction+"");
            //c1. transform.position = new Vector3(w.getPlayer().position.x, w.getPlayer().position.y/3.0f,0) +new Vector3(400,200,0) * scale;
            /*
            float vitesseTracking = 10.0f;
            if (Mathf.Abs(direction) > vitesseTracking)
            {
                c1.transform.position = new Vector3(w.getPlayer().position.x, c1.transform.position.y + direction / Mathf.Abs(direction) * vitesseTracking, 0) + new Vector3(400, 0, 0) * scale;
            }
            else
            {
                c1.transform.position = new Vector3(w.getPlayer().position.x, w.getPlayer().position.y, 0) + new Vector3(400, 200, 0) * scale;
            }*/
            //c2.transform.position = new Vector3(0, -100, 0);

            speed.text = "";
            for (int i = 0; i < w.getPlayer().facteurVitesse / 5; ++i)
            {
                speed.text += "|";
            }

            if (mainGameIsRunning)
            {
                w.update(dt);

                if (w.hasWin)
                {
                    SceneManager.LoadScene("win");
                    //youWinText.rectTransform.position = new Vector3(200, 200, 0);
                }


                if (w.hasLoos)
                {
                    SceneManager.LoadScene("loos");
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
}
