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
/// This class control objects contain SpriteRenderer component
/// belong to a stage in scene.
/// </summary>
public class VisibleObj : MonoBehaviour
{
    [SerializeField]
    private bool belongToStage = true;

    private Stage stage = null;

    /// <summary>
    /// Get "stage" that this visible object belong
    /// </summary>
    /// <param name="stage"></param>
    public void SetStage(Stage stage)
    {
        this.stage = stage;
    }

    private void OnBecameInvisible()
    {
        if (belongToStage)
        {
            // Notify to "stage"
            stage.VisibleObjectDisappear();
        }

        // Use "if" to fix weired error when run on editor
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }
}
