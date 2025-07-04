using UnityEngine;

public static class SettingsManager_SpaceInvaders
{
    private const string SoundKey = "Settings_Sound";
    public static bool IsSoundOn { get; set; }

    // El constructor estático carga los ajustes guardados al iniciar el juego.
    static SettingsManager_SpaceInvaders()
    {
        // 1 para ON, 0 para OFF. Por defecto, está en ON.
        IsSoundOn = PlayerPrefs.GetInt(SoundKey, 1) == 1;
    }

    public static void Save()
    {
        PlayerPrefs.SetInt(SoundKey, IsSoundOn ? 1 : 0);
        PlayerPrefs.Save();
    }
}