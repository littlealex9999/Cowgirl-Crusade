using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCameraShake : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

             VirtualCamera.instance.ScreenShake(1f, 1f, true);

            //VirtualCamera.instance.DoTweenShake();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {

            VirtualCamera.instance.HealthScreen(0.5f);
        }


    }
}
