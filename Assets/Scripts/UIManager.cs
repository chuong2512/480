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

/// <summary>
/// This class manage UI and events in game
/// </summary>
public class UIManager : Singleton<UIManager>
{
    private GameController gameController;
    private Player player;

    [SerializeField]
    private GameObject
        pauseBtn,
        headBtn,
        faceBtn,
        paused,
        gameOver;

    [Space(10)]
    [SerializeField]
    private Image soundBtn;
    [SerializeField]
    private Sprite
        soundOn,
        soundOff;

    [SerializeField]
    private Text starTxt;

    [Space(10)]
    [SerializeField]
    private Sprite[] items;

    [Space(10)]
    [SerializeField]
    private GameObject firstGiftBtn;
    [SerializeField]
    private Image firstGift;
    [SerializeField]
    private GameObject firstGiftNotify;

    [Space]
    [SerializeField]
    private GameObject secondGiftBtn;
    [SerializeField]
    private Image secondGift;
    [SerializeField]
    private GameObject secondGiftNotify;

    [Space]
    [SerializeField]
    private GameObject thirdGiftBtn;
    [SerializeField]
    private Image thirdGift;
    [SerializeField]
    private GameObject thirdGiftNotify;

    private List<int> unlockedHeadItemsIndex = null;
    private int unlockedHeadItemsArrayLastIndex = 0;

    private List<int> unlockedFaceItemsIndex = null;
    private int unlockedFaceItemsArrayLastIndex = 0;

    [Space(10)]
    public AudioSource sound = null;
    public AudioSource star = null;
	public AudioSource themeMusic = null;

    private void Start()
    {
        gameController = GameController.Instance;
        player = Player.Instance;

        unlockedHeadItemsIndex = new List<int>();
        unlockedFaceItemsIndex = new List<int>();

        CheckSound();
        CheckHeadAndFaceItems();
        CheckLastHeadAndFaceItems();
    }

