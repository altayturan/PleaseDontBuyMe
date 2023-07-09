using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LoadMainMenu : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    private void Start()
    {
        videoPlayer.loopPointReached += LoadMainMenuScene;
    }

    private void LoadMainMenuScene(VideoPlayer source)
    {
        SceneManager.LoadScene("MainMenu");
    }
}
