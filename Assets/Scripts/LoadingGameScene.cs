using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingGameScene : MonoBehaviour
{
    public void LoadGameScene(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("Demo 2");
    }
}
