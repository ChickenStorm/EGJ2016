using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class main_loose : MonoBehaviour {

    public Button startGameButton;

    void CreatGameSceneDefault()
    {
        //Application.LoadLevel("Jeu");
        SceneManager.LoadScene("Jeu");
    }

    // Use this for initialization
    void Start () {
        startGameButton.onClick.AddListener(() => { CreatGameSceneDefault(); });
    }

    

    // Update is called once per frame
    void Update () {
	
	}
}
