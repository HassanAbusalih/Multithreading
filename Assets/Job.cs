using System;

public abstract class Job <Result, Parameter>
{
    public Action<Result> OnJobComplete;

    public abstract void Execute(Parameter parameter);
}