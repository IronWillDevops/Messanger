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

        public const string GitHubHtmlUrl = "https://github.com/IronWillDevops/Messager";
        public const string GitHubRepoUrl = "https://api.github.com/repos/IronWillDevops/Messanger/releases/latest";
        private readonly HttpClient httpClient;

        public event EventHandler<Version> UpdateAvailable;

        public UpdateChecker()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("YourAppName");
        }

        public async Task<Version> CheckForUpdateAsync()
        {
            HttpResponseMessage response = await httpClient.GetAsync(GitHubRepoUrl);
            response.EnsureSuccessStatusCode();

            string jsonString = await response.Content.ReadAsStringAsync();
            var releaseInfo = JsonSerializer.Deserialize<GitHubRelease>(jsonString);

            if (releaseInfo != null && Version.TryParse(releaseInfo.tag_name, out Version latestVersion))
            {
                return latestVersion;
            }

            throw new Exception("Invalid release information.");
        }

        public void NotifyUpdateAvailable(Version latestVersion)
        {
            UpdateAvailable?.Invoke(this, latestVersion);
        }
    }
    public class GitHubRelease
    {
        public string tag_name { get; set; }
        public string assets_url { get; set; }
    }

}
