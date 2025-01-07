using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace DownloadHtmlAsync
{
	public class HtmlDownloader
	{
		private static readonly HttpClient httpClient = new HttpClient();

		public static async Task DownloadHtmlAsync(string url, string filePath)
		{
			try
			{
				HttpResponseMessage response = await httpClient.GetAsync(url);
				response.EnsureSuccessStatusCode();

				string htmlContent = await response.Content.ReadAsStringAsync();
				await File.WriteAllTextAsync(filePath, htmlContent);

				Console.WriteLine($"Downloaded and saved HTML content from {url} to {filePath}");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred while downloading {url}: {ex.Message}");
			}
		}
	}

	public class Program
	{
		public static async Task DownloadHtmlFromUrlsAsync(List<string> urls, string directoryPath)
		{
			if (!Directory.Exists(directoryPath))
			{
				Directory.CreateDirectory(directoryPath);
			}

			List<Task> downloadTasks = new List<Task>();

			foreach (string url in urls)
			{
				string fileName = Path.Combine(directoryPath, $"{Guid.NewGuid()}.html");
				downloadTasks.Add(HtmlDownloader.DownloadHtmlAsync(url, fileName));
			}

			// Wait for all tasks to complete
			await Task.WhenAll(downloadTasks);

			// Wait for any one task to complete
			int completedTaskIndex = Task.WaitAny(downloadTasks.ToArray());
			Console.WriteLine($"Task {completedTaskIndex + 1} completed first.");
		}

		static async Task Main(string[] args)
		{
			List<string> urls = new List<string>
			{
				"https://www.google.com",
				"https://www.yahoo.com",
				"https://www.msn.com",
				"https://www.abc.com"
			};

			string directoryPath = "path/to/your/directory";

			await DownloadHtmlFromUrlsAsync(urls, directoryPath);
		}
	}
}
