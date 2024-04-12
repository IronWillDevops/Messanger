using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Messanger.Model
{
    public class UpdateChecker
    {
      
        public const string GitHubHtmlUrl = "https://github.com/IronWillDevops/Messanger";
        public const string GitHubRepoUrl = "https://api.github.com/repos/IronWillDevops/Messanger/releases/latest";private readonly HttpClient _httpClient;
        public static Version AppVersion = new Version(0, 0, 0, 0);
        private readonly INotificationService _notificationService;

        public UpdateChecker(INotificationService notificationService)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Messanger");
            _notificationService = notificationService;
        }

        public async Task<Version> CheckForUpdateAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(GitHubRepoUrl);
                response.EnsureSuccessStatusCode();

                string jsonString = await response.Content.ReadAsStringAsync();
                var releaseInfo = JsonSerializer.Deserialize<GitHubRelease>(jsonString);

                if (releaseInfo != null && Version.TryParse(releaseInfo.tag_name, out Version latestVersion))
                {
                    return latestVersion;
                }
                throw new Exception("Invalid release information.");
            }
            catch (HttpRequestException ex)
            {
                _notificationService.ShowNotification("Failed to check for updates. Ensure you have an internet connection.");
                throw new Exception("Failed to check for updates. Ensure you have an internet connection.", ex);
            }
            catch (Exception ex)
            {
                _notificationService.ShowNotification("Failed to check for updates.");
                throw new Exception("Failed to check for updates.", ex);
            }
        }
    }

    public class GitHubRelease
    {
        public string tag_name { get; set; }
    }

}
