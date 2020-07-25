using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
	[SerializeField] private GameObject quitButton = null;

#if UNITY_WEBGL
	private void Awake()
	{
		quitButton.SetActive(false);
	}
#endif

	public void StartGame()
	{
		// TODO: Load scene 1
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
