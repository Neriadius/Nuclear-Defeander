using UnityEngine;

public class Slider_logic_LED : MonoBehaviour
{
    public GameObject[] lamps;
    private int SliderValueStored;
    public void LampUpdate(int SliderValue)
    {
        if (SliderValue != SliderValueStored)
        {
            for (int i = 0; i < lamps.Length; i++)
            {
                if (i <= (SliderValue / 5))
                {
                    Light l = lamps[i].GetComponent<Light>();
                    //l.LightOn();
                }
                else
                {
                    Light l = lamps[i].GetComponent<Light>();
                    l.LightOff();
                }

            }
        }
        SliderValueStored = SliderValue;
    }

}
