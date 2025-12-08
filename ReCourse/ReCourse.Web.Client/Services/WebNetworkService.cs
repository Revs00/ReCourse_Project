using ReCourse.Shared.Services;
using System;

namespace ReCourse.Web.Client.Services
{
    // Placeholder untuk layanan jaringan di platform web
    // Karena aplikasi web biasanya selalu terhubung, layanan ini bersifat statis
    public class WebNetworkService : INetworkService
    {
        public event EventHandler<ConnectionStatus> ConnectivityChanged;
        public ConnectionStatus CurrentStatus => ConnectionStatus.Connected;
        public string CurrentConnectionType => "Web/Browser";
    }
}
