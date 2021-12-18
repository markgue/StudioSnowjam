using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AnimUI : MonoBehaviour
{
    [SerializeField] private Image img;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float mTimeperFrame = .05f;
    private float mElapsesdTime = 0f;
    private int mCurrentFrame = 0;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void SetSprite()
    {
        if(mCurrentFrame >= 0 && mCurrentFrame < sprites.Length)
        {
            img.sprite = sprites[mCurrentFrame];
        }
    }
    // Update is called once per frame
    void Update()
    {
        mElapsesdTime += Time.deltaTime;
        if (mElapsesdTime >= mTimeperFrame)
        {
            ++mCurrentFrame;
            SetSprite();
            if(mCurrentFrame >= sprites.Length)
            {
                enabled = false;
            }
            mElapsesdTime = 0;
        }
    }
}
