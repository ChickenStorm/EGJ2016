using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlateformeScript : MonoBehaviour {

    public Plateform plateform;
    public Image image;
    Animation platAnim = new Animation("Platform", 0.05f, 1);
    public float dephasageX;
    public float amplitudeX;

    // Use this for initialization
    void Start () {
        float x = image.rectTransform.position.x;
        float y = image.rectTransform.position.y;
        float largeur = image.rectTransform.rect.width;
        float longueur = image.rectTransform.rect.height;

        plateform = new Plateform(new Vector3(x, y, 0), new Vector3(largeur, longueur, 0),image, platAnim, dephasageX, amplitudeX);
        
        Debug.Log(plateform.dimension.x + ", " + plateform.dimension.y);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
