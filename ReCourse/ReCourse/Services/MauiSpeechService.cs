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
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(15));
            try
            {
                var isGranted = await SpeechToText.Default.RequestPermissions(cts.Token);
                if (!isGranted) return "Izin mikrofon ditolak.";

                // Menggunakan CultureInfo Indonesia secara eksplisit jika ingin tes bahasa Indonesia
                // Atau biarkan CultureInfo.CurrentCulture jika HP/PC sudah bahasa Indonesia
                var culture = System.Globalization.CultureInfo.CurrentCulture;

                var result = await SpeechToText.Default.ListenAsync(culture, new Progress<string>(), cts.Token);

                if (result.IsSuccessful)
                {
                    return result.Text;
                }

                // Jika error Online Speech muncul lagi, kita berikan instruksi ke user
                if (result.Exception?.Message.Contains("Privacy Settings") == true)
                {
                    return "Aktifkan 'Online Speech Recognition' di Privacy Settings Windows Anda.";
                }

                return $"Gagal: {result.Exception?.Message ?? "Tidak ada suara"}";
            }
            catch (Exception ex)
            {
                return $"Terjadi kesalahan: {ex.Message}";
            }
        }
    }
}
