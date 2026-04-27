using UnityEngine;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Instance { get; private set; }

    [SerializeField] private GameConfig config;
    private string databaseURL;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            if (config != null)
            {
                databaseURL = config.databaseUrl;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // El resto de tu lógica de Firebase aquí usaría 'databaseURL'
}
