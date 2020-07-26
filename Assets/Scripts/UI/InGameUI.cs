using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
	[SerializeField] int endSceneIndex = 2;
	[SerializeField] Score score = null;
	[SerializeField] BlackFade fade = null;
	[SerializeField] TextMeshProUGUI introText = null;
	[SerializeField] string[] introTexts = null;
	[SerializeField] float timePerLine = 1.0f;
	[SerializeField] GameObject PauseMenu;

    private void Awake()
	{
		score.Reset();
		StartCoroutine(Intro(PlayerData.FirstGame));
		PlayerData.SetFirstGame();
	}

    private void Update()
    {
        if(Input.GetAxis("Pause") == 1)
        {
            PauseMenu.SetActive(true);
        }
        else
        {
            PauseMenu.SetActive(false);
        }
    }

    public void EndGame()
	{
		StartCoroutine(Outro());
	}

	private IEnumerator Intro(bool isFirstTime)
	{
		if (isFirstTime)
		{
			Time.timeScale = 0.0f;
			for (int i = 0; i < introTexts.Length; ++i)
			{
				introText.text = introTexts[i];
				yield return new WaitForSecondsRealtime(timePerLine);
			}

			//Activate controls and other audio
			Time.timeScale = 1.0f;
		}
		else
		{
			introText.gameObject.SetActive(false);
			yield return null;
		}

		yield return fade.StartFading(false);

		if (isFirstTime)
			introText.gameObject.SetActive(false);
	}

	private IEnumerator Outro()
	{
		yield return fade.StartFading(true);
		SceneManager.LoadScene(endSceneIndex, LoadSceneMode.Single);
	}
}
