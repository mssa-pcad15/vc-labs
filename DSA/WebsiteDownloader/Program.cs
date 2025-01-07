// See https://aka.ms/new-console-template for more information

using System.Collections.Concurrent;
using WebsiteDownloader;

Uri[] sitesUrl = [
                new Uri("https://google.com"),
                new Uri("https://msn.com"),
                new Uri("https://abc.com"),
                new Uri("https://wikipedia.com"),
                new Uri("https://cnbc.com"),
                new Uri("https://bloomberg.com"),
                new Uri("https://weather.com"),
];

ConcurrentQueue<Downloader> downloadersQueue = new();//thread safe queue for subsquent parallel ope
Parallel.ForEach(sitesUrl, site=> downloadersQueue.Enqueue(new Downloader(site))); // multi threaded

//parallel forEach

List<Task<int>> downloadTasks = new();

while (downloadersQueue.TryDequeue(out Downloader downloader))
{ 
    downloadTasks.Add(downloader.DownloadAsync()); 
}

await Task.WhenAll(downloadTasks);
int totalBytes = downloadTasks.Sum(d=>d.Result);
Console.WriteLine($"All sites downloaded, {totalBytes} bytes written.");
