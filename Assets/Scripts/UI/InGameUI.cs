using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
	[SerializeField] Score score = null;
	
	private void Awake()
	{
		score.Reset();
	}
}
