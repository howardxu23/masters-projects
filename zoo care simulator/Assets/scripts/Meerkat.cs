using System.Collections;
using UnityEngine;

// THIS IS FOR MEERKAT FEEDING 2D MINIGAME ONLY
// Just in case people think this is to do with all Meerkats, it is only
// the feeding minigame that is 2D, no 3D AI or 3D meerkat info or control
// in here - Owen

public class Meerkat : MonoBehaviour
{
    [Header("Graphics")]
    [SerializeField] private Sprite meerkat;
    [SerializeField] private Sprite meerkatFed;
    [Header("GameManager")]
    [SerializeField] private GameManager gameManager;

    // The offset of the sprite to hide it.
    private Vector2 startPosition = new Vector2(0.0f, -20.0f);
    private Vector2 endPosition = Vector2.zero;
    // How long it takes to show a meerkat.
    private float showDuration = 0.5f;
    private float duration = 1.0f;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private BoxCollider2D boxCollider2D;
    private Vector2 boxOffset;
    private Vector2 boxSize;
    private Vector2 boxOffsetHidden;
    private Vector2 boxSizeHidden;

    // Want to be able to feed a meerkat     // Animal parameters
    private bool feedable = true;
    public enum AnimalType { Meerkat, Penguin };
    private AnimalType animalType;
    private float penguinRate = 0.25f;
    private float meerkatRate = 0.0f;
    private int lives;
    private int meerkatIndex = 0;

    private IEnumerator ShowHide(Vector2 start, Vector2 end)
    {
        // Make sure we start at the start.
        transform.localPosition = start;

        // Show the meerkat.
        float elapsed = 0.0f;
        while (elapsed < showDuration)
        {
            transform.localPosition = Vector2.Lerp(start, end, elapsed / showDuration);
            boxCollider2D.offset = Vector2.Lerp(boxOffsetHidden, boxOffset, elapsed / showDuration);
            boxCollider2D.size = Vector2.Lerp(boxSizeHidden, boxSize, elapsed / showDuration);
            // Update at max framerate.
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Make sure we're exactly at the end.
        transform.localPosition = end;
        boxCollider2D.offset = boxOffset;
        boxCollider2D.size = boxSize;

        // Wait for duration to pass.
        yield return new WaitForSeconds(duration);

        // Hide the meerkat.
        elapsed = 0.0f;
        while (elapsed < showDuration)
        {
            transform.localPosition = Vector2.Lerp(end, start, elapsed / showDuration);
            boxCollider2D.offset = Vector2.Lerp(boxOffset, boxOffsetHidden, elapsed / showDuration);
            boxCollider2D.size = Vector2.Lerp(boxSize, boxSizeHidden, elapsed / showDuration);
            // Update at max framerate.
            elapsed += Time.deltaTime;
            yield return null;
        }
        // Make sure we're exactly back at the start position.
        transform.localPosition = start;
        boxCollider2D.offset = boxOffsetHidden;
        boxCollider2D.size = boxSizeHidden;

        // If we got to the end and it's still hittable then we missed it.
        if (feedable)
        {
            feedable = false;
            // We only give time penalty if it isn't a sneaky penguin
            gameManager.Missed(meerkatIndex, animalType != AnimalType.Penguin);
        }
    }

    public void Hide()
    {
        // Set the appropriate meerkat parameters to hide it.
        transform.localPosition = startPosition;
        boxCollider2D.offset = boxOffsetHidden;
        boxCollider2D.size = boxSizeHidden;
    }

    private IEnumerator QuickHide()
    {
        yield return new WaitForSeconds(0.25f);
        // Whilst we were waiting we may have spawned again here, so just
        // check that hasn't happened before hiding it. This will stop it
        // flickering in that case.
        if (!feedable)
        {
            Hide();
        }
    }

    private void OnMouseDown()
    {
        if (feedable)
        {
            switch (animalType)
            {
                case AnimalType.Meerkat:
                    spriteRenderer.sprite = meerkatFed;
                    gameManager.AddScore(meerkatIndex);
                    // stop anim
                    StopAllCoroutines();
                    StartCoroutine(QuickHide());
                    feedable = false;
                    break;

                case AnimalType.Penguin:
                    gameManager.GameOver(1);
                    // If lives == 2 reduce, and change sprite.
                    /*  if (lives == 2)
                      {
                          spriteRenderer.sprite = penguinFed;
                          lives--;
                      }
                      else
                      {
                          spriteRenderer.sprite = penguinFed;
                      //    gameManager.AddScore(penguinIndex);
                          // Stop the animation
                          StopAllCoroutines();
                          StartCoroutine(QuickHide());
                          // Turn off hittable so that we can't keep tapping for score.
                          feedable = false;
                      }*/
                    break;
                default:
                    break;

            }
        }
    }
    private void CreateNext()
    {
        float random = Random.Range(0f, 1f);
        if (random < penguinRate)
        {
            // Spawn a sneaky penguin
            animalType = AnimalType.Penguin;
            // animator handles sprite
            animator.enabled = true;
        }
        else
        {
            animator.enabled = false;
            random = Random.Range(0f, 1f);
            if (random < meerkatRate)
            {
                // Spawn a hungry meerkat
                animalType = AnimalType.Meerkat;
                spriteRenderer.sprite = meerkat;
                lives = 1;
            }
        }
        // Mark as hittable so we can register an onclick event.
        feedable = true;
    }

    // As the level progresses the game gets harder.
    private void SetLevel(int level)
    {
        // As level increases increse the penguin rate to 0.25 at level 10.
        penguinRate = Mathf.Min(level * 0.25f, 0.5f);

        // Increase the amounts of Meerkats until 100% at level 40.
        meerkatRate = Mathf.Min(level * 0.025f, 1f);

        // Duration bounds get quicker as we progress. No cap on insanity.
        float durationMin = Mathf.Clamp(1 - level * 0.1f, 0.01f, 1f);
        float durationMax = Mathf.Clamp(2 - level * 0.1f, 0.01f, 2f);
        duration = Random.Range(durationMin, durationMax);
    }

    private void Awake()
    {
        // Get references to the components we'll need.
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        // Work out collider values.
        boxOffset = boxCollider2D.offset;
        boxSize = boxCollider2D.size;
        boxOffsetHidden = new Vector2(boxOffset.x, -startPosition.y / 2f);
        boxSizeHidden = new Vector2(boxSize.x, 0f);
    }

    public void Activate(int level)
    {
        SetLevel(level);
        CreateNext();
        StartCoroutine(ShowHide(startPosition, endPosition));
    }

    // Used by the game manager to uniquely identify meerkats 
    public void SetIndex(int index)
    {
        meerkatIndex = index;
    }

    // Used to freeze the game on finish.
    public void StopGame()
    {
        feedable = false;
        StopAllCoroutines();
    }

}
