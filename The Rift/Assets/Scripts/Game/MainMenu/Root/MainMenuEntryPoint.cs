using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private GameObject _sceneRootBinder;

    public void Run()
    {
        Debug.Log("MainMenu scene loaded");
    }
}
