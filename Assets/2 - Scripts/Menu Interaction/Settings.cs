using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// TODO Save settings between scenes (Currently lost on switch)
public class Settings : MonoBehaviour
{
    public Dropdown ResolutionD;
    public Toggle FullscreenT;
    public Slider VolumeS;

    void Start()
    {
        // Initialize Resolution-Dropdown
        Resolution[] resolutions = Screen.resolutions.Distinct().ToArray();
        ResolutionD.ClearOptions();

        for (int i = 0; i < resolutions.Length; i++)
        {
            if(resolutions[i].width >= 1280)
            {
                ResolutionD.options.Add(new Dropdown.OptionData(ResToString(resolutions[i])));
            }

            ResolutionD.value = i;
        }
        
        // TODO Find correct current Resolution
        //ResolutionD.value = ResolutionD.options.indexOf(Screen.currentResolution.width + "x" + Screen.currentResolution.height);

        // Initialize Fullscreen-Toggle
        FullscreenT.isOn = Screen.fullScreen;

        // Initialize Volume-Slider
        VolumeS.value = AudioListener.volume;

        // EventListener for Resolution-Dropdown
        ResolutionD.onValueChanged.AddListener(delegate
        {
            Debug.Log("Setting Resolution to " + ResolutionD.options[ResolutionD.value]);
            Screen.SetResolution(resolutions[ResolutionD.value].width, resolutions[ResolutionD.value].height, Screen.fullScreen, resolutions[ResolutionD.value].refreshRate);	
        });

        // EventListener for Fullscreen-Checkbox
        FullscreenT.onValueChanged.AddListener(delegate
        {
            Debug.Log("Fullscreen is " + FullscreenT.isOn);
            Screen.fullScreen = FullscreenT.isOn;
        });

        // EventListener for Volume-Slider
        VolumeS.onValueChanged.AddListener(delegate
        {
            // No Debug.Log because of too many triggers
            AudioListener.volume = VolumeS.value;
        });
    }

    private string ResToString(Resolution res)
    {
        return res.width + "x" + res.height +", " + res.refreshRate + "Hz";
    }
}
