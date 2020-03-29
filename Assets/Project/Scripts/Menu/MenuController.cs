using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        // To show cursor after First Person Controller
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    
    public void OnPlay()
    {
        SceneManager.LoadScene("Level1");
    }

}