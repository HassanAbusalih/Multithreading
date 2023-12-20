using System.Threading;

public class JobSystem
{
    public static void StartJob<Result, Parameter>(Job<Result, Parameter> job, Parameter parameter)
    {
        ThreadPool.QueueUserWorkItem(new WaitCallback( (object state) => { job.Execute(parameter); }));
    }
}