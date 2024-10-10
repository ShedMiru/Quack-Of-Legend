using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public GameObject[] backsound;
    public void LoadScene(string aValue)
    {
        //Melakukan perpindahan antar scene. Catatan: Scene yang dipanggil sudah didaftarkan di Build Setting
        SceneManager.LoadScene(aValue);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void CutSound()
    {
        backsound = GameObject.FindGameObjectsWithTag("Audio");
        foreach (GameObject backs in backsound)
        {
            SceneManager.MoveGameObjectToScene(backs, SceneManager.GetActiveScene());   
        }
    }
}