using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Personnage : Entity
{
    public bool toucheEnfoncerD { get; set; }
    public bool toucheEnfoncerA { get; set; }
    public bool toucheEnfoncerSpace { get; set; }
    public float facteurVitesse { get; set; }
    float vitesseMin;
    float timerCollision;
    bool aDejaSaute;
    bool timerActive;


    float timeFaste1;
    float timeFaste2;


    private Animation AnimationStill = new Animation("joueurStill", 0.05f, 1);
    private Animation AnimationFast = new Animation("joueurFast", 0.05f, 5);
    private Animation AnimationTransit = new Animation("joueurTransit", 0.05f, 1);


    Vector3 deplacementCible;

    public Personnage(Vector3 pos, Vector3 dim, Vector3 vit, Sprite spri, Image im, Animation anim) : base(pos, dim, vit, false, spri, im, anim)
    {
        toucheEnfoncerD = false;
        toucheEnfoncerA = false;
        toucheEnfoncerSpace = false;
        vitesseMin = 10;
        facteurVitesse = vitesseMin;
        timerCollision = 0;
        timerActive = false;
        aDejaSaute = false;
    }

    /*public Personnage(Image im) : this(new Vector3(), new Vector3(), new Vector3(), new Sprite(),im)
    {

    }*/


    public override void update(float dt, World w)
    {

        //base.update(dt,w);
        if (position.y < -1000)
        {
            position = new Vector3(0, 500, 0);
        }

        //deplacer joueur
        im.rectTransform.position = position;

        if (Input.GetKeyDown(KeyCode.D))
            toucheEnfoncerD = true;
        if (Input.GetKeyUp(KeyCode.D))
            toucheEnfoncerD = false;
        if (Input.GetKeyDown(KeyCode.A))
            toucheEnfoncerA = true;
        if (Input.GetKeyUp(KeyCode.A))
            toucheEnfoncerA = false;
        if (Input.GetKeyDown(KeyCode.W))
            toucheEnfoncerD = true;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (Plateform p in w.platforms)
                saut(p);
        }

        deplacer(dt);
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
            AnimationStill.update(dt);
            im.sprite = AnimationStill.image;
            timeFaste1 = 0;
            timeFaste2 = 0;
            im.rectTransform.rect.width = 71.5f;
        }
        else if (Mathf.Abs(deplacementCible.x) > 11)
        {

            if (timeFaste1 < 1)
            {
                timeFaste1 += dt;
                anim.update(dt * facteurVitesse / 5);
                im.sprite = anim.image;
                AnimationTransit.timer = 0;
            }
            else if (timeFaste2 < 0.1)
            {
                timeFaste2 += dt;
                AnimationTransit.update(dt);

                im.sprite = AnimationTransit.image;
            }
            else
            {
                //AnimationTransit.timer = 0;
                im.rectTransform = 200;
                AnimationFast.update(dt);
                im.sprite = AnimationFast.image;
            }
        }
        else
        {
            //AnimationStill.hasUpdate = false;
            im.rectTransform.rect.width = 71.5f;
            anim.update(dt * facteurVitesse / 5);
            im.sprite = anim.image;
            timeFaste1 = 0;
            timeFaste2 = 0;
        }
        /*if (deplacementCible.x < 0) {
            im.transform.localScale = new Vector3 (-1,1,1);
        }
        if (deplacementCible.x > 0)
        {
            im.transform.localScale = new Vector3(1, 1, 1);
        }*/
    }



    public void deplacer(float dt)
    {
        deplacementCible = new Vector3(deplacementCible.x, 0, deplacementCible.z);
        if (toucheEnfoncerD)
        {
            deplacementCible = new Vector3(facteurVitesse * 30 * dt, 0, 0);
            if (vitesse.x < 10 && vitesse.x>0)
                vitesse = new Vector3(vitesse.x + 1, vitesse.y, vitesse.z);
        }
        if (toucheEnfoncerA)
        {

            deplacementCible = new Vector3(-facteurVitesse * 30 * dt, 0, 0);
            if (vitesse.x > -10 && vitesse.x<0)
                vitesse = new Vector3(vitesse.x - 1, vitesse.y, vitesse.z);
        }
        vitesse -= new Vector3(0, 1 * 30 * dt, 0);
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
        float marge = 6.0f;
        if ((position.y - 15 < platef.position.y + platef.dimension.y) && (position.y - 15 > platef.position.y) && !aDejaSaute 
            && (position.x + dimension.x -15 > platef.position.x) && (position.x +15 < platef.position.x + platef.dimension.x))//(position.x-marge < platef.position.x + platef.dimension.x) && (position.x + dimension.x + marge > platef.position.x))
        {
            GameObject.Find("Debug2").GetComponent<Text>().text = position + ";"+platef.position +";"+platef.dimension;

            vitesse += new Vector3(0, 25, 0);
            aDejaSaute = true;
        }
    }

    public void validerDeplacement()
    {
        position += deplacementCible;
    }

    public void collision(Plateform platef)
    {
        if (!(position.x + dimension.x < platef.position.x - 30 || position.x > platef.position.x + platef.dimension.x + 30))
        {

            if (platef != null)
            {
                float marge = 5;
                Vector3 nPosition = position + deplacementCible;
                //Debug.Log(nPosition.y + ", " + dimension.x + " : " + platef.position.x + " , " + platef.dimension.x);
                if ((nPosition.x + dimension.x - marge > platef.position.x && nPosition.x + dimension.x - marge < platef.position.x + platef.dimension.x) || (nPosition.x + marge < platef.position.x + platef.dimension.x && nPosition.x + marge > platef.position.x))
                {
                    //Debug.Log(nPosition);
                    if (position.y + marge < platef.position.y + platef.dimension.y && position.y + dimension.y - marge > platef.position.y)
                    {

                         Debug.Log("ok");
                        deplacementCible.x = 0;
                        vitesse = new Vector3(0, vitesse.y, vitesse.z);

                        if (!timerActive)
                        {
                            facteurVitesse -= 3;
                            timerActive = true;
                        }
                        if (facteurVitesse <= vitesseMin)
                        {
                            facteurVitesse = vitesseMin;
                        }
                        Debug.Log(facteurVitesse);
                    }
                }
                if ((nPosition.y + dimension.y - marge > platef.position.y && nPosition.y + dimension.y - marge < platef.position.y + platef.dimension.y) || (nPosition.y + marge < platef.position.y + platef.dimension.y && nPosition.y + marge > platef.position.y))
                {
                    if (position.x + marge < platef.position.x + platef.dimension.x && position.x + dimension.x - marge > platef.position.x)
                    {
                       
                        
                            Debug.Log("ko "+platef.position + ";" +platef.dimension);
                            deplacementCible.y = 0;
                            vitesse = new Vector3(vitesse.x, 0, vitesse.z);
                    }
                }
                //return false;
            }
            else
            {
            }
        }
    }


}
