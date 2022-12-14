using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using DG.Tweening;

public class VirtualCamera : MonoBehaviour
{
    CinemachineVirtualCamera virtualCamera;
    CinemachineBasicMultiChannelPerlin multiChannelPerlin;

    float amplitude, intensity;
    float duration;
    float shakingTime = 0f;
    bool fade;

    [SerializeField] TweenableImage healthScreen, damageScreen;
    // [SerializeField] float maxScreenShake = 10f;


    // Start is called before the first frame update
    void Start()
    { 
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        multiChannelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        if(shakingTime > 0)
        {
            ScreenShakeCooldown();
        }
        
    }

    public void ScreenShake(float amplitude, float intensity, float duration, bool fade = true)
    {
        if (shakingTime > 0)
        {
            if (amplitude >= this.amplitude)
            {
                ActivateShake(amplitude, intensity, duration, fade);
            }

        }else
        {
            ActivateShake(amplitude, intensity, duration, fade);
        }

    }

    void ActivateShake(float amplitude, float intensity, float duration, bool fade = true)
    {
        this.amplitude = amplitude;
        this.duration = duration;
        this.fade = fade;
        this.intensity = intensity;

        multiChannelPerlin.m_AmplitudeGain = amplitude;
        multiChannelPerlin.m_FrequencyGain = intensity;

        // multiChannelPerlin.m_AmplitudeGain = Mathf.Clamp(multiChannelPerlin.m_AmplitudeGain, 0, maxScreenShake);


        if (duration > shakingTime)
        {
            shakingTime = duration;
        }


    }

    void ScreenShakeCooldown()
    {
        shakingTime -= Time.deltaTime;

        if (fade)
        {
            multiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(amplitude, 0f, 1 - (shakingTime / duration));
            multiChannelPerlin.m_FrequencyGain = Mathf.Lerp(intensity, 0f, 1 - (shakingTime / duration));

            if (multiChannelPerlin.m_AmplitudeGain <= 0)
            {
                shakingTime = 0f;
            }

        }
        else
        {
            // multiChannelPerlin.m_AmplitudeGain -= (Time.deltaTime / shakingTime);

            if (shakingTime <= 0)
            {
                multiChannelPerlin.m_AmplitudeGain = 0f;
                multiChannelPerlin.m_FrequencyGain = 0f;
                shakingTime = 0f;

            }
        }
    }


    public void HealthScreen(float duration = 0.5f)
    {
        damageScreen.HideImage();
        healthScreen.DisplayImage(0, 0.3f, duration);
    }

    public void DamageScreen(float duration = 0.5f)
    {
        healthScreen.HideImage();
        damageScreen.DisplayImage(0, 0.3f, duration);

    }

}
