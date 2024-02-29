using Application.Common.Interfaces.Services;

namespace Infrastructure.Services
{
    internal class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}