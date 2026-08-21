using System.IO;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float speed;
}

[System.Serializable]
public class PulpitData
{
    public float min_pulpit_destroy_time;
    public float max_pulpit_destroy_time;
    public float pulpit_spawn_time;
}

[System.Serializable]
public class ConfigData
{
    public PlayerData player_data;
    public PulpitData pulpit_data;
}

public class gameManager : MonoBehaviour
{
    // Instance for easy access (optional)
    public static gameManager Instance { get; private set; }
    public static ConfigData Config { get; private set; }
    public TextAsset doofusDiaryAsset;

    // Parsed config stored here for non-static access
    public ConfigData config;

    void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadConfig();
        if (config != null)
        {
            Config = config; // expose statically
            Debug.Log($"Player speed: {config.player_data.speed}");
            Debug.Log($"Pulpit spawn time: {config.pulpit_data.pulpit_spawn_time}");
        }
    }

    void LoadConfig()
    {
        string text = null;

        if (doofusDiaryAsset != null)
        {
            text = doofusDiaryAsset.text;
        }
        else
        {
            // Try to read from Assets root at runtime/editor
            var path = Path.Combine(Application.dataPath, "doofus_diary.json");
            if (File.Exists(path))
            {
                try { text = File.ReadAllText(path); }
                catch (System.Exception ex) { Debug.LogError($"Error reading {path}: {ex}"); }
            }

            // Fallback: Resources folder (if you move the file to Assets/Resources)
            if (string.IsNullOrEmpty(text))
            {
                var txt = Resources.Load<TextAsset>("doofus_diary");
                if (txt != null) text = txt.text;
            }
        }

        if (string.IsNullOrEmpty(text))
        {
            Debug.LogError("doofus_diary.json not found");
            return;
        }

        try
        {
            config = JsonUtility.FromJson<ConfigData>(text);
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to parse doofus_diary.json: {ex}");
        }
    }

    // Public static helper accessors
    public static float GetPlayerSpeed()
    {
        if (Config?.player_data == null)
        {
            Debug.LogWarning("gameManager: Config or player_data is null when requesting PlayerSpeed");
            return 0f;
        }
        return Config.player_data.speed;
    }

    public static float GetPulpitSpawnTime()
    {
        if (Config?.pulpit_data == null)
        {
            Debug.LogWarning("gameManager: Config or pulpit_data is null when requesting PulpitSpawnTime");
            return 0f;
        }
        return Config.pulpit_data.pulpit_spawn_time;
    }

    public static float GetMinPulpitDestroyTime()
    {
        if (Config?.pulpit_data == null)
        {
            Debug.LogWarning("gameManager: Config or pulpit_data is null when requesting MinPulpitDestroyTime");
            return 0f;
        }
        return Config.pulpit_data.min_pulpit_destroy_time;
    }

    public static float GetMaxPulpitDestroyTime()
    {
        if (Config?.pulpit_data == null)
        {
            Debug.LogWarning("gameManager: Config or pulpit_data is null when requesting MaxPulpitDestroyTime");
            return 0f;
        }
        return Config.pulpit_data.max_pulpit_destroy_time;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
