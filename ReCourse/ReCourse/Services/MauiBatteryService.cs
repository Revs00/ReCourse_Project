using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Devices; // Perlu untuk mengakses API baterai MAUI
using ReCourse.Shared.Services;

namespace ReCourse.Services
{
    public class MauiBatteryService : IBatteryService
    {
        private BatteryData _currentBatteryInfo = new BatteryData();
        // Implementasi event dari interface
        public event EventHandler<BatteryData> BatteryChanged;
        public BatteryData CurrentBatteryInfo => _currentBatteryInfo;
        public MauiBatteryService()
        {
            // Langganan ke event BatteryInfoChanged bawaan MAUI
            Battery.BatteryInfoChanged += Battery_BatteryInfoChanged;
            UpdateBatteryInfo();
        }
        public Task<BatteryData> LoadBatteryInfoAsync()
        {
            UpdateBatteryInfo();
            return Task.FromResult(_currentBatteryInfo);
        }

        private void Battery_BatteryInfoChanged(object? sender, BatteryInfoChangedEventArgs e)
        {
            // Memperbarui status dan memicu event shared
            UpdateBatteryInfo();
            BatteryChanged?.Invoke(this, _currentBatteryInfo);
        }
        private void UpdateBatteryInfo()
        {
            _currentBatteryInfo.ChargeLevel = Battery.Default.ChargeLevel;
            _currentBatteryInfo.State = Battery.Default.State.ToString();
            _currentBatteryInfo.Source = Battery.Default.PowerSource.ToString();

            if (Battery.Default.ChargeLevel <= 0.20 && Battery.Default.State == BatteryState.Discharging)
            {
                _currentBatteryInfo.StatusMessage = "Baterai lemah. Silakan isi daya perangkat Anda.";
            }
            else if (Battery.Default.State == BatteryState.Charging)
            {
                _currentBatteryInfo.StatusMessage = $"Mengisi daya dari {Battery.Default.PowerSource}";
            }
            else
            {
                _currentBatteryInfo.StatusMessage = "Baterai masih banyak.";
            }
        }

        public void Dispose()
        {
            // Penting: Hapus langganan saat service dibuang
            Battery.BatteryInfoChanged -= Battery_BatteryInfoChanged;
        }
    }
}
