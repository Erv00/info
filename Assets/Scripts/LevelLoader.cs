using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

	public void Load(int x)
    {
        SceneManager.LoadScene(x);
    }
}
