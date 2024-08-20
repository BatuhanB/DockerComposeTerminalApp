using System.Diagnostics;
using System.Text;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.Windows.Interop;

namespace DockerComposeTerminalApp;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private string selectedFolder = string.Empty;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void SelectFolderButton_Click(object sender, RoutedEventArgs e)
    {
        using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
        {
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                selectedFolder = dialog.SelectedPath;
                SelectedFolderText.Text = selectedFolder;
                RunDockerComposeButton.IsEnabled = true;
                StopDockerComposeButton.IsEnabled = true;
            }
        }
    }

    private async void RunDockerComposeButton_Click(object sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrEmpty(selectedFolder))
        {
            await ExecuteDockerCommandAsync("docker-compose build && docker-compose up -d");
        }
    }

    private async void StopDockerComposeButton_Click(object sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrEmpty(selectedFolder))
        {
            await ExecuteDockerCommandAsync("docker-compose down");
        }
    }

    private async Task ExecuteDockerCommandAsync(string command)
    {
        try
        {
            var startInfo = new ProcessStartInfo
            {
                WorkingDirectory = selectedFolder,
                FileName = "cmd.exe",
                Arguments = $"/C {command}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (var process = new Process())
            {
                process.StartInfo = startInfo;
                process.OutputDataReceived += (s, e) =>
                {
                    if (e.Data != null)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            TerminalOutput.AppendText(e.Data + Environment.NewLine);
                            TerminalOutput.Visibility = Visibility.Visible;
                        });
                    }
                };
                process.ErrorDataReceived += (s, e) =>
                {
                    if (e.Data != null)
                    {
                        if (e.Data.Contains("Error", StringComparison.OrdinalIgnoreCase))
                        {
                            Dispatcher.Invoke(() =>
                            {
                                TerminalOutput.AppendText("Error: " + e.Data + Environment.NewLine);
                                TerminalOutput.Visibility = Visibility.Visible;
                            });
                        }
                    }
                };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                await Task.Run(() => process.WaitForExit());
            }

            System.Windows.MessageBox.Show("Command executed successfully.");
        }
        catch (Exception ex)
        {
            Dispatcher.Invoke(() => TerminalOutput.AppendText($"An error occurred: {ex.Message}" + Environment.NewLine));
        }
    }
}