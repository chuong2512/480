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
/// This class handle behaviours of a Yeti
/// </summary>
public class Yeti : MonoBehaviour
{
    private Player player;

    [SerializeField]
    private UnityEngine.Rendering.SortingGroup sortingGroup;

    private Vector3
        startPos = Vector3.zero,
        endPos = Vector3.zero;

    private float startTimeMove = 0.0f;
    private int direction = 1;
    private bool canMove = false;

    private bool prepareToMove = true;

    private void Start()
    {
        player = Player.Instance;
    }

    private void OnEnable()
    {
        sortingGroup.sortingOrder = 1;
    }

    private void Update()
    {
        if (player.transform.position.y > transform.position.y)
        {
            sortingGroup.sortingOrder = 1;
        }
        else
        {
            sortingGroup.sortingOrder = -3;
        }

        // Check Move
        if (prepareToMove)
        {
            prepareToMove = false;
            StartCoroutine(WaitToMove());
        }

        // Move
        if (canMove)
        {
            float percentage = (Time.time - startTimeMove) / 0.5f;

            transform.position = Vector3.Lerp(startPos, endPos, percentage);
            if (transform.position.x >= 2.55f || transform.position.x <= -2.55f)
            {
                canMove = false;
                prepareToMove = true;
                return;
            }

            if (percentage >= 1.0f)
            {
                canMove = false;
                prepareToMove = true;
            }
        }
    }

    private IEnumerator WaitToMove()
    {
        yield return new WaitForSeconds(0.25f);

        startPos = transform.position;

        var random = Random.Range(0.0f, 1.0f);
        if (random >= 0.5f)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
        endPos = transform.position + new Vector3(1.0f * direction, 0.0f, 0.0f);

        startTimeMove = Time.time;
        canMove = true;
    }
}
