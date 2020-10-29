using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{    
    private Button _restartButton;
    // Start is called before the first frame update
    void Start()
    {
        _restartButton = this.gameObject.GetComponent<Button>();
        _restartButton.onClick.AddListener(Restart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Restart(){
        SceneManager.LoadScene("MainScene");
    }
}
