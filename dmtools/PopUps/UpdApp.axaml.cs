using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace dmtools.PopUps;

public partial class UpdApp : Window
{
    public System.Net.Http.HttpClient wc { get; set; }
    public string updatelink { get; set; }
    public bool cancelled { get; set; }
    public UpdApp(string updlink)
    {
        InitializeComponent();
        ini();
        updatelink = updlink;
    }
    public bool IsRe { get; set; }
    public void ini()
    {
        wc = new System.Net.Http.HttpClient();
    }

    private void Window_OnClosing(object? sender, WindowClosingEventArgs e)
    {
        e.Cancel = true;
    }
    
    private async void Start_OnClick(object? sender, RoutedEventArgs e)
    {
        File.Delete("dmtoolssetup.exe");
        cancelled = false;
        this.Closing += Window_OnClosing;
        Start.IsEnabled = false;
        this.Title = Application.Current.FindResource("Downloading").ToString();
        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(new System.Uri(updatelink), HttpCompletionOption.ResponseHeadersRead);
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("Error: " + response.StatusCode);
            return;
        }
        var totalBytes = response.Content.Headers.ContentLength ?? -1L;
        var canReportProgress = totalBytes != -1;
        var totalBytesRead = 0L;
        var readChunkSize = 8192;
        using (var contentStream = await response.Content.ReadAsStreamAsync())
        using (var fileStream = new FileStream("dmtoolssetup.exe", FileMode.Create, FileAccess.Write, FileShare.None, readChunkSize, true))
        {
            var buffer = new byte[readChunkSize];
            int bytesRead;
            while ((bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                if (cancelled)
                {
                    break;
                }
                await fileStream.WriteAsync(buffer, 0, bytesRead);
                totalBytesRead += bytesRead;
                if (canReportProgress)
                {
                    var progressPercentage = Math.Round((double)totalBytesRead / totalBytes * 100, 0);
                    Progress.Value = progressPercentage;
                }
            }
        }
        OnDownloadFileCompleted();
    }
    public static EventHandler def { get; set; }
    private async void OnDownloadFileCompleted()
    {
        if (cancelled)
        {
            Start.IsEnabled = true;
            this.Title = Application.Current.FindResource("Cancelled").ToString();
            Start.Content = Application.Current.FindResource("Start").ToString();
            Progress.Value = 0;
            File.Delete("dmtoolssetup.exe");
            return;
        }
        Process.Start("dmtoolssetup.exe");
        if (Avalonia.Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.Shutdown();
        }
    }

    private Task<bool> unpack()
    {
        return Task.Run(() =>
        {
            string zipPath = "Media.zip";
            string extractPath = System.IO.Directory.GetCurrentDirectory();
            System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, extractPath);
            return true;
        });
    }
    private void Cancel_OnClick(object? sender, RoutedEventArgs e)
    {
        cancelled = true;
        this.Closing -= Window_OnClosing;
        this.Close();
    }
}