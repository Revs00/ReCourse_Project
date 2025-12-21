using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReCourse.Shared.Services
{
    public interface ISpeechService
    {
        // Text to Speech
        Task SpeakAsync(string text);

        // Speech to Text
        Task<string> ListenAsync();
    }
}
