using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieDisplayBar : MonoBehaviour
{
    public Slider slider;

    public Gradient gradient;

    public Image fill;

    public void SetValue(float value)
    {
        slider.value = value;

    }
    
    public void SetMaxValue(float value)
    {
        slider.maxValue = value;
        slider.value = value;

    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
