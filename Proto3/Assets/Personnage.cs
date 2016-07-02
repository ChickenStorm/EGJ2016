using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Personnage : Entity
{
    public bool toucheEnfoncerD { get; set; }
    public bool toucheEnfoncerA { get; set; }
    public bool toucheEnfoncerSpace { get; set; }
    public float facteurVitesse { get; set; }

    bool aDejaSaute;

    Vector3 deplacementCible;

    public Personnage(Vector3 pos, Vector3 dim, Vector3 vit, Sprite spri,Image im) : base(pos, dim, vit, false, spri,im)
    {
        toucheEnfoncerD = false;
        toucheEnfoncerA = false;
        toucheEnfoncerSpace = false;
        facteurVitesse = 5;


        aDejaSaute = false;
    }

    public Personnage(Image im) : this(new Vector3(), new Vector3(), new Vector3(), new Sprite(),im)
    {

    }

    public override void update(float dt,World w) {
        //base.update(dt,w);


        //deplacer joueur
        im.rectTransform.position = position;

        if(Input.GetKeyDown(KeyCode.D))
            toucheEnfoncerD = true;
        if(Input.GetKeyUp(KeyCode.D))
            toucheEnfoncerD = false;
        if(Input.GetKeyDown(KeyCode.A))
            toucheEnfoncerA = true;
        if(Input.GetKeyUp(KeyCode.A))
            toucheEnfoncerA = false;
        if(Input.GetKeyDown(KeyCode.W))
            toucheEnfoncerD = true;
        if(Input.GetKeyDown(KeyCode.Space)) {
        foreach(Plateform p in w.platforms)
            saut(p);
        }

        deplacer();
        foreach(Plateform p in w.platforms)
            collision(p);
        validerDeplacement();
        //Debug.text = joueur.position.x + ","+ joueur.position.y;
    }

    public void deplacer()
    {
        deplacementCible = new Vector3();
        if (toucheEnfoncerD)
        {
            deplacementCible = new Vector3(facteurVitesse, 0, 0);
        }
        if (toucheEnfoncerA)
        {
            deplacementCible = new Vector3(-facteurVitesse, 0, 0);
        }
        vitesse -= new Vector3(0, 1, 0);
        deplacementCible += vitesse;

        aDejaSaute = false; 
    }

    public void saut(Plateform platef)
    {
        if (position.y - 15 < platef.position.y + platef.dimension.y && !aDejaSaute)
        {
            vitesse += new Vector3(0, 20, 0);
            aDejaSaute = true;
        }
    }

    public void validerDeplacement()
    {
        position += deplacementCible;
    }

    public bool collision(Plateform platef)
    {
        Vector3 nPosition = position + deplacementCible;
        //Debug.Log(nPosition.y + ", " + dimension.x + " : " + platef.position.x + " , " + platef.dimension.x);
        if ((nPosition.x + dimension.x > platef.position.x && nPosition.x + dimension.x < platef.position.x + platef.dimension.x) || (nPosition.x < platef.position.x + platef.dimension.x && nPosition.x > platef.position.x))
        {
            Debug.Log(nPosition);
            if (position.y < platef.position.y + platef.dimension.y && position.y + dimension.y > platef.position.y)
            {
                Debug.Log("ok");
                deplacementCible.x = 0;
                vitesse = new Vector3(0, vitesse.y, vitesse.z);
            }
        }
        if ((nPosition.y + dimension.y > platef.position.y && nPosition.y + dimension.y < platef.position.y + platef.dimension.y) || (nPosition.y < platef.position.y + platef.dimension.y && nPosition.y > platef.position.y))
        {
            if (position.x < platef.position.x + platef.dimension.x && position.x + dimension.x > platef.position.x)
            {
                deplacementCible.y = 0;
                vitesse = new Vector3(vitesse.x, 0, vitesse.z);
            }
        }
        return false;
    }
}
