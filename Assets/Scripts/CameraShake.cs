using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraShake : MonoBehaviour
{
    CinemachineVirtualCamera virtualCamera;
    CinemachineBasicMultiChannelPerlin multiChannelPerlin;

    float shakingTime = 0f;
    bool shaking = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        multiChannelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        if(shaking)
        {
            shakingTime -= Time.deltaTime;

            if (shakingTime <= 0)
            {
                StopShaking();
            }

        }
        
    }

    public void Shake(float amplitude, float duration)
    {
        multiChannelPerlin.m_AmplitudeGain = amplitude;
        
        if(duration > shakingTime)
        {
            shakingTime = duration;
        }
        
        shaking = true;
    }

    void StopShaking()
    {
        shaking = false;
        multiChannelPerlin.m_AmplitudeGain = 0f;
        
    }

}
