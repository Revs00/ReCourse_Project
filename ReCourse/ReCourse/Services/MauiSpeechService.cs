using ReCourse.Shared.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Media;

namespace ReCourse.Services
{
    public class MauiSpeechService : ISpeechService
    {
        // Fitur TTS (Text to Speech)
        public async Task SpeakAsync(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return;

            var options = new Microsoft.Maui.Media.SpeechOptions();

            // Trik paksa: Mencari properti secara dinamis agar tidak error saat compile
            var prop = options.GetType().GetProperty("Speed") ?? options.GetType().GetProperty("Rate");
            if (prop != null)
            {
                prop.SetValue(options, 0.5f); // Set kecepatan ke 0.3 (Pelan dan Jelas)
            }

            await TextToSpeech.Default.SpeakAsync(text, options);
        }

        // Fitur STT (Speech to Text)
        public async Task<string> ListenAsync()
        {
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            try
            {
                var status = await Permissions.RequestAsync<Permissions.Microphone>();
                if (status != PermissionStatus.Granted)
                    return "Izin mikrofon tidak diberikan.";

                // --- SOLUSI BARU: Biarkan sistem memilih bahasa default ---
                // Kita gunakan CultureInfo.CurrentCulture secara langsung
                var result = await SpeechToText.Default.ListenAsync(
                    CultureInfo.CurrentCulture,
                    new Progress<string>(),
                    cts.Token);

                if (result.IsSuccessful)
                {
                    return string.IsNullOrWhiteSpace(result.Text) ? "Tidak ada suara terdeteksi." : result.Text;
                }

                var errorMsg = result.Exception?.Message ?? "Gagal tanpa pesan";

                // Tambahkan saran jika error bahasa masih muncul
                if (errorMsg.Contains("language is not supported"))
                {
                    return "Error: Bahasa tidak didukung. Pastikan 'Speech Recognition' sudah terpasang di Windows Settings.";
                }

                return $"Gagal: {errorMsg}";
            }
            catch (Exception ex)
            {
                return $"Kesalahan: {ex.Message}";
            }
        }
    }
}
