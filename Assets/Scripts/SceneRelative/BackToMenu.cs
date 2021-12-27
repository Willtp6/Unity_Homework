using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour, IPointerClickHandler
{
    public int sceneIndex = 0;
    public static int bestScore;
    public void OnPointerClick(PointerEventData eventData)
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(sceneIndex);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void back()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(0);
    }
}
