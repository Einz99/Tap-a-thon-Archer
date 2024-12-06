using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SliderSetUp : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _textSlider;
    void Start()
    {
        _slider.onValueChanged.AddListener((v) => {
            _textSlider.text = (v * 100).ToString("0");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
