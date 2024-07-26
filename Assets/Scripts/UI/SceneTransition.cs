using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private RectTransform startTransition;
    [SerializeField] private RectTransform endTransition;
    [SerializeField] private float slideTime = 1f;

    private void Awake()
    {
        StartSceneTransition();

        endTransition.gameObject.SetActive(false);
    }

    public void StartSceneTransition()
    {
        startTransition.gameObject.SetActive(true);
        startTransition.anchoredPosition = Vector2.zero;
        Time.timeScale = 1f;
        startTransition.DOAnchorPos(new Vector2(3000, 0f), 0.7f).SetUpdate(false).OnComplete(StartScene).SetLink(gameObject);
    }

    public void EndSceneTransition(string scene)
    {
        endTransition.gameObject.SetActive(true);
        endTransition.anchoredPosition = new Vector2(-3000f, 0f);
        endTransition.DOAnchorPos(new Vector2(0f, 0f), slideTime).SetDelay(0.1f).SetUpdate(true).OnComplete(() => LoadScene(scene)).SetLink(gameObject);
    }

    private void StartScene() =>
        startTransition.gameObject.SetActive(false);

    private void LoadScene(string scene) =>
        SceneManager.LoadScene(scene);

}
