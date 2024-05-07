namespace Burger.Shared.Dtos
{
    public record ResultWithDataDto<TData> (bool IsSuccess, TData Data ,string? errorMessage)
    {
        public static ResultWithDataDto<TData> Success(TData data) => new ResultWithDataDto<TData>(true, data, null);
        public static ResultWithDataDto<TData> Failure(string? error) => new ResultWithDataDto<TData>(false, default, error);
    }
}
