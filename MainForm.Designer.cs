namespace NVCP_Toggle
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showHideMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Text = "NVIDIA Color Toggle";
            this.notifyIcon.Visible = true;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showHideMenuItem,
            this.toggleMenuItem,
            this.exitMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(134, 70);
            // 
            // showHideMenuItem
            // 
            this.showHideMenuItem.Name = "showHideMenuItem";
            this.showHideMenuItem.Size = new System.Drawing.Size(133, 22);
            this.showHideMenuItem.Text = "Show/Hide";
            //
            // toggleMenuItem
            //
            this.toggleMenuItem.Name = "toggleMenuItem";
            this.toggleMenuItem.Size = new System.Drawing.Size(133, 22);
            this.toggleMenuItem.Text = "Toggle";
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.Size = new System.Drawing.Size(133, 22);
            this.exitMenuItem.Text = "Exit";
            // Initialize containers
            this.groupBoxProfile1 = new System.Windows.Forms.GroupBox();
            this.groupBoxProfile2 = new System.Windows.Forms.GroupBox();
            
            // Initialize master controls
            this.btnToggle = new System.Windows.Forms.Button();
            this.checkedListBoxDisplays = new System.Windows.Forms.CheckedListBox();
            this.labelDisplays = new System.Windows.Forms.Label();
            this.labelHotkey = new System.Windows.Forms.Label();
            this.txtHotkey = new System.Windows.Forms.TextBox();
            this.btnSetHotkey = new System.Windows.Forms.Button();

            // Profile 1 Controls
            this.label1 = new System.Windows.Forms.Label();
            this.lblVibranceValue1 = new System.Windows.Forms.Label();
            this.trackVibrance1 = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.lblBrightnessValue1 = new System.Windows.Forms.Label();
            this.trackBrightness1 = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.lblContrastValue1 = new System.Windows.Forms.Label();
            this.trackContrast1 = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.lblGammaValue1 = new System.Windows.Forms.Label();
            this.trackGamma1 = new System.Windows.Forms.TrackBar();
            this.btnSave1 = new System.Windows.Forms.Button();

            // Profile 2 Controls
            this.label6 = new System.Windows.Forms.Label();
            this.lblGammaValue2 = new System.Windows.Forms.Label();
            this.trackGamma2 = new System.Windows.Forms.TrackBar();
            this.label7 = new System.Windows.Forms.Label();
            this.lblContrastValue2 = new System.Windows.Forms.Label();
            this.trackContrast2 = new System.Windows.Forms.TrackBar();
            this.label8 = new System.Windows.Forms.Label();
            this.lblBrightnessValue2 = new System.Windows.Forms.Label();
            this.trackBrightness2 = new System.Windows.Forms.TrackBar();
            this.label10 = new System.Windows.Forms.Label();
            this.lblVibranceValue2 = new System.Windows.Forms.Label();
            this.trackVibrance2 = new System.Windows.Forms.TrackBar();
            this.btnSave2 = new System.Windows.Forms.Button();

            // Suspend Layout
            this.groupBoxProfile1.SuspendLayout();
            this.groupBoxProfile2.SuspendLayout();
            this.SuspendLayout();
            
            // Setup GroupBoxes
            SetupGroupBox(this.groupBoxProfile1, "Profile 1", 12, 12, 380, 320);
            SetupGroupBox(this.groupBoxProfile2, "Profile 2", 400, 12, 380, 320);

            // Setup Profile 1 Controls
            SetupSliderGroup(this.label1, this.lblVibranceValue1, this.trackVibrance1, "Vibrance:", 15, 25, 0, 100);
            SetupSliderGroup(this.label3, this.lblBrightnessValue1, this.trackBrightness1, "Brightness:", 15, 80, 0, 100);
            SetupSliderGroup(this.label4, this.lblContrastValue1, this.trackContrast1, "Contrast:", 15, 135, 0, 100);
            SetupSliderGroup(this.label5, this.lblGammaValue1, this.trackGamma1, "Gamma:", 15, 190, 30, 280);
            this.btnSave1.Location = new System.Drawing.Point(15, 280);
            this.btnSave1.Size = new System.Drawing.Size(350, 35);
            this.btnSave1.Name = "btnSave1";
            this.btnSave1.Text = "Save Profile 1";
            this.groupBoxProfile1.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.label1, this.lblVibranceValue1, this.trackVibrance1,
                this.label3, this.lblBrightnessValue1, this.trackBrightness1, this.label4, this.lblContrastValue1, this.trackContrast1,
                this.label5, this.lblGammaValue1, this.trackGamma1, this.btnSave1
            });
            
            // Setup Profile 2 Controls
            SetupSliderGroup(this.label10, this.lblVibranceValue2, this.trackVibrance2, "Vibrance:", 15, 25, 0, 100);
            SetupSliderGroup(this.label8, this.lblBrightnessValue2, this.trackBrightness2, "Brightness:", 15, 80, 0, 100);
            SetupSliderGroup(this.label7, this.lblContrastValue2, this.trackContrast2, "Contrast:", 15, 135, 0, 100);
            SetupSliderGroup(this.label6, this.lblGammaValue2, this.trackGamma2, "Gamma:", 15, 190, 30, 280);
            this.btnSave2.Location = new System.Drawing.Point(15, 280);
            this.btnSave2.Size = new System.Drawing.Size(350, 35);
            this.btnSave2.Name = "btnSave2";
            this.btnSave2.Text = "Save Profile 2";
            this.groupBoxProfile2.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.label10, this.lblVibranceValue2, this.trackVibrance2,
                this.label8, this.lblBrightnessValue2, this.trackBrightness2, this.label7, this.lblContrastValue2, this.trackContrast2,
                this.label6, this.lblGammaValue2, this.trackGamma2, this.btnSave2
            });

            // Setup other controls
            this.labelDisplays.Location = new System.Drawing.Point(12, 340);
            this.labelDisplays.Text = "Apply to Displays:";
            this.labelDisplays.AutoSize = true;
            this.checkedListBoxDisplays.FormattingEnabled = true;
            this.checkedListBoxDisplays.Location = new System.Drawing.Point(15, 360);
            this.checkedListBoxDisplays.Name = "checkedListBoxDisplays";
            this.checkedListBoxDisplays.Size = new System.Drawing.Size(377, 100);

            this.btnToggle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnToggle.Location = new System.Drawing.Point(400, 360);
            this.btnToggle.Name = "btnToggle";
            this.btnToggle.Size = new System.Drawing.Size(380, 50);
            this.btnToggle.Text = "TOGGLE";

            this.labelHotkey.Location = new System.Drawing.Point(400, 425);
            this.labelHotkey.Text = "Global Hotkey:";
            this.labelHotkey.AutoSize = true;
            this.txtHotkey.Location = new System.Drawing.Point(500, 423);
            this.txtHotkey.Name = "txtHotkey";
            this.txtHotkey.Size = new System.Drawing.Size(180, 22);
            this.txtHotkey.ReadOnly = true;
            this.btnSetHotkey.Location = new System.Drawing.Point(690, 420);
            this.btnSetHotkey.Name = "btnSetHotkey";
            this.btnSetHotkey.Size = new System.Drawing.Size(90, 28);
            this.btnSetHotkey.Text = "Set";

            // Setup MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 470);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.groupBoxProfile1, this.groupBoxProfile2, this.labelDisplays, this.checkedListBoxDisplays,
                this.btnToggle, this.labelHotkey, this.txtHotkey, this.btnSetHotkey
            });
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "NVIDIA Color Toggle";
            
            // Resume Layout
            this.groupBoxProfile1.ResumeLayout(false);
            this.groupBoxProfile1.PerformLayout();
            this.groupBoxProfile2.ResumeLayout(false);
            this.groupBoxProfile2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        // Control declarations
        private System.Windows.Forms.GroupBox groupBoxProfile1, groupBoxProfile2;
        private System.Windows.Forms.Label label1, label3, label4, label5, label6, label7, label8, label10;
        private System.Windows.Forms.Label lblVibranceValue1, lblBrightnessValue1, lblContrastValue1, lblGammaValue1;
        private System.Windows.Forms.Label lblVibranceValue2, lblBrightnessValue2, lblContrastValue2, lblGammaValue2;
        private System.Windows.Forms.TrackBar trackVibrance1, trackBrightness1, trackContrast1, trackGamma1;
        private System.Windows.Forms.TrackBar trackVibrance2, trackBrightness2, trackContrast2, trackGamma2;
        private System.Windows.Forms.Button btnSave1, btnSave2, btnToggle, btnSetHotkey;
        private System.Windows.Forms.CheckedListBox checkedListBoxDisplays;
        private System.Windows.Forms.Label labelDisplays, labelHotkey;
        private System.Windows.Forms.TextBox txtHotkey;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem showHideMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toggleMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;

        // Helper methods for UI setup
        private void SetupGroupBox(System.Windows.Forms.GroupBox box, string text, int x, int y, int width, int height)
        {
            box.Location = new System.Drawing.Point(x, y);
            box.Name = "groupBox" + text.Replace(" ", "");
            box.Size = new System.Drawing.Size(width, height);
            box.TabStop = false;
            box.Text = text;
            box.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            box.ForeColor = System.Drawing.Color.White;
        }

        private void SetupSliderGroup(System.Windows.Forms.Label label, System.Windows.Forms.Label valueLabel, System.Windows.Forms.TrackBar trackBar, string text, int x, int y, int min, int max)
        {
            // Label for the slider
            label.AutoSize = true;
            label.Location = new System.Drawing.Point(x, y);
            label.Text = text;
            label.ForeColor = System.Drawing.Color.White;

            // TrackBar (Slider)
            trackBar.Location = new System.Drawing.Point(x + 80, y - 5);
            trackBar.Maximum = max;
            trackBar.Minimum = min;
            trackBar.Size = new System.Drawing.Size(180, 56);
            trackBar.TickFrequency = (max - min) / 10;
            
            // Label for the value
            valueLabel.AutoSize = false;
            valueLabel.Location = new System.Drawing.Point(x + 265, y);
            valueLabel.Size = new System.Drawing.Size(50, 17);
            valueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            valueLabel.ForeColor = System.Drawing.Color.White;
        }
    }
}
