using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Visualiser : MonoBehaviour
{
    public float rms;
    public float db;
    public int sampleSize = 50;
    public float[] spectrum;

    private AudioSource source;
    private float[] samples;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        samples = new float[sampleSize];
        spectrum = new float[sampleSize];
    }

    private void Update()
    {
        AnalyzeSound();
    }

    private void AnalyzeSound()
    {
        source.GetOutputData(samples, 0);

        float sum = 0f;
        for (int i = 0; i < sampleSize; i++) {
            sum += samples[i] * samples[i];
        }

        rms = Mathf.Sqrt(sum / sampleSize);
        db = 20 * Mathf.Log10(rms * 10);

        source.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
    }
}
