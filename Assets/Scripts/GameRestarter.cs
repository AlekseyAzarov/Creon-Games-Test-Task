using UnityEngine.SceneManagement;

public static class GameRestarter
{
    public static void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
