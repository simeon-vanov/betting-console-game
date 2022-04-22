using BettingConsoleGame.Domain.Enums;

namespace BettingConsoleGame.Domain.ValueObjects;

public class Result<T>
{
    private Result() { }

    public static Result<T> Succesful(T action) =>
        new Result<T>()
        {
            ResultItem = action,
            ResultType = ResultType.Success
        };

    public static Result<T> Failed(string error) =>
        new Result<T>()
        {
            ResultItem = default,
            ResultType = ResultType.Fail,
            Errors = new List<string>() { error }
        };

    public static Result<T> Failed(IList<string> errors) =>
        new Result<T>()
        {
            ResultItem = default,
            ResultType = ResultType.Fail,
            Errors = errors
        };

    public T ResultItem { get; set; }

    public ResultType ResultType { get; set; }

    public IList<string> Errors { get; set; } = new List<string>();
}
