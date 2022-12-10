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
        if (healthBar != null) {
            if (player != null) {
                healthBar.fillAmount = Mathf.Clamp(player.getCurrentHealth / player.GetHealthMax(), 0, 1);
            } else {
                healthBar.fillAmount = 0;
            }
        }

        if (shieldBar != null) {
            if (player != null) {
                shieldBar.fillAmount = Mathf.Clamp(player.getCurrentShield / player.GetShieldMax(), 0, 1);
            } else {
                shieldBar.fillAmount = 0;
            }
        }
    }
}
