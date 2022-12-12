using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCameraShake : MonoBehaviour
{
    Character character;
    
    // Start is called before the first frame update
    void Start()
    {
        character = GameManager.instance.GetPlayer.gameObject.GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {

             //VirtualCamera.instance.ScreenShake(1f, 1f, true);

            

            character.TakeDamage(5);

            //VirtualCamera.instance.DoTweenShake();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {

            VirtualCamera.instance.HealthScreen(0.5f);
        }


    }
}
