using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadManager : MonoBehaviour
{
    public Image objProcessBar;
    public Text objProcessText;
    public Button login_button;
    private WaitForEndOfFrame waitForEnd;


    public void  LoadScenePro()
    {
        //this.StartIE("1", StartLoading("1"));
    }
    private IEnumerator StartLoading(string sceneName)
    {
        float i = 0;
        AsyncOperation acOp = SceneManager.LoadSceneAsync(sceneName);
        acOp.allowSceneActivation = false;
        login_button.enabled = false;
        if (acOp != null)
        {

            if (!acOp.isDone)
            {
                objProcessText.text = "正在进入教室";
                while (i < 100)
                {
                    i++;
                    print(i);
                    objProcessBar.fillAmount = i / 100;
                    objProcessText.text = i.ToString() + "%";
                    yield return waitForEnd;
                }
                acOp.allowSceneActivation = true;
            }
        }
    }
}
