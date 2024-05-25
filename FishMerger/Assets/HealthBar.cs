
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

	public Slider slider;
	public Gradient gradient;
	public Image fill;

	public void SetValues(float minVal, float maxVal)
	{
		slider.maxValue = maxVal;
		slider.minValue = minVal;
		fill.color = gradient.Evaluate(1f);
		slider.value = minVal;
	}

    public void SetForce(float force)
	{
		slider.value = force;

		fill.color = gradient.Evaluate(slider.normalizedValue);
	}

}
