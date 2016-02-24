using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    private SceneManager SceneManagement;

    public void OnClick()
    {
        SceneManager.LoadScene("AustinScene");
    }

}
