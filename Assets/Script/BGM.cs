using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGM : MonoBehaviour
{
    public static BGM instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DontDestroyScene()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void ActiveScene()
    {
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
    }
}
