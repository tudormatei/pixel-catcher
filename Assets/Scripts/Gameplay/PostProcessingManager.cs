using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

namespace Scripts.Gameplay
{
    public class PostProcessingManager : MonoBehaviour
    {
        private PostProcessVolume volume;

        private float timeToLerp = 0.2f;

        private Vignette vignette;
        private Bloom bloom;
        private ColorGrading colorGrading;
        private ChromaticAberration chromaticAberration;
        private LensDistortion lensDistortion;

        private float vignetteIntensity, bloomIntensity, colorGradingIntensity, chromaticAberrationIntesity, lensDistortionIntensity;

        private void Awake()
        {
            volume = gameObject.GetComponent<PostProcessVolume>();
        }

        private void Start()
        {
            volume.profile.TryGetSettings(out vignette);
            volume.profile.TryGetSettings(out bloom);
            volume.profile.TryGetSettings(out lensDistortion);
            volume.profile.TryGetSettings(out chromaticAberration);
            volume.profile.TryGetSettings(out colorGrading);

            vignetteIntensity = vignette.intensity.value;
            bloomIntensity = bloom.intensity.value;
            chromaticAberrationIntesity = chromaticAberration.intensity.value;
            lensDistortionIntensity = lensDistortion.intensity.value;

            LoadSceneEffect();
        }

        private void LoadSceneEffect()
        {
            StartCoroutine(LerpSecondLens(22f, 1f, 44f));
            StartCoroutine(LerpSecondBloom(9f, 1f, 20f));
        }

        public void TutorialFinishEffect()
        {
            if (gameObject.activeSelf)
            {
                StartCoroutine(TutorialEffect());
            }
        }

        private IEnumerator TutorialEffect()
        {
            StartCoroutine(LerpFirstChrom(1f, timeToLerp));
            StartCoroutine(LerpFistBloom(40, timeToLerp));
            yield return StartCoroutine(LerpFirstLens(45f, timeToLerp));

            SceneManager.LoadScene("MainMenu");
        }

        public void ShieldPostProcessing()
        {
            if (gameObject.activeSelf)
            {
                StartCoroutine(LerpFirstLens(45f, timeToLerp));
                StartCoroutine(LerpFirstChrom(1f, timeToLerp));
                StartCoroutine(LerpFistBloom(20f, timeToLerp));
            }
        }

        IEnumerator LerpFistBloom(float targetValue, float duration)
        {
            float time = 0;
            float startValue = bloomIntensity;

            while (time < duration)
            {
                bloom.intensity.value = Mathf.Lerp(startValue, targetValue, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
            bloom.intensity.value = targetValue;

            StartCoroutine(LerpSecondBloom(bloomIntensity, timeToLerp, bloom.intensity.value));
        }

        IEnumerator LerpSecondBloom(float targetValue, float duration, float startValueB)
        {
            float time = 0;
            float startValue = startValueB;

            while (time < duration)
            {
                bloom.intensity.value = Mathf.Lerp(startValue, targetValue, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
            bloom.intensity.value = targetValue;
        }

        IEnumerator LerpFirstChrom(float targetValue, float duration)
        {
            float time = 0;
            float startValue = chromaticAberrationIntesity;

            while (time < duration)
            {
                chromaticAberration.intensity.value = Mathf.Lerp(startValue, targetValue, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
            chromaticAberration.intensity.value = targetValue;

            StartCoroutine(LerpSecondChrom(chromaticAberrationIntesity, timeToLerp));
        }

        IEnumerator LerpSecondChrom(float targetValue, float duration)
        {
            float time = 0;
            float startValue = chromaticAberration.intensity.value;

            while (time < duration)
            {
                chromaticAberration.intensity.value = Mathf.Lerp(startValue, targetValue, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
            chromaticAberration.intensity.value = targetValue;
        }

        IEnumerator LerpFirstLens(float targetValue, float duration)
        {
            float time = 0;
            float startValue = lensDistortionIntensity;

            while (time < duration)
            {
                lensDistortion.intensity.value = Mathf.Lerp(startValue, targetValue, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
            lensDistortion.intensity.value = targetValue;

            StartCoroutine(LerpSecondLens(lensDistortionIntensity, timeToLerp, lensDistortion.intensity.value));
        }

        IEnumerator LerpSecondLens(float targetValue, float duration, float startValueL)
        {
            float time = 0;
            float startValue = startValueL;

            while (time < duration)
            {
                lensDistortion.intensity.value = Mathf.Lerp(startValue, targetValue, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
            lensDistortion.intensity.value = targetValue;
        }
    }

}
