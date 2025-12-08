using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReCourse.Shared.Services
{
    // Enum untuk merepresentasikan status koneksi jaringan
    public enum ConnectionStatus
    {
        Unknown,
        Connected,
        Disconnected
    }
    public interface  INetworkService
    {
        // Event yang dipicu ketika status koneksi berubah
        event EventHandler<ConnectionStatus> ConnectivityChanged;
        //Mendapatkan status koneksi saat ini
        ConnectionStatus CurrentStatus { get; }
        // Mendapatkan jenis koneksi saat ini (misalnya WiFi, Mobile Data, dll.)
        string CurrentConnectionType { get; }

    }
}
