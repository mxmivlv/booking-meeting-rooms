using Microsoft.Extensions.Options;

namespace Infrastructure.Settings;

public class InfrastructureOptions: IOptions<InfrastructureSettings>
{
    public InfrastructureSettings Value { get; }
}