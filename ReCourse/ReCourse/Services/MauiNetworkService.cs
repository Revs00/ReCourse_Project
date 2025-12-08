using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Networking; // Perlu untuk mengakses API jaringan MAUI
using ReCourse.Shared.Services;

namespace ReCourse.Services
{
    public class MauiNetworkService : INetworkService
    {
        private ConnectionStatus _currentStatus = ConnectionStatus.Disconnected;
        public event EventHandler<ConnectionStatus> ConnectivityChanged;
        public MauiNetworkService()
        {
            // Mendaftarkan event handler untuk perubahan konektivitas
            Connectivity.ConnectivityChanged += OnConnectivityChanged;
            // Inisialisasi status koneksi saat ini
            UpdateStatus();
        }

        public ConnectionStatus CurrentStatus => _currentStatus;
        public string CurrentConnectionType
        {
            get
            {
                // Mengambil tipe jaringan MAUI dan menggabungkannya menjadi string
                var profiles = Connectivity.Current.ConnectionProfiles;
                if (profiles.Any())
                {
                    return string.Join(", ", profiles.Select(p => p.ToString()));
                }
                return "None";
            }
        }

        private void OnConnectivityChanged (object? sender, ConnectivityChangedEventArgs e)
        {
            UpdateStatus();
            ConnectivityChanged?.Invoke(this, _currentStatus);
        }
        private void UpdateStatus()
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                _currentStatus = ConnectionStatus.Connected;
            }
            else
            {
                _currentStatus = ConnectionStatus.Disconnected;
            }
        }
    }
}
