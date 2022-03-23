using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    void Start()
    {
        
    }
   public void PlayAgain()
    {
      
        SceneManager.LoadScene(0);
    }
    public void NextLevel()
    {
        
        SceneManager.LoadScene(1);
            
    }
    public void Reset()
    {
        GameManager.islevelEnd = false;
        GameManager.isGameOver = false;
        GameManager.isGameStart = true;
    }

}
