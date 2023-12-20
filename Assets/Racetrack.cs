using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Racetrack : MonoBehaviour
{
    [SerializeField] int repetitions = 10;
    [SerializeField] int points = 1000000;
    [SerializeField] float timeForSingleThread;
    [SerializeField] float timeForMultiThread;
    [SerializeField] float singleThreadCompletions;
    [SerializeField] float multiThreadCompletions;
    [SerializeField] TextMeshProUGUI timeForSingleThreadText;
    [SerializeField] TextMeshProUGUI timeForMultiThreadText;
    [SerializeField] TextMeshProUGUI singleThreadCompletionsText;
    [SerializeField] TextMeshProUGUI multiThreadCompletionsText;

    private void Start()
    {
        StartCoroutine(Race());
    }

    void Update()
    {
        singleThreadCompletionsText.text = singleThreadCompletions.ToString();
        multiThreadCompletionsText.text = multiThreadCompletions.ToString();
        timeForSingleThreadText.text = timeForSingleThread.ToString();
        timeForMultiThreadText.text = timeForMultiThread.ToString();
    }

    IEnumerator Race()
    {
        float time = Time.realtimeSinceStartup;
        for (int i = 0; i < repetitions; i++)
        {
            singleThreadCompletions++;
            EstimatePi(points);
        }
        timeForSingleThread = Time.realtimeSinceStartup - time;

        time = Time.realtimeSinceStartup;
        for (int i = 0; i < repetitions; i++)
        {
            PiEstimationJob piEstimationJob = new();
            piEstimationJob.OnJobComplete += JobComplete;
            JobSystem.StartJob(piEstimationJob, points);
        }
        yield return new WaitUntil(() => multiThreadCompletions == repetitions);
        timeForMultiThread = Time.realtimeSinceStartup - time;
    }

    void JobComplete(double pi)
    {
        multiThreadCompletions++;
    }

    void EstimatePi(int numberOfPoints)
    {
        System.Random random = new();
        int pointsInsideCircle = 0;

        for (int i = 0; i < numberOfPoints; i++)
        {
            double x = random.NextDouble();
            double y = random.NextDouble();

            if (x * x + y * y <= 1)
                pointsInsideCircle++;
        }
    }
}