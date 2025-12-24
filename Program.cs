using System;
using System.Windows.Forms;
using System.IO;
using NvAPIWrapper;
using WindowsDisplayAPI;
using System.Collections.Generic;
using System.Linq;

namespace NVCP_Toggle
{
    public static class ToggleLogic
    {
        private const int SystemDefaultVibrance = 50;

        public static string PerformToggle(List<WindowsDisplayAPI.Display> selectedDisplays, ProfileSettings profile1, ProfileSettings profile2)
        {
            bool isNvidiaInitialized = false;
            string stateFile = Path.Combine(AppContext.BaseDirectory, "toggle_state.txt");
            string targetProfileName;
            ProfileSettings targetProfileSettings;

            try
            {
                string currentProfileState = File.Exists(stateFile) ? File.ReadAllText(stateFile).Trim() : "2";
                targetProfileName = (currentProfileState == "1") ? "profile2.json" : "profile1.json";
                string nextProfileState = (currentProfileState == "1") ? "2" : "1";
                targetProfileSettings = (currentProfileState == "1") ? profile2 : profile1;

                NVIDIA.Initialize();
                isNvidiaInitialized = true;
                
                ApplySettings(true, selectedDisplays, null);

                System.Threading.Thread.Sleep(150);

                ApplySettings(false, selectedDisplays, targetProfileSettings);
                
                File.WriteAllText(stateFile, nextProfileState);
                return $"Successfully toggled to {targetProfileName}.";
            }
            catch (Exception e)
            {
                throw new ApplicationException($"Error during toggle: {e.Message}", e);
            }
            finally
            {
                if (isNvidiaInitialized) NVIDIA.Unload();
            }
        }

        private static void ApplySettings(bool isReset, List<WindowsDisplayAPI.Display> targetDisplays, ProfileSettings settings)
        {
            var allNvDisplays = NvAPIWrapper.Display.Display.GetDisplays();
            var allWinDisplays = WindowsDisplayAPI.Display.GetDisplays().ToList();

            foreach (var winDisplay in targetDisplays)
            {
                int displayIndex = allWinDisplays.FindIndex(d => d.DeviceName == winDisplay.DeviceName);

                if (displayIndex != -1 && displayIndex < allNvDisplays.Length)
                {
                    var nvDisplay = allNvDisplays[displayIndex];

                    if (isReset)
                    {
                        nvDisplay.DigitalVibranceControl.CurrentLevel = SystemDefaultVibrance;
                        winDisplay.GammaRamp = new DisplayGammaRamp();
                    }
                    else
                    {
                        nvDisplay.DigitalVibranceControl.CurrentLevel = settings.vibrance;
                        winDisplay.GammaRamp = new DisplayGammaRamp(settings.brightness, settings.contrast, settings.gamma);
                    }
                }
            }
        }
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += (s, e) => ShowError(e.ExceptionObject as Exception);
            Application.ThreadException += (s, e) => ShowError(e.Exception);
            Application.Run(new MainForm());
        }

        private static void ShowError(Exception ex)
        {
            if (ex == null) return;
            MessageBox.Show($"An unexpected error occurred: {ex.Message}\n\n{ex.StackTrace}", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}