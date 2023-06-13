using Microsoft.Extensions.Options;

namespace Application.Settings;

public class ApplicationOptions: IOptions<ApplicationSettings>
{
    public ApplicationSettings Value { get; }
}