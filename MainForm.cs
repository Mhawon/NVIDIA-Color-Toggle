using System;
using System.Windows.Forms;
using System.IO;
using System.Text.Json;
using System.Linq;
using NvAPIWrapper;
using WindowsDisplayAPI;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NVCP_Toggle
{
    public class AppSettings
    {
        public ProfileSettings Profile1 { get; set; }
        public ProfileSettings Profile2 { get; set; }
        public HotkeySettings Hotkey { get; set; }
        public List<string> SelectedDisplays { get; set; }
    }

    public class HotkeySettings
    {
        public Keys Key { get; set; }
        public uint Modifiers { get; set; }
    }

    public class ProfileSettings
    {
        public int vibrance { get; set; }
        public float brightness { get; set; }
        public float contrast { get; set; }
        public float gamma { get; set; }

        public static ProfileSettings GetDefault()
        {
            return new ProfileSettings { vibrance = 50, brightness = 0.5f, contrast = 0.5f, gamma = 1.0f };
        }
    }

    public partial class MainForm : Form
    {
        [DllImport("user32.dll")] private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
        [DllImport("user32.dll")] private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        private const int HOTKEY_ID = 9000;
        private const int WM_HOTKEY = 0x0312;

        private readonly string configPath = Path.Combine(AppContext.BaseDirectory, "config.json");
        private AppSettings settings;

        public MainForm()
        {
            InitializeComponent();
            this.Icon = new System.Drawing.Icon(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("NVCP_Toggle.icon.ico"));
            notifyIcon.Icon = this.Icon;
            this.Load += MainForm_Load;
            this.FormClosing += MainForm_FormClosing;
            this.Resize += MainForm_Resize;

            notifyIcon.MouseDoubleClick += notifyIcon_MouseDoubleClick;
            showHideMenuItem.Click += showHideMenuItem_Click;
            toggleMenuItem.Click += toggleMenuItem_Click;
            exitMenuItem.Click += exitMenuItem_Click;
            
            // Apply Dark Theme
            this.BackColor = System.Drawing.Color.FromArgb(37, 37, 38);
            this.ForeColor = System.Drawing.Color.White;

            ApplyDarkThemeToControls(this.Controls);

            btnSave1.Click += (s, e) => SaveProfile(1);
            btnSave2.Click += (s, e) => SaveProfile(2);
            btnToggle.Click += (s, e) => PerformToggleOperation();
            txtHotkey.KeyDown += txtHotkey_KeyDown;
            btnSetHotkey.Click += btnSetHotkey_Click;
            checkedListBoxDisplays.ItemCheck += checkedListBoxDisplays_ItemCheck;

            // Connect all sliders to the central scroll event handler
            trackVibrance1.Scroll += (s, e) => UpdateAllValueLabels();
            trackBrightness1.Scroll += (s, e) => UpdateAllValueLabels();
            trackContrast1.Scroll += (s, e) => UpdateAllValueLabels();
            trackGamma1.Scroll += (s, e) => UpdateAllValueLabels();

            trackVibrance2.Scroll += (s, e) => UpdateAllValueLabels();
            trackBrightness2.Scroll += (s, e) => UpdateAllValueLabels();
            trackContrast2.Scroll += (s, e) => UpdateAllValueLabels();
            trackGamma2.Scroll += (s, e) => UpdateAllValueLabels();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                this.ShowInTaskbar = false;
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void showHideMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.Hide();
                this.ShowInTaskbar = false;
            }
            else
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.ShowInTaskbar = true;
            }
        }

        private void toggleMenuItem_Click(object sender, EventArgs e)
        {
            PerformToggleOperation();
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ApplyDarkThemeToControls(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is GroupBox)
                {
                    control.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
                    control.ForeColor = System.Drawing.Color.White;
                    ApplyDarkThemeToControls(control.Controls);
                }
                else if (control is Button)
                {
                    Button btn = (Button)control;
                    btn.BackColor = System.Drawing.Color.FromArgb(63, 63, 70);
                    btn.ForeColor = System.Drawing.Color.White;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(80, 80, 80);
                }
                else if (control is TextBox)
                {
                    TextBox txt = (TextBox)control;
                    txt.BackColor = System.Drawing.Color.FromArgb(63, 63, 70);
                    txt.ForeColor = System.Drawing.Color.White;
                    txt.BorderStyle = BorderStyle.FixedSingle;
                }
                else if (control is CheckedListBox)
                {
                    CheckedListBox chk = (CheckedListBox)control;
                    chk.BackColor = System.Drawing.Color.FromArgb(63, 63, 70);
                    chk.ForeColor = System.Drawing.Color.White;
                    chk.BorderStyle = BorderStyle.FixedSingle;
                }
                else if (control is Label)
                {
                    control.ForeColor = System.Drawing.Color.White;
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                NVIDIA.Initialize();
                PopulateDisplayList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing or detecting displays: {ex.Message}", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Enabled = false; return;
            }

            LoadConfiguration();
        }

        private void LoadConfiguration()
        {
            if (File.Exists(configPath))
            {
                string jsonString = File.ReadAllText(configPath);
                settings = JsonSerializer.Deserialize<AppSettings>(jsonString);
            }
            else
            {
                settings = new AppSettings
                {
                    Profile1 = ProfileSettings.GetDefault(),
                    Profile2 = ProfileSettings.GetDefault(),
                    Hotkey = new HotkeySettings { Key = Keys.None, Modifiers = 0 },
                    SelectedDisplays = new List<string>()
                };
            }

            // Load Profile 1
            trackVibrance1.Value = settings.Profile1.vibrance;
            trackBrightness1.Value = (int)(settings.Profile1.brightness * 100);
            trackContrast1.Value = (int)(settings.Profile1.contrast * 100);
            trackGamma1.Value = (int)(settings.Profile1.gamma * 100);

            // Load Profile 2
            trackVibrance2.Value = settings.Profile2.vibrance;
            trackBrightness2.Value = (int)(settings.Profile2.brightness * 100);
            trackContrast2.Value = (int)(settings.Profile2.contrast * 100);
            trackGamma2.Value = (int)(settings.Profile2.gamma * 100);

            // Load Hotkey
            if (settings.Hotkey != null && settings.Hotkey.Key != Keys.None)
            {
                txtHotkey.Text = GetHotkeyText(settings.Hotkey.Modifiers, settings.Hotkey.Key);
                // Delay hotkey registration to ensure form handle is fully initialized
                this.BeginInvoke(new Action(() => {
                    if (!RegisterHotKey(this.Handle, HOTKEY_ID, settings.Hotkey.Modifiers, (uint)settings.Hotkey.Key))
                    {
                        // Optionally log or display an error if registration fails on startup
                        // MessageBox.Show("Failed to register hotkey on startup.", "Hotkey Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }));
            }

                        else

                        {

                            txtHotkey.Text = "Click here and press a key combination.";

                        }

            

                        // Load Selected Displays

                                    if(settings.SelectedDisplays != null)

                                    {

                                        for (int i = 0; i < checkedListBoxDisplays.Items.Count; i++)

                                        {

                                            var display = (WindowsDisplayAPI.Display)checkedListBoxDisplays.Items[i];

                                            if (settings.SelectedDisplays.Contains(display.DeviceName))

                                            {

                                                checkedListBoxDisplays.SetItemChecked(i, true);

                                            }

                                            else

                                            {

                                                checkedListBoxDisplays.SetItemChecked(i, false);

                                            }

                                        }

                                    }

                        

                        UpdateAllValueLabels();

                    }

            

                    private void SaveConfiguration()

                    {

                        string jsonString = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });

                        File.WriteAllText(configPath, jsonString);

                    }

                    

                    private void SaveProfile(int profileNumber)

                    {

                        if (profileNumber == 1)

                        {

                            settings.Profile1 = new ProfileSettings {

                                vibrance = trackVibrance1.Value,

                                brightness = trackBrightness1.Value / 100.0f,

                                contrast = trackContrast1.Value / 100.0f,

                                gamma = trackGamma1.Value / 100.0f

                            };

                        }

                        else

                        {

                            settings.Profile2 = new ProfileSettings {

                                vibrance = trackVibrance2.Value,

                                brightness = trackBrightness2.Value / 100.0f,

                                contrast = trackContrast2.Value / 100.0f,

                                gamma = trackGamma2.Value / 100.0f

                            };

                        }

                        SaveConfiguration();

                        MessageBox.Show($"Profile {profileNumber} saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }

            

                    private void PerformToggleOperation()

                    {

                        var selectedDisplays = checkedListBoxDisplays.CheckedItems.OfType<WindowsDisplayAPI.Display>().ToList();

                        if (selectedDisplays.Count == 0)

                        {

                            MessageBox.Show("Please select at least one display.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            return;

                        }

            

                        try

                        {

                            string result = ToggleLogic.PerformToggle(selectedDisplays, settings.Profile1, settings.Profile2);

                            // Non-disruptive feedback could be a status bar message

                        }

                        catch (Exception ex)

                        {

                            MessageBox.Show($"Toggle failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }

                    }

                    

                    private void PopulateDisplayList()

                    {

                        checkedListBoxDisplays.Items.Clear();

                        foreach (var winDisplay in WindowsDisplayAPI.Display.GetDisplays())

                        {

                            string displayName = winDisplay.ToPathDisplayTarget().FriendlyName;

                            if (string.IsNullOrEmpty(displayName)) displayName = $"Display ({winDisplay.DeviceName})";

                            checkedListBoxDisplays.Items.Add(winDisplay);

                        }

                    }

                    

                    private void UpdateAllValueLabels()

                    {

                        lblVibranceValue1.Text = trackVibrance1.Value.ToString();

                        lblBrightnessValue1.Text = (trackBrightness1.Value / 100.0f).ToString("F2");

                        lblContrastValue1.Text = (trackContrast1.Value / 100.0f).ToString("F2");

                        lblGammaValue1.Text = (trackGamma1.Value / 100.0f).ToString("F2");

            

                        lblVibranceValue2.Text = trackVibrance2.Value.ToString();

                        lblBrightnessValue2.Text = (trackBrightness2.Value / 100.0f).ToString("F2");

                        lblContrastValue2.Text = (trackContrast2.Value / 100.0f).ToString("F2");

                        lblGammaValue2.Text = (trackGamma2.Value / 100.0f).ToString("F2");

                    }

            

                    // --- Hotkey Methods ---

                    private void txtHotkey_KeyDown(object sender, KeyEventArgs e)

                    {

                        e.SuppressKeyPress = true;

                        if (e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.ShiftKey || e.KeyCode == Keys.Alt) return;

                        

                        uint modifiers = 0;

                        if (e.Control) { modifiers |= 2; }

                        if (e.Alt) { modifiers |= 1; }

                        if (e.Shift) { modifiers |= 4; }

            

                        settings.Hotkey = new HotkeySettings { Key = e.KeyCode, Modifiers = modifiers };

                        txtHotkey.Text = GetHotkeyText(modifiers, e.KeyCode);

                    }

            

                    private string GetHotkeyText(uint modifiers, Keys key)

                    {

                        string text = "";

                        if ((modifiers & 2) > 0) text += "Ctrl + ";

                        if ((modifiers & 1) > 0) text += "Alt + ";

                        if ((modifiers & 4) > 0) text += "Shift + ";

                        text += key.ToString();

                        return text;

                    }

            

                    private void btnSetHotkey_Click(object sender, EventArgs e)

                    {

                        UnregisterHotKey(this.Handle, HOTKEY_ID);

                        if (settings.Hotkey.Key == Keys.None || settings.Hotkey.Modifiers == 0)

                        {

                            MessageBox.Show("Please select a valid combination with at least one modifier (Ctrl, Alt, Shift).", "Invalid Hotkey", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            return;

                        }

                        if (RegisterHotKey(this.Handle, HOTKEY_ID, settings.Hotkey.Modifiers, (uint)settings.Hotkey.Key))

                        {

                            SaveConfiguration();

                            MessageBox.Show($"Hotkey '{txtHotkey.Text}' registered successfully!", "Hotkey Set", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }

                        else

                        {

                            MessageBox.Show("Failed to register hotkey. It might be in use by another application.", "Hotkey Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }

                    }

                    

                    private void checkedListBoxDisplays_ItemCheck(object sender, ItemCheckEventArgs e)

                    {

                        var display = (WindowsDisplayAPI.Display)checkedListBoxDisplays.Items[e.Index];

                        if (e.NewValue == CheckState.Checked)

                        {

                            if (!settings.SelectedDisplays.Contains(display.DeviceName))

                            {

                                settings.SelectedDisplays.Add(display.DeviceName);

                            }

                        }

                        else // e.NewValue is Unchecked

                        {

                            settings.SelectedDisplays.Remove(display.DeviceName);

                        }

                        SaveConfiguration();

                    }

            

                    private void MainForm_FormClosing(object sender, FormClosingEventArgs e) => UnregisterHotKey(this.Handle, HOTKEY_ID);

                    

                    protected override void WndProc(ref Message m)

                    {

                        if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == HOTKEY_ID) PerformToggleOperation();

                        base.WndProc(ref m);

                    }

                }

            }

            