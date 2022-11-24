using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] Player player;

    public Image healthBar;
    public Image shieldBar;

    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(player.getCurrentHealth / player.GetHealthMax(), 0, 1);
        shieldBar.fillAmount = Mathf.Clamp(player.getCurrentShield / player.GetShieldMax(), 0, 1);
    }
}
