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
/// This class handle behaviours of player
/// </summary>
public class Player : Singleton<Player>
{
    private GameController gameController = null;
    private UIManager uiManager = null;

    [SerializeField]
    private GameObject[] items = null;

    // Cache gameObject.transform
    [Space(10)]
    [SerializeField]
    private Transform thisTransform = null;
    // Transform of child contain Sprite of main character
    [SerializeField]
    private Transform spriteTransform = null;
    [SerializeField]
    private float speed = 1.0f;
    private int direction = -1;
    private float zAngleValue = 30.0f;

    private Animator spriteAnim = null;

    [Space]
    [SerializeField]
    private GameObject explosion = null;
    private bool isDead = false;

    private GameObject
        lastHeadItem = null,
        lastFaceItem = null;

    // Cache main camera in scene
    private Transform mainCamera = null;
    private float mainCameraOffset = 0.0f;
    private float mainCameraHalfPastHeight = 0.0f;

    private bool
        readyToGo = false,
        handDown = false,
        clickDone = false;

    private void Start()
    {
        gameController = GameController.Instance;
        uiManager = UIManager.Instance;
        mainCamera = Camera.main.transform;
        mainCameraOffset = thisTransform.position.y - mainCamera.position.y;
        mainCameraHalfPastHeight = mainCamera.GetComponent<Camera>().orthographicSize;

        spriteAnim = spriteTransform.GetComponent<Animator>();
    }

    // Active head items or face items
    public void ActiveItems(int index)
    {
        if (0 <= index && index <= 9)
        {
            if (lastHeadItem != null)
                lastHeadItem.SetActive(false);

            items[index].SetActive(true);
            lastHeadItem = items[index];
        }
        else if (10 <= index)
        {
            if (lastFaceItem != null)
                lastFaceItem.SetActive(false);

            items[index].SetActive(true);
            lastFaceItem = items[index];
        }
    }

    /// <summary>
    /// Inactive items of player
    /// </summary>
    /// <param name="code">
    /// code value = 0 mean head items
    /// code value = 1 mean face items
    /// </param>
    public void InactiveItems(int code)
    {
        if (code == 0)
        {
            lastHeadItem.SetActive(false);
            lastHeadItem = null;
        }
        else if (code == 1)
        {
            lastFaceItem.SetActive(false);
            lastFaceItem = null;
        }
    }

    private void Update()
    {
        if (!isDead)
        {
            if (readyToGo)
            {
                // Control movement of character
                HandleMovement();
            }
        }
    }

    private void HandleMovement()
    {
        if (handDown)
        {
            handDown = false;
            clickDone = true;
            // Change direction of movement
            if (zAngleValue == 30.0f)
            {
                zAngleValue = -30.0f;
            }
            else
            {
                zAngleValue = 30.0f;
            }

            thisTransform.rotation = Quaternion.Euler(0.0f, 0.0f, zAngleValue);
            spriteTransform.localScale = new Vector3(
                                                    spriteTransform.localScale.x * -1.0f,
                                                    spriteTransform.localScale.y,
                                                    spriteTransform.localScale.z);
            spriteTransform.localRotation = Quaternion.Euler(0.0f, 0.0f, -zAngleValue);
            direction = -direction;
        }
        thisTransform.position += transform.right * Time.deltaTime * speed * direction;
    }

    // Event Trigger
    public void HandDown()
    {
        if (!readyToGo)
        {
            // Start game
            readyToGo = true;
            spriteAnim.enabled = true;
            uiManager.GameIsStarted();
        }

        if (!clickDone)
            handDown = true;
    }

    // Event Trigger
    public void HandUp()
    {
        clickDone = false;
    }

    private void LateUpdate()
    {
        // Make main camera follow character
        mainCamera.position = new Vector3(mainCamera.position.x, thisTransform.position.y - mainCameraOffset, mainCamera.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If detect check point then notify spawn new stage
        if (collision.tag == Constants.tag_1)
        {
            gameController.SpawnStage(new Vector3(0.0f, mainCamera.position.y - mainCameraHalfPastHeight, 0.0f));
        }

        // If detect stars
        if (collision.tag == Constants.tag_3)
        {
            gameController.UpdateStar();
            collision.gameObject.SetActive(false);

            if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
            {
                uiManager.star.Play();
            }
        }

        // Die
        if (collision.tag == Constants.tag_2)
        {
            isDead = true;
            spriteTransform.gameObject.SetActive(false);
            explosion.SetActive(true);
        }
    }
}
