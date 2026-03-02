/*
 * Description
 * ----------------------------------------------------
 * This script sets stars earned by player to ui panel.
 */

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StarsSetter : MonoBehaviour
{
    private const float TARGET_STAR_SIZE = 1.7f; // target star game object size

    [SerializeField] [Tooltip("Star object sprite.")] private Sprite star;
    [SerializeField] [Tooltip("3 stars particles effect.")] private ParticleSystem[] starParticles;
    [SerializeField] [Tooltip("Particle system that activates when full number of stars is reached")] private ParticleSystem fullStarsCountParticles;
    [SerializeField] [Tooltip("Star sound effect source.")] private AudioSource starSoundEffectSource;

    /// <summary>
    /// Sets players stars in ui panel.
    /// </summary>
    /// <param name="count">Stars count.</param>
    public void SetStars(int count)
    {
        // check if stars count more 0
        if (count > 0)
        {
            Transform parent = gameObject.transform;
            GameObject starNormal = ConfigureStarObject();

            float delay = 0.1f; // start delay
            for (int i = 0; i < count; i++)
            {
                SpawnStarToParent(starNormal, parent, delay, i + 1);
                delay += .1f; // increase delay
            }

            if (count == 3)
                StartCoroutine(CelebrateAllCollectedStars(1f));
        }
    }

    /// <summary>
    /// Configures star game object for ui.
    /// </summary>
    /// <returns>Configured star object.</returns>
    private GameObject ConfigureStarObject()
    {
        GameObject star = new GameObject(); // new game object
        star.transform.localScale = new Vector2(0f, 0f); // initial size
        Image image = star.AddComponent<Image>(); // add image component
        image.sprite = this.star; // set star sprite
        
        return star;
    }

    /// <summary>
    /// Spawns star game object to score panel.
    /// </summary>
    /// <param name="star">Star object.</param>
    /// <param name="parent">Parent object.</param>
    /// <param name="animDelay">Animation delay.</param>
    /// <param name="starNum">Star object number.</param>
    private void SpawnStarToParent(GameObject star, Transform parent, float animDelay, int starNum)
    {
        GameObject currentStar = Instantiate(star); // spawn star
        currentStar.transform.SetParent(parent); // set star to parent
        StartCoroutine(AnimateStar(animDelay, currentStar, starNum));
    }

    /// <summary>
    /// Activates particle system if 3 stars collected.
    /// </summary>
    /// <param name="delay">Start delay.</param>
    private IEnumerator CelebrateAllCollectedStars(float delay)
    {
        yield return new WaitForSeconds(delay);
        Handheld.Vibrate(); // vibrate if all stars
        fullStarsCountParticles.Play(); // activate particles system
    }

    /// <summary>
    /// Animates star game object.
    /// </summary>
    /// <param name="delay">Animation delay.</param>
    /// <param name="star">Star object.</param>
    /// <param name="starNum">Star object number.</param>
    private IEnumerator AnimateStar(float delay, GameObject star, int starNum)
    {
        yield return new WaitForSeconds(delay); // delay
        Transform starTransform = star.transform; // get star transform

        starParticles[starNum - 1].Play();
        starSoundEffectSource.Play();

        for (float i = 0f; i < TARGET_STAR_SIZE; i += .1f)
        {
            starTransform.localScale = new Vector2(i, i); // edit size
            yield return new WaitForSeconds(0.02f); // delay
        }
    }
}