    private void CheckSound()
    {
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

    private void CheckHeadAndFaceItems()
    {
        // Check head items: 0 - 9 is the head items is set on editor
        for (var i = 9; i >= 0; i--)
        {
            var key = "item_" + i;
            if (PlayerPrefs.GetInt(key, 0) == 1)
            {
                if (!headBtn.activeInHierarchy)
                    headBtn.SetActive(true);

                unlockedHeadItemsIndex.Add(i);
            }
        }
        unlockedHeadItemsIndex.Add(-1); // -1 is a special value, it's mean don't use head items

        // Check face items: begin from 10 is the face items is set on editor
        for (var i = items.Length - 1; i >= 10; i--)
        {
            var key = "item_" + i;
            if (PlayerPrefs.GetInt(key, 0) == 1)
            {
                if (!faceBtn.activeInHierarchy)
                    faceBtn.SetActive(true);

                unlockedFaceItemsIndex.Add(i);
            }
        }
        unlockedFaceItemsIndex.Add(-1); // -1 is a special value, it's mean don't use face items
    }

    private void CheckLastHeadAndFaceItems()
    {
        var headIndex = PlayerPrefs.GetInt(Constants.LAST_HEAD_ITEMS_INDEX, -1);
        // If we are using a head item
        if (headIndex != -1)
        {
            // Active last head item
            player.ActiveItems(headIndex);

            // Get this head item index, we saved at the last time
            unlockedHeadItemsArrayLastIndex = unlockedHeadItemsIndex.FindIndex(x => x == headIndex) + 1;
            if (unlockedHeadItemsArrayLastIndex >= unlockedHeadItemsIndex.Count)
            {
                unlockedHeadItemsArrayLastIndex = 0;
            }
        }

        var faceIndex = PlayerPrefs.GetInt(Constants.LAST_FACE_ITEMS_INDEX, -1);
        // If we are using a face item
        if (faceIndex != -1)
        {
            // Active last face item
            player.ActiveItems(faceIndex);

            // Get this face item index, we saved at the last time
            unlockedFaceItemsArrayLastIndex = unlockedFaceItemsIndex.FindIndex(x => x == faceIndex) + 1;
            if (unlockedFaceItemsArrayLastIndex >= unlockedFaceItemsIndex.Count)
            {
                unlockedFaceItemsArrayLastIndex = 0;
            }
        }
    }

    public void GameIsStarted()
    {
        pauseBtn.SetActive(true);
        headBtn.SetActive(false);
        faceBtn.SetActive(false);
    }

    public void UpdateStar(int starAmount)
    {
        starTxt.text = starAmount + "";
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        gameController.CheckGift();
		AdsControl.Instance.showAds ();
    }

    public void HeadItemsBtn_Onclick()
    {
        if (unlockedHeadItemsIndex[unlockedHeadItemsArrayLastIndex] != -1)
        {
            // Save last head item index
            PlayerPrefs.SetInt(Constants.LAST_HEAD_ITEMS_INDEX, unlockedHeadItemsIndex[unlockedHeadItemsArrayLastIndex]);
            player.ActiveItems(unlockedHeadItemsIndex[unlockedHeadItemsArrayLastIndex]);

            unlockedHeadItemsArrayLastIndex++;
            if (unlockedHeadItemsArrayLastIndex >= unlockedHeadItemsIndex.Count)
            {
                unlockedHeadItemsArrayLastIndex = 0;
            }
        }
        else
        {
            // Save last head item index
            PlayerPrefs.SetInt(Constants.LAST_HEAD_ITEMS_INDEX, -1);
            player.InactiveItems(0);
            unlockedHeadItemsArrayLastIndex = 0;
        }

        if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
        {
            sound.Play();
        }
    }

    public void FaceItemsBtn_Onclick()
    {
        if (unlockedFaceItemsIndex[unlockedFaceItemsArrayLastIndex] != -1)
        {
            // Save last face item index
            PlayerPrefs.SetInt(Constants.LAST_FACE_ITEMS_INDEX, unlockedFaceItemsIndex[unlockedFaceItemsArrayLastIndex]);
            player.ActiveItems(unlockedFaceItemsIndex[unlockedFaceItemsArrayLastIndex]);

            unlockedFaceItemsArrayLastIndex++;
            if (unlockedFaceItemsArrayLastIndex >= unlockedFaceItemsIndex.Count)
            {
                unlockedFaceItemsArrayLastIndex = 0;
            }
        }
        else
        {
            // Save last head item index
            PlayerPrefs.SetInt(Constants.LAST_FACE_ITEMS_INDEX, -1);
            player.InactiveItems(1);
            unlockedFaceItemsArrayLastIndex = 0;
        }

        if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
        {
            sound.Play();
        }
    }

    public void PauseBtn_Onclick()
    {
        Time.timeScale = 0.0f;
        paused.SetActive(true);
		AdsControl.Instance.showAds ();

        if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
        {
            sound.Play();
        }
    }

    public void ResumeBtn_Onclick()
    {
        paused.SetActive(false);
        Time.timeScale = 1.0f;

        if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
        {
            sound.Play();
        }
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

    public void ActiveGiftBtn(int total)
    {
        switch (total)
        {
            case 1:
                firstGiftBtn.SetActive(true);
                break;
            case 2:
                firstGiftBtn.SetActive(true);
                secondGiftBtn.SetActive(true);
                break;
            case 3:
                firstGiftBtn.SetActive(true);
                secondGiftBtn.SetActive(true);
                thirdGiftBtn.SetActive(true);
                break;
        }
    }

    public void OpenGift(int giftIndex)
    {
        var total = items.Length;
        var random = Random.Range(0, total);
        var key = "item_" + random;

        // Show gift and disappear gift button
        switch (giftIndex)
        {
            case 0:
                firstGift.sprite = items[random];
                firstGift.SetNativeSize();
                firstGift.gameObject.SetActive(true);
                firstGiftBtn.SetActive(false);
                break;
            case 1:
                secondGift.sprite = items[random];
                secondGift.SetNativeSize();
                secondGift.gameObject.SetActive(true);
                secondGiftBtn.SetActive(false);
                break;
            case 2:
                thirdGift.sprite = items[random];
                thirdGift.SetNativeSize();
                thirdGift.gameObject.SetActive(true);
                thirdGiftBtn.SetActive(false);
                break;
        }

        // Check state of item: unlock - 1 or not - 0
        if (PlayerPrefs.GetInt(key, 0) == 0)
        {
            if (giftIndex == 0)
            {
                firstGiftNotify.SetActive(true);
            }
            else if (giftIndex == 1)
            {
                secondGiftNotify.SetActive(true);
            }
            else if (giftIndex == 2)
            {
                thirdGiftNotify.SetActive(true);
            }

            // Item is unlocled from now
            PlayerPrefs.SetInt(key, 1);
        }
    }

    public void PlayAgainOrBackHomeBtn_Onclick(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
