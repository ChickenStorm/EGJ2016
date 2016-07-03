using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Personnage : Entity
{
    public bool toucheEnfoncerD { get; set; }
    public bool toucheEnfoncerA { get; set; }
    public bool toucheEnfoncerSpace { get; set; }
    public float facteurVitesse { get; set; }
    public bool toucheEnfoncerR { get; set; }

    private float timerResp = 0;
    private float MaxtimerResp = 3;

    private bool hasRespawn = false;


    private AudioSource jumpSound;
    private AudioSource fallUnderMapSound;
    private AudioSource hitWallSound;
    private AudioSource boostSound;


    public Vector3 respawnPos { get; set; }

    float vitesseMin;
    float timerCollision;
    bool aDejaSaute;
    bool timerActive;
    bool enSaut;
    public Image playerFast;
    public Image playerTr;
    public Animation voidAnim = new Animation("void", 0.05f, 1);
    int numberOfDeath = 0;
    Vector3 dimensionInitiale;

    float scale = Screen.width / 1600.0f;

    float timeFaste1;
    float timeFaste2;

    private bool hasPlayAccelerationSound = false;

    private float timerFastSound = 0;
    private const float MaxtimerFastSound = 2.5f;

    private Animation AnimationStill = new Animation("joueurStill", 0.05f, 1);
    private Animation AnimationFast = new Animation("joueurFast", 0.05f, 5);
    private Animation AnimationTransit = new Animation("joueurTransit", 0.05f, 11);


    Vector3 deplacementCible;
    Vector3 deplacementPlateforme; 

    public Personnage(Vector3 pos, Vector3 dim, Vector3 vit, Sprite spri, Image im, Animation anim, Image playerFastP,
        Image playerTrP, Vector3 respawnPosP, AudioSource jumpSoundP, AudioSource fallUnderMapSoundP, AudioSource hitWallSoundP,
        AudioSource boostSoundP)
        : base(pos, dim, vit, false, spri, im, anim)
    {
        jumpSound = jumpSoundP;
        fallUnderMapSound = fallUnderMapSoundP;
        hitWallSound = hitWallSoundP;
        boostSound = boostSoundP;
        voidAnim.update(1);

        toucheEnfoncerD = false;
        toucheEnfoncerA = false;
        toucheEnfoncerSpace = false;
        vitesseMin = 24;

        dimensionInitiale = dim * scale;
        facteurVitesse = vitesseMin;

        timerCollision = 0;
        timerActive = false;

        aDejaSaute = false;
        enSaut = false;
        playerFast = playerFastP;
        playerTr = playerTrP;
        respawnPos = respawnPosP;
        deplacementPlateforme = new Vector3();
    }

    /*public Personnage(Image im) : this(new Vector3(), new Vector3(), new Vector3(), new Sprite(),im)
    {

    }*/

    private void respawn(World w)
    {
        position = respawnPos + new Vector3(0, 100, 0);
        ++numberOfDeath;
        w.virus.setToInitialPosWithOfSet(numberOfDeath, this);
        facteurVitesse = vitesseMin;
        fallUnderMapSound.PlayOneShot(fallUnderMapSound.clip, 0.8f);
    }

    public void playHitWall()
    {
        hitWallSound.PlayOneShot(hitWallSound.clip, 0.8f);
    }

    public override void update(float dt, World w)
    {
        if (hasPlayAccelerationSound)
        {
            timerFastSound += dt;
            if (timerFastSound >= MaxtimerFastSound)
            {
                hasPlayAccelerationSound = false;
                timerFastSound = 0;
            }
        }
        if (hasRespawn)
        {
            timerResp += dt;
            if (timerResp >= MaxtimerResp)
            {
                hasRespawn = false;
                timerResp = 0;
            }
        }
        //base.update(dt,w);
        if (position.y < -1000)
        {

            /*position = new Vector3(0, 1300, 0);
            ++numberOfDeath;
            w.virus.setToInitialPosWithOfSet(numberOfDeath,this);
            //w.virus.position = new Vector3(200+300* numberOfDeath, 1000, 0);
            facteurVitesse = vitesseMin;*/
            respawn(w);
        }

        //deplacer joueur
        playerFast.rectTransform.position = position + (new Vector3(dimension.x * im.rectTransform.pivot.x, dimension.y * im.rectTransform.pivot.y, 0));
        playerTr.rectTransform.position = position + (new Vector3(dimension.x * im.rectTransform.pivot.x, dimension.y * im.rectTransform.pivot.y, 0));

        im.rectTransform.position = position + (new Vector3(dimension.x * im.rectTransform.pivot.x, dimension.y * im.rectTransform.pivot.y, 0));



        if (Input.GetKeyDown(KeyCode.C))
            toucheEnfoncerD = true;
        /*if (Input.GetKeyDown(KeyCode.R))
            toucheEnfoncerR = true;*/
        if (Input.GetKeyUp(KeyCode.C))
            toucheEnfoncerD = false;
        if (Input.GetKeyDown(KeyCode.Y))
            toucheEnfoncerA = true;
        if (Input.GetKeyUp(KeyCode.Y))
            toucheEnfoncerA = false;
        /*if (Input.GetKeyUp(KeyCode.R))
            toucheEnfoncerR = false;*/
        if (Input.GetKeyDown(KeyCode.X))
        {
            foreach (Plateform p in w.platforms)
                saut(p);
        }

        if(vitesse.y > 0)
        {
        if (Input.GetKeyUp(KeyCode.X))
            {
            vitesse = new Vector3(vitesse.x, 0, vitesse.z);
        }
        }

        if (Input.GetKeyDown(KeyCode.R) && !hasRespawn)
        {
            hasRespawn = true;
            respawn(w);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            facteurVitesse += 2;
        }

        deplacer(dt);
        deplacementPlateforme = new Vector3();
        foreach (Plateform p in w.platforms)
            deplacementShaky(p);
        foreach (Plateform p in w.platforms)
            collision(p);
        validerDeplacement();
        //Debug.text = joueur.position.x + ","+ joueur.position.y;

        if (timerActive)
        {
            timerCollision += dt;
        }
        //Debug.Log(timerCollision);
        if (timerCollision > 2)
        {
            timerActive = false;
            timerCollision = 0;
        }

        //Debug.Log(deplacementCible.x);
        float animMult = Mathf.Sign(deplacementCible.x);


        if (Mathf.Abs(deplacementCible.x) < 0.5f)
        {
            im.gameObject.SetActive(true);
            playerFast.gameObject.SetActive(false);
            playerTr.gameObject.SetActive(false);
            //im.transform.Translate(new Vector3(0, 0, 0));
            //im.transform.localScale = new Vector3(1, 1, 1);
            dimension = dimensionInitiale;
            //im.rectTransform.rect.Set(0100, 100, 2000, 100);
            //playerFast.sprite = voidAnim.image;
            //playerTr.sprite = voidAnim.image;
            AnimationStill.update(dt);
            im.sprite = AnimationStill.image;
            timeFaste1 = 0;
            timeFaste2 = 0;
            //im.r
        }
        else if (Mathf.Abs(deplacementCible.x) > 15)
        {

            if (timeFaste1 < 0.5)
            {
                //playAccelerationSound = false;
                im.gameObject.SetActive(true);
                playerFast.gameObject.SetActive(false);
                playerTr.gameObject.SetActive(false);
                timeFaste1 += dt;
                anim.update(dt * facteurVitesse / 100);
                im.sprite = anim.image;
                AnimationTransit.timer = 0;
            }
            else if (timeFaste2 < 0.5)
            {
                if (!hasPlayAccelerationSound)
                {
                    hasPlayAccelerationSound = true;
                    boostSound.PlayOneShot(boostSound.clip, 0.2f);
                }
                im.gameObject.SetActive(false);
                playerFast.gameObject.SetActive(false);
                playerTr.gameObject.SetActive(true);
                timeFaste2 += dt;
                AnimationTransit.update(dt);

                playerTr.sprite = AnimationTransit.image;
            }
            else
            {
                //playAccelerationSound = false;
                //AnimationTransit.timer = 0;
                //im.rectTransform.anchoredPosition = new Vector2(0.01f, 0);
                //im.transform.Translate(new Vector3(-150, 0, 0));
                //im.transform.localScale = new Vector3(2.7972027972027972027972027972028f, 1, 1);
                im.gameObject.SetActive(false);
                playerFast.gameObject.SetActive(true);
                playerTr.gameObject.SetActive(false);
                //dimension = new Vector3(200, 100, 0);
                //im.rectTransform.anchoredPosition = new Vector2(0, 0);
                AnimationFast.update(dt);
                playerFast.sprite = AnimationFast.image;
            }
        }
        else
        {
            //AnimationStill.hasUpdate = false;
            //im.rectTransform.rect.width = 71.5f;
            im.gameObject.SetActive(true);
            playerFast.gameObject.SetActive(false);
            playerTr.gameObject.SetActive(false);

            //im.transform.localScale = new Vector3(1, 1, 1);
            dimension = dimensionInitiale;
            anim.update(dt * facteurVitesse / 15);
            im.sprite = anim.image;
            timeFaste1 = 0;
            timeFaste2 = 0;
        }
        if (deplacementCible.x < 0)
        {
            im.transform.localScale = new Vector3(-1, 1, 1);
            playerFast.transform.localScale = new Vector3(-1, 1, 1);
            playerTr.transform.localScale = new Vector3(-1, 1, 1);
        }
        if (deplacementCible.x > 0)
        {
            playerFast.transform.localScale = new Vector3(1, 1, 1);
            playerTr.transform.localScale = new Vector3(1, 1, 1);
            im.transform.localScale = new Vector3(1, 1, 1);
        }
    }



    public void deplacer(float dt)
    {
        deplacementCible = new Vector3(deplacementCible.x, 0, deplacementCible.z);
        if (toucheEnfoncerD)
        {
            deplacementCible = new Vector3(facteurVitesse * 40 * dt, 0, 0) * scale;
            if (vitesse.x < 10 && vitesse.x > 0)
                vitesse = new Vector3(vitesse.x + 1 * scale, vitesse.y, vitesse.z);
        }
        if (toucheEnfoncerA)
        {

            deplacementCible = new Vector3(-facteurVitesse * 40 * dt, 0, 0) * scale;
            if (vitesse.x > -10 && vitesse.x < 0)
                vitesse = new Vector3(vitesse.x - 1 * scale, vitesse.y, vitesse.z);
        }
        vitesse -= new Vector3(0, 4 * 30 * dt * scale, 0);//gravite
        vitesse = new Vector3(vitesse.x * 0.6f, vitesse.y, vitesse.z);
        deplacementCible += vitesse;
        deplacementCible = new Vector3(deplacementCible.x * 0.6f, deplacementCible.y, deplacementCible.z);

        if (Mathf.Abs(deplacementCible.x) < 1e-3)
        {
            deplacementCible.x = 0;
        }

        aDejaSaute = false;
    }

    public void saut(Plateform platef)
    {
        float marge = 6.0f * scale;
        float xGauche = position.x - dimension.x * (im.rectTransform.pivot.x);
        float xDroite = position.x + dimension.x * (1 - im.rectTransform.pivot.x);
        float yBas = position.y - dimension.y * (im.rectTransform.pivot.y);
        float yHaut = position.y + dimension.y * (1 - im.rectTransform.pivot.y);
        //if ((position.y - 15 < platef.position.y + platef.dimension.y) && (position.y - 15 > platef.position.y) && !aDejaSaute
        //    && (position.x + dimension.x - 15 > platef.position.x) && (position.x + 15 < platef.position.x + platef.dimension.x))//(position.x-marge < platef.position.x + platef.dimension.x) && (position.x + dimension.x + marge > platef.position.x))
        //{
        if (!enSaut)
        {
            jumpSound.PlayOneShot(jumpSound.clip, 0.8f);
            vitesse = new Vector3(vitesse.x, vitesse.y + 30 * scale, vitesse.z);
            aDejaSaute = true;
            enSaut = true;
        }
        //}
    }

    public void validerDeplacement()
    {
        position += deplacementCible+deplacementPlateforme;

    }

    public void collision(Plateform platef)
    {
        if (!(position.x + dimension.x < platef.position.x - 30 || position.x > platef.position.x + platef.dimension.x + 30))
        {
            if (platef != null)
            {
                Vector3 nPosition = (position + deplacementCible);

                float xGauche = nPosition.x - dimension.x * (0);
                float xDroite = nPosition.x + dimension.x * (1 - 0);
                float yBas = nPosition.y - dimension.y * (0);
                float yHaut = nPosition.y + dimension.y * (1 - 0);


                float marge = 5*scale;
                //Debug.Log(nPosition.y + ", " + dimension.x + " : " + platef.position.x + " , " + platef.dimension.x);
                if ((xDroite - marge > platef.position.x && xDroite - marge < platef.position.x + platef.dimension.x) ||
                    (xGauche + marge < platef.position.x + platef.dimension.x && xGauche + marge > platef.position.x))
                {
                    //Debug.Log(nPosition);
                    if (position.y + marge < platef.position.y + platef.dimension.y && position.y + dimension.y - marge > platef.position.y)
                    {
                        vitesse = new Vector3(0, vitesse.y, vitesse.z);
                        Debug.Log("DC:"+deplacementCible.x);
                        if (!timerActive && deplacementCible.x>9.0f)
                        {
                            //Choc
                            Debug.Log("Choc");
                            facteurVitesse -= 10;
                            timerActive = true;
                            playHitWall();
                        }
                        deplacementCible.x = 0;
                        if (facteurVitesse <= vitesseMin)
                        {
                            facteurVitesse = vitesseMin;
                        }
                    }
                }
                if ((yHaut - marge > platef.position.y && yHaut - marge < platef.position.y + platef.dimension.y) || (yBas + marge < platef.position.y + platef.dimension.y && yBas + marge > platef.position.y))
                {
                    if (position.x + marge < platef.position.x + platef.dimension.x && position.x + dimension.x - marge > platef.position.x)
                    {
                        deplacementCible.y = 0;
                        vitesse = new Vector3(vitesse.x, 0, vitesse.z);
                        enSaut = false;
                    }
                }
                //return false;
            }
        }
    }

    public void deplacementShaky(Plateform platef)
    {
        if (!(position.x + dimension.x < platef.position.x - 30 || position.x > platef.position.x + platef.dimension.x + 30))
        {
            if (platef != null)
            {

                Vector3 nPosition = (position + deplacementCible);

                float xGauche = nPosition.x - dimension.x * (0);
                float xDroite = nPosition.x + dimension.x * (1 - 0);
                float yBas = nPosition.y - dimension.y * (0);
                float yHaut = nPosition.y + dimension.y * (1 - 0);


                float marge = 5 * scale;
                //Debug.Log(nPosition.y + ", " + dimension.x + " : " + platef.position.x + " , " + platef.dimension.x);

                if ((yHaut - marge > platef.position.y && yHaut - marge < platef.position.y + platef.dimension.y) || (yBas + marge < platef.position.y + platef.dimension.y && yBas + marge > platef.position.y))
                {
                    if (position.x + marge < platef.position.x + platef.dimension.x && position.x + dimension.x - marge > platef.position.x)
                    {

                        deplacementPlateforme = platef.deplacement;

                    }
                }
                //return false;
            }
        }
    }

}
