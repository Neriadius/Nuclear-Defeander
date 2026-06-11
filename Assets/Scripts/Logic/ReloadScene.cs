using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
       
        public void ReloadMainScene()
    {
                   SceneManager.LoadScene(0);
    }
    
}
