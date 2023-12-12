using Creations.Application.Common.Interfaces;

namespace Creations.Infrastructure.Services;
public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
