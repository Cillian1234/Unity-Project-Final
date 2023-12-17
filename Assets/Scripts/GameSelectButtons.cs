using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSelectButtons : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        MoveToScene();
    }

    private void MoveToScene()
    {
        if (CompareTag("Game.Main"))
        {
            SceneManager.LoadScene(0);
        } else if (CompareTag("Game.Tetris"))
        {
            SceneManager.LoadScene(1);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        } else if (CompareTag("Game.Snake"))
        {
            SceneManager.LoadScene(2);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        } else if (CompareTag("Game.2DShooter"))
        {
            SceneManager.LoadScene(3);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        } else if (CompareTag("Github"))
        {
            Application.OpenURL("https://github.com/Cillian1234/Unity-Project-Final");
        }
    }
}
