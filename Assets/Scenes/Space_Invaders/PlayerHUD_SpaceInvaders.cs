using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD_SpaceInvaders : MonoBehaviour
{
    [Header("Elementos del HUD")]
    public TextMeshProUGUI scoreText;
    public Image[] lifeIcons;
    public Image[] bulletIcons;

    public void UpdateScore(int score)
    {
        if (scoreText != null)
        {
            // "D5" formatea el número con 5 dígitos, ej: 00150
            scoreText.text = "PTS " + score.ToString("D5");
        }
    }

    public void UpdateLives(int currentLives)
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            lifeIcons[i].enabled = (i < currentLives);
        }
    }

    public void UpdateAmmoDisplay(int bulletsAvailable)
    {
        for (int i = 0; i < bulletIcons.Length; i++)
        {
            bulletIcons[i].enabled = (i < bulletsAvailable);
        }
    }
}