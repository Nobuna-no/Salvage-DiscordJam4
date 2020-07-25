using UnityEngine;

[CreateAssetMenu(fileName = "Score", menuName = "Score")]
public class Score : ScriptableObject
{
	[SerializeField] private int value = 0;

	public int Value => value;

	public void Increment()
	{
		++value;
	}

	public void Reset()
	{
		value = 0;
	}
}
