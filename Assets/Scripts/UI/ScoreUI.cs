using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreUI : MonoBehaviour
{
	[SerializeField] private Score score = null;
	private TextMeshProUGUI text = null;

	private void Awake()
	{
		text = GetComponent<TextMeshProUGUI>();
	}

	private void OnEnable()
	{
		score.OnValueChanged += OnScoreChanged;
	}

	private void OnDisable()
	{
		score.OnValueChanged -= OnScoreChanged;
	}

	private void Start()
	{
		text.text = score.Value.ToString();
	}

	private void OnScoreChanged(int value)
	{
		text.text = value.ToString();
	}
}
