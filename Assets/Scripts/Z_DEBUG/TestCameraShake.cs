using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCameraShake : MonoBehaviour
{
    Character player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.GetPlayer.gameObject.GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {

            //VirtualCamera.instance.ScreenShake(1f, 1f, true);

            player.TakeDamage(10f);

            //VirtualCamera.instance.DoTweenShake();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
           // player.GiveHealth(10f);

        }


    }
}
