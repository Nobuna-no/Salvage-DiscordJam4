using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BlackFade : MonoBehaviour
{
	[SerializeField] private bool startBlack = true;
	[SerializeField] private float speed = 1.0f;
	private Image image = null;

	public UnityEvent OnFadeOver = new UnityEvent();

	private float Alpha
	{
		get => image.color.a;
		set
		{
			Color c = image.color;
			c.a = value;
			image.color = c;
		}
	}

	private void Awake()
	{
		image = GetComponent<Image>();
		Alpha = startBlack ? 1.0f : 0.0f;
	}

	public Coroutine StartFading(bool fadeToBlack)
	{
		StopAllCoroutines();
		return StartCoroutine(fadeToBlack ? FadeIn() : FadeOut());
	}

	private IEnumerator FadeIn()
	{
		while (Alpha < 1.0f)
		{
			Alpha = Mathf.Min(1.0f, Alpha + speed * Time.deltaTime);
			yield return null;
		}
		OnFadeOver.Invoke();
	}

	private IEnumerator FadeOut()
	{
		while (Alpha > 0.0f)
		{
			Alpha = Mathf.Max(0.0f, Alpha - speed * Time.deltaTime);
			yield return null;
		}
		OnFadeOver.Invoke();
	}
}
