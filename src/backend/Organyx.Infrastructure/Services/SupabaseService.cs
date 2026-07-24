using Supabase;
using Client = Supabase.Client;

namespace Organyx.Infrastructure.Services;

public class SupabaseService
{
    public Client Client { get; }

    public SupabaseService()
    {
        var url = Environment.GetEnvironmentVariable("SUPABASE_URL")
            ?? throw new InvalidOperationException("SUPABASE_URL is required.");
        var serviceKey = Environment.GetEnvironmentVariable("SUPABASE_SERVICE_KEY")
            ?? Environment.GetEnvironmentVariable("SERVICE_ROLE_KEY")
            ?? throw new InvalidOperationException("SUPABASE_SERVICE_KEY (or SERVICE_ROLE_KEY) is required.");

        Client = new Client(url, serviceKey, new SupabaseOptions
        {
            AutoConnectRealtime = false,
            AutoRefreshToken = false
        });
    }

    public async Task InitializeAsync() => await Client.InitializeAsync();
}
