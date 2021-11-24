using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    private Canvas canvas;
    [SerializeField]
    private Button buttonBack;
    [SerializeField]
    private Button buttonQuit;

    private bool paused
    {
        get => canvas.enabled;
        set => canvas.enabled = value;
    }

    private void Start()
    {
        canvas = GetComponent<Canvas>();

        buttonBack.onClick.AddListener(() => paused = false);
        buttonQuit.onClick.AddListener(() => Quit());

        paused = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
        }

        if (paused)
        {
            Time.timeScale = 0;
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            Time.timeScale = 5;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
