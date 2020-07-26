using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VacuumWeaponUI : MonoBehaviour
{
	[SerializeField] VacuumWeapon weapon = null;
	Slider slider = null;
	Image sliderFill = null;
	[SerializeField] Color safeHeatColor = Color.black;
	[SerializeField] Color overHeatColor = Color.red;

	private void Awake()
	{
		slider = GetComponent<Slider>();
		sliderFill = slider.fillRect.GetComponent<Image>();
		sliderFill.color = safeHeatColor;
	}

	private void OnEnable()
	{
		weapon.OnFireRateChanged.AddListener(OnFireRateChanged);
		weapon.OnVacumeOverHeatMax.AddListener(OnOverHeat);
		weapon.OnVacumeOverHeatEnd.AddListener(OnSafeHeat);
	}

	private void OnDisable()
	{
		weapon.OnFireRateChanged.RemoveListener(OnFireRateChanged);
		weapon.OnVacumeOverHeatMax.RemoveListener(OnOverHeat);
		weapon.OnVacumeOverHeatEnd.RemoveListener(OnSafeHeat);
	}

	public void OnOverHeat()
	{
		sliderFill.color = overHeatColor;
	}

	public void OnSafeHeat()
	{
		sliderFill.color = safeHeatColor;
	}

	public void OnFireRateChanged(float value)
	{
		slider.value = value;
	}
}
