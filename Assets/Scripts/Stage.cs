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
/// This class is used to control behaviours of objects "stage".
/// </summary>
public class Stage : MonoBehaviour
{
    private SpriteRenderer[] visibleObjects = null;

    private int objectsDisappear = 0;

    private void OnEnable()
    {
        if (visibleObjects != null)
        {
            // Active visible objects again after these inactive in scene
            var length = visibleObjects.Length;
            for (var i = length - 1; i >= 0; i--)
            {
                visibleObjects[i].gameObject.SetActive(true);
            }
        }
    }

    private void Start()
    {
        SetUpForAllVisibleObjects();
    }

    private void SetUpForAllVisibleObjects()
    {
        visibleObjects = transform.GetComponentsInChildren<SpriteRenderer>(true);

        // Add required script to these objects
        var length = visibleObjects.Length;
        for (var i = length - 1; i >= 0; i--)
        {
            visibleObjects[i].gameObject.AddComponent<VisibleObj>();
            visibleObjects[i].GetComponent<VisibleObj>().SetStage(this);
        }
    }

    /// <summary>
    /// Get notification if a visible object just inactive in scene
    /// </summary>
    public void VisibleObjectDisappear()
    {
        objectsDisappear++;

        // If all visible objects inactive in scene then we'll inactive this "stage"
        if (objectsDisappear == visibleObjects.Length)
        {
            objectsDisappear = 0;
            gameObject.SetActive(false);
        }
    }
}
