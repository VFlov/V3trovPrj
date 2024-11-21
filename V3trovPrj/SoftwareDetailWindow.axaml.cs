using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using V3trovPrj.Models;

namespace V3trovPrj
{
    public partial class SoftwareDetailWindow : Window
    {
        private Software Software { get; }

        public SoftwareDetailWindow(Software software)
        {
            this.Software = software;
            InitializeComponent();

            Icon.Source = Additions.ImageDownload(software.IconPath).Result;
            Name.Text = software.Name;
            Description.Text = software.Description;

            this.KeyDown += SoftwareDetailWindow_KeyDown;
        }

        private void SoftwareDetailWindow_KeyDown(object sender, Avalonia.Input.KeyEventArgs e)
        {
            if (e.Key == Avalonia.Input.Key.Escape)
            {
                this.Close();
            }
        }

        private async void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            await DownloadAndInstall();
        }

        public async Task DownloadAndInstall()
        {
            var tempFilePath = Path.ChangeExtension(Path.GetTempFileName(), ".exe");
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(Additions.ServerPath + Software.DownloadPath);
                response.EnsureSuccessStatusCode();
                using (var fileStream = File.Create(tempFilePath))
                {
                    await response.Content.CopyToAsync(fileStream);
                }
            }
            Process.Start(new ProcessStartInfo
            {
                FileName = tempFilePath,
                UseShellExecute = true
            });
            File.Delete(tempFilePath);
        }
    }
}