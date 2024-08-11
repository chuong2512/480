/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeScript : MonoBehaviour
{
    [SerializeField]
    private Image soundBtn = null;
    [SerializeField]
    private Sprite
        soundOn = null,
        soundOff = null;

    [SerializeField]
	private AudioSource sound, themeMusic;

    private void Start()
    {
        Time.timeScale = 1.0f;

        if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
        {
            soundBtn.sprite = soundOn;
			themeMusic.Play ();
        }
        else
        {
            soundBtn.sprite = soundOff;
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OnOffSound()
    {
        if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
        {
            PlayerPrefs.SetInt(Constants.SOUND, 0);
            soundBtn.sprite = soundOff;
			themeMusic.Pause ();
        }
        else
        {
            PlayerPrefs.SetInt(Constants.SOUND, 1);
            soundBtn.sprite = soundOn;
            sound.Play();
			themeMusic.Play ();
        }
    }
}