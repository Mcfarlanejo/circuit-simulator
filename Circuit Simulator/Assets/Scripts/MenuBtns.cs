using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuBtns : MonoBehaviour
{
        
    public void StartGame()
    {
        SceneManager.LoadScene("House");
    }
}
