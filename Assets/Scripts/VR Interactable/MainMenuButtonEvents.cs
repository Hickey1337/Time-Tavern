using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonEvents : MonoBehaviour
{
    public void goToGame(string name)
    {
        SceneManager.LoadScene(name);
    }
}
