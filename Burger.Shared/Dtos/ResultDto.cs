namespace Burger.Shared.Dtos
{
    public record ResultDto(bool IsSuccess, string? ErrorMessage)
    {
        public static ResultDto Success() => new (true, null);
        public static ResultDto Failure(string? error) => new (false, error);
    }
}
