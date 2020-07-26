using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
	[SerializeField] int endSceneIndex = 2;
	[SerializeField] Score score = null;
	[SerializeField] BlackFade fade = null;
	
	private void Awake()
	{
		score.Reset();
	}

	public void EndGame()
	{
		StartCoroutine(Outro());
	}

	private IEnumerator Intro()
	{
		// TODO: Cinematic if first time playing
		yield return fade.StartFading(false);
	}

	private IEnumerator Outro()
	{
		yield return fade.StartFading(true);
		SceneManager.LoadScene(endSceneIndex, LoadSceneMode.Single);
	}
}
