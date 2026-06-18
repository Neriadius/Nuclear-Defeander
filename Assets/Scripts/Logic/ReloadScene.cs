using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    //public scoreObject Operator;

    private void Awake()
    {
        //Operator = FindFirstObjectByType<scoreObject>();
    }

    public void ReloadMainScene()
    {
        //Operator.onlyOneTime = true;
        SceneManager.LoadScene(0);
        
    }
    
}
