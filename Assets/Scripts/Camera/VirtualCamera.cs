using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using DG.Tweening;

public class VirtualCamera : MonoBehaviour
{
    static public VirtualCamera instance { get; private set; }
    CinemachineVirtualCamera virtualCamera;
    CinemachineBasicMultiChannelPerlin multiChannelPerlin;

    float shakingTime = 0f;
    bool shaking = false;
    bool fadeShake = true;

    [SerializeField] ShowImage healthScreen, damageScreen;
    [SerializeField] float maxScreenShake = 10f;


    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        multiChannelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        if(shaking)
        {
            if (fadeShake)
            {
                multiChannelPerlin.m_AmplitudeGain -= (Time.deltaTime / shakingTime);

                if(multiChannelPerlin.m_AmplitudeGain <= 0)
                {
                    shaking = false;
                }

            }
            else
            {
                shakingTime -= Time.deltaTime;

                if (shakingTime <= 0)
                {
                    StopShaking();
                }
            }
            
        }
        
    }

    public void ScreenShake(float amplitude, float duration, bool damageFeedback = false, bool fade = true)
    {
        multiChannelPerlin.m_AmplitudeGain += amplitude;
        multiChannelPerlin.m_AmplitudeGain = Mathf.Clamp(multiChannelPerlin.m_AmplitudeGain, 0, maxScreenShake);
        
        if(duration > shakingTime)
        {
            shakingTime = duration;
        }

        if (damageFeedback)
        {
            healthScreen.HideImage();
            damageScreen.DisplayImage(0, 0.1f, duration);
        }

        shaking = true;

        fadeShake = fade;
    }


    public void DoTweenShake()
    {
        //transform.DOShakePosition();
    }

    public void HealthScreen(float duration)
    {
        damageScreen.HideImage();
        healthScreen.DisplayImage(0, 0.1f, duration);
    }

    void StopShaking()
    {
        shaking = false;
        multiChannelPerlin.m_AmplitudeGain = 0f;
        
    }

}
