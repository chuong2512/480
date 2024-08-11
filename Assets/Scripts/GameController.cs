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

/// <summary>
/// This class handle game logic. This also can become a mediate object to hold
/// components between other objects.
/// </summary>
public class GameController : Singleton<GameController>
{
    private UIManager uiManager;

    // Stages is created in scene
    [SerializeField]
    private GameObject[] stages;

    private int totalStars = 0;

    private void Start()
    {
        uiManager = UIManager.Instance;

        totalStars = 0;

        // Spawn random a stage
        stages[Random.Range(0, stages.Length)].SetActive(true);
    }

    /// <summary>
    /// Spawn a new stage.
    /// </summary>
    /// <param name="position">
    /// position to spawn.
    /// </param>
    public void SpawnStage(Vector3 position)
    {
        while (true)
        {
            int random = Random.Range(0, stages.Length);
            if (!stages[random].activeInHierarchy)
            {
                stages[random].transform.position = position;
                stages[random].SetActive(true);
                break;
            }
        }
    }

    public void UpdateStar()
    {
        totalStars++;
        uiManager.UpdateStar(totalStars);
    }

    public void CheckGift()
    {
        if (10 <= totalStars && totalStars < 25)
        {
            uiManager.ActiveGiftBtn(1);
        }
        else if (25 <= totalStars && totalStars < 50)
        {
            uiManager.ActiveGiftBtn(2);
        }
        else if (50 <= totalStars)
        {
            uiManager.ActiveGiftBtn(3);
        }
    }
}
