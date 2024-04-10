using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace dmtools.PopUps;

public partial class MediaDownloading : Window
{
    public System.Net.Http.HttpClient wc { get; set; }
    public bool cancelled { get; set; }
    public MediaDownloading()
    {
        InitializeComponent();
        ini();
    }
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
        File.Delete("Media.zip");
        cancelled = false;
        this.Closing += Window_OnClosing;
        Start.Click -= Start_OnClick;
        Start.Content = this.Title = "Downloading...";
        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(new System.Uri("https://www.triangleonthewall.org/Media.zip"), HttpCompletionOption.ResponseHeadersRead);
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
        using (var fileStream = new FileStream("Media.zip", FileMode.Create, FileAccess.Write, FileShare.None, readChunkSize, true))
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
    
    private void OnDownloadFileCompleted()
    {
        if (cancelled)
        {
            Start.Click += Start_OnClick;
            this.Title = "Cancelled";
            Start.Content = "Start";
            Progress.Value = 0;
            File.Delete("Media.zip");
            return;
        }
        string zipPath = "Media.zip";
        string extractPath = System.IO.Directory.GetCurrentDirectory();
        Progress.IsIndeterminate = true;
        Start.Content = this.Title = "Unpacking...";
        Directory.Delete("Music", true);
        Directory.Delete("Images", true);
        System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, extractPath);
        Progress.IsIndeterminate = false;
        Progress.Value = 100;
        Start.Content = this.Title = "Finished";
        Start.Click += Cancel_OnClick;
    }
    private void Cancel_OnClick(object? sender, RoutedEventArgs e)
    {
        cancelled = true;
        this.Closing -= Window_OnClosing;
        this.Close();
    }
}