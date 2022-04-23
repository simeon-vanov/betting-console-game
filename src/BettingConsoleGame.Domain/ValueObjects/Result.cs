using BettingConsoleGame.Domain.Enums;

namespace BettingConsoleGame.Domain.ValueObjects;

public class Result<T>
{
    private Result() { }

    public static Result<T> Succeed(T action) =>
        new Result<T>()
        {
            Value = action,
            Succeeded = true
        };

    public static Result<T> Fail(string error) =>
        new Result<T>()
        {
            Value = default,
            Succeeded = false,
            Errors = new List<string>() { error }
        };

    public static Result<T> Fail(IList<string> errors) =>
        new Result<T>()
        {
            Value = default,
            Succeeded = false,
            Errors = errors
        };

    public T Value { get; set; }

    public bool Succeeded { get; init; }

    public bool Failed => !Succeeded;

    public IList<string> Errors { get; set; } = new List<string>();
}
