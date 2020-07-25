using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenuUI : MonoBehaviour
{
	[SerializeField] private Score score = null;
	[SerializeField] private TextMeshProUGUI text = null;
	[SerializeField] private int mainMenuSceneIndex = 0;
	[SerializeField] private int gameSceneIndex = 1;

	public void Awake()
	{
		text.SetText(text.text, score.Value);
	}

	public void StartGame()
	{
		SceneManager.LoadScene(gameSceneIndex, LoadSceneMode.Single);
	}

	public void Back()
	{
		SceneManager.LoadScene(mainMenuSceneIndex, LoadSceneMode.Single);
	}
}
