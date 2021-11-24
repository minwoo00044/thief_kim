using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnclickNewGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnclickLoad()
    {
        Debug.Log("불러오기");
    }

    public void OnClickOption()
    {
        Debug.Log("옵션");
    }

    public void OnclickQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
