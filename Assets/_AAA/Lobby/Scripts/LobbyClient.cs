using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyClient : MonoBehaviour
{
    private void Start()
    {
        LoadGameServer();
        LoadGameClient();
    }
    
    private void LoadGameServer()
    {
        SceneManager.LoadScene("GameServer", LoadSceneMode.Additive);
    }
    
    private void LoadGameClient()
    {
        SceneManager.LoadScene("GameClient", LoadSceneMode.Additive);
    }
}