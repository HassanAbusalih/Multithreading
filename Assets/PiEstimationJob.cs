using System;

public class PiEstimationJob : Job<double, int>
{
    public override void Execute(int numberOfPoints)
    {
        double piEstimate = EstimatePi(numberOfPoints);
        OnJobComplete?.Invoke(piEstimate);
    }

    private double EstimatePi(int numberOfPoints)
    {
        Random random = new Random();
        int pointsInsideCircle = 0;

        for (int i = 0; i < numberOfPoints; i++)
        {
            double x = random.NextDouble();
            double y = random.NextDouble();

            if (x * x + y * y <= 1)
                pointsInsideCircle++;
        }

        return 4.0 * pointsInsideCircle / numberOfPoints;
    }
}