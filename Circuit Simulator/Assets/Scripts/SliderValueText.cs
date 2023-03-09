using UnityEngine.UI;
using UnityEngine;
using System.Net;

public class SliderValueText : MonoBehaviour
{
   public Slider slider;
   public Text textComp;

   void Start()
   {
        UpdateText(slider.value);
        slider.onValueChanged.AddListener(UpdateText);
   }
   void Update()
    {
        UpdateText(slider.value);
    }

   void UpdateText(float val)
    {
        textComp.text = slider.value.ToString();

   }
}
