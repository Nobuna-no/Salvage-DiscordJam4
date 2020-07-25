using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
	[SerializeField] private GameObject quitButton = null;
	[SerializeField] private Level levels = null;
	[SerializeField] private GameObject mainMenu = null;
	[SerializeField] private GameObject credits = null;

#if UNITY_WEBGL
	private void Awake()
	{
		quitButton.SetActive(false);
	}
#endif

	public void StartGame()
	{
		SceneManager.LoadScene(levels.GetRandomScene(), LoadSceneMode.Single);
	}

	public void ShowCredits(bool visible)
	{
		mainMenu.SetActive(!visible);
		credits.SetActive(visible);
	}

	public void Quit()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
        Application.Quit ();
#endif
	}
}
