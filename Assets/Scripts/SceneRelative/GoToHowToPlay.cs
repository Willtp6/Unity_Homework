using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GoToHowToPlay : MonoBehaviour, IPointerClickHandler
{
    public int sceneIndex = 2;
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
}
