using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class main_menu : MonoBehaviour {

    public Button startGameButton;


    // Use this for initialization
    void Start () {
        startGameButton.onClick.AddListener(() => { CreatGameSceneDefault(); });
    }
    void CreatGameSceneDefault() {
        //Application.LoadLevel("Jeu");
        SceneManager.LoadScene("Jeu");
    }
    // Update is called once per frame
    void Update () {
	
	}
}
