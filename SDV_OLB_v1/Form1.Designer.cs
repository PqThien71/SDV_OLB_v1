using System.Drawing;
using System.Windows.Forms;

namespace SDV_OLB_v1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnHeader = new System.Windows.Forms.Panel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.pnFindCam = new System.Windows.Forms.Panel();
            this.btnTestConnect = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.cbxCamera = new System.Windows.Forms.ComboBox();
            this.cbxInterface = new System.Windows.Forms.ComboBox();
            this.cbxCamIndex = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLiveCam = new System.Windows.Forms.Button();
            this.pnSetCam = new System.Windows.Forms.Panel();
            this.nbTimeout = new System.Windows.Forms.NumericUpDown();
            this.nbGain = new System.Windows.Forms.NumericUpDown();
            this.nbExTime = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pnCurrentCam = new System.Windows.Forms.Panel();
            this.btnSaveCam = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.lbCurrentDevice = new System.Windows.Forms.Label();
            this.lbCurrentInterface = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.WindowControl = new HalconDotNet.HSmartWindowControl();
            this.pnFindCam.SuspendLayout();
            this.pnSetCam.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbGain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbExTime)).BeginInit();
            this.pnCurrentCam.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnHeader
            // 
            this.pnHeader.Location = new System.Drawing.Point(12, 2);
            this.pnHeader.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnHeader.Name = "pnHeader";
            this.pnHeader.Size = new System.Drawing.Size(644, 40);
            this.pnHeader.TabIndex = 0;
            // 
            // pnFindCam
            // 
            this.pnFindCam.Controls.Add(this.btnTestConnect);
            this.pnFindCam.Controls.Add(this.button1);
            this.pnFindCam.Controls.Add(this.cbxCamera);
            this.pnFindCam.Controls.Add(this.cbxInterface);
            this.pnFindCam.Controls.Add(this.cbxCamIndex);
            this.pnFindCam.Controls.Add(this.label4);
            this.pnFindCam.Controls.Add(this.label3);
            this.pnFindCam.Controls.Add(this.label2);
            this.pnFindCam.Controls.Add(this.label1);
            this.pnFindCam.Location = new System.Drawing.Point(662, 2);
            this.pnFindCam.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnFindCam.Name = "pnFindCam";
            this.pnFindCam.Size = new System.Drawing.Size(334, 163);
            this.pnFindCam.TabIndex = 1;
            // 
            // btnTestConnect
            // 
            this.btnTestConnect.Location = new System.Drawing.Point(209, 128);
            this.btnTestConnect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnTestConnect.Name = "btnTestConnect";
            this.btnTestConnect.Size = new System.Drawing.Size(94, 23);
            this.btnTestConnect.TabIndex = 8;
            this.btnTestConnect.Text = "Connection";
            this.btnTestConnect.UseVisualStyleBackColor = true;
            this.btnTestConnect.Click += new System.EventHandler(this.btnTestConnect_Click_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(88, 128);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Find Cam";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbxCamera
            // 
            this.cbxCamera.FormattingEnabled = true;
            this.cbxCamera.Location = new System.Drawing.Point(88, 90);
            this.cbxCamera.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbxCamera.Name = "cbxCamera";
            this.cbxCamera.Size = new System.Drawing.Size(215, 24);
            this.cbxCamera.TabIndex = 6;
            // 
            // cbxInterface
            // 
            this.cbxInterface.FormattingEnabled = true;
            this.cbxInterface.Location = new System.Drawing.Point(88, 60);
            this.cbxInterface.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbxInterface.Name = "cbxInterface";
            this.cbxInterface.Size = new System.Drawing.Size(215, 24);
            this.cbxInterface.TabIndex = 5;
            this.cbxInterface.SelectedIndexChanged += new System.EventHandler(this.cbxInterface_SelectedIndexChanged);
            // 
            // cbxCamIndex
            // 
            this.cbxCamIndex.FormattingEnabled = true;
            this.cbxCamIndex.Items.AddRange(new object[] {
            "1",
            "2"});
            this.cbxCamIndex.Location = new System.Drawing.Point(88, 29);
            this.cbxCamIndex.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbxCamIndex.Name = "cbxCamIndex";
            this.cbxCamIndex.Size = new System.Drawing.Size(215, 24);
            this.cbxCamIndex.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Device";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Interface";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Cam";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "FIND CAM";
            // 
            // btnLiveCam
            // 
            this.btnLiveCam.Location = new System.Drawing.Point(222, 62);
            this.btnLiveCam.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLiveCam.Name = "btnLiveCam";
            this.btnLiveCam.Size = new System.Drawing.Size(94, 23);
            this.btnLiveCam.TabIndex = 9;
            this.btnLiveCam.Text = "Live";
            this.btnLiveCam.UseVisualStyleBackColor = true;
            this.btnLiveCam.Click += new System.EventHandler(this.btnLiveCam_Click);
            // 
            // pnSetCam
            // 
            this.pnSetCam.Controls.Add(this.nbTimeout);
            this.pnSetCam.Controls.Add(this.nbGain);
            this.pnSetCam.Controls.Add(this.nbExTime);
            this.pnSetCam.Controls.Add(this.label8);
            this.pnSetCam.Controls.Add(this.label7);
            this.pnSetCam.Controls.Add(this.label6);
            this.pnSetCam.Controls.Add(this.label5);
            this.pnSetCam.Location = new System.Drawing.Point(662, 170);
            this.pnSetCam.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnSetCam.Name = "pnSetCam";
            this.pnSetCam.Size = new System.Drawing.Size(334, 113);
            this.pnSetCam.TabIndex = 2;
            // 
            // nbTimeout
            // 
            this.nbTimeout.Location = new System.Drawing.Point(153, 81);
            this.nbTimeout.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nbTimeout.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nbTimeout.Name = "nbTimeout";
            this.nbTimeout.Size = new System.Drawing.Size(150, 22);
            this.nbTimeout.TabIndex = 14;
            this.nbTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nbGain
            // 
            this.nbGain.Location = new System.Drawing.Point(153, 56);
            this.nbGain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nbGain.Name = "nbGain";
            this.nbGain.Size = new System.Drawing.Size(150, 22);
            this.nbGain.TabIndex = 13;
            this.nbGain.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nbExTime
            // 
            this.nbExTime.Location = new System.Drawing.Point(153, 30);
            this.nbExTime.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nbExTime.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nbExTime.Name = "nbExTime";
            this.nbExTime.Size = new System.Drawing.Size(150, 22);
            this.nbExTime.TabIndex = 12;
            this.nbExTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 82);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 16);
            this.label8.TabIndex = 11;
            this.label8.Text = "Timeout";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 16);
            this.label7.TabIndex = 10;
            this.label7.Text = "Gain";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 16);
            this.label6.TabIndex = 9;
            this.label6.Text = "Exposure time";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "PARAMETER CAM";
            // 
            // pnCurrentCam
            // 
            this.pnCurrentCam.Controls.Add(this.btnLiveCam);
            this.pnCurrentCam.Controls.Add(this.btnSaveCam);
            this.pnCurrentCam.Controls.Add(this.checkBox1);
            this.pnCurrentCam.Controls.Add(this.lbCurrentDevice);
            this.pnCurrentCam.Controls.Add(this.lbCurrentInterface);
            this.pnCurrentCam.Controls.Add(this.label11);
            this.pnCurrentCam.Controls.Add(this.label10);
            this.pnCurrentCam.Controls.Add(this.label9);
            this.pnCurrentCam.Location = new System.Drawing.Point(662, 295);
            this.pnCurrentCam.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnCurrentCam.Name = "pnCurrentCam";
            this.pnCurrentCam.Size = new System.Drawing.Size(334, 107);
            this.pnCurrentCam.TabIndex = 2;
            // 
            // btnSaveCam
            // 
            this.btnSaveCam.Location = new System.Drawing.Point(222, 30);
            this.btnSaveCam.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSaveCam.Name = "btnSaveCam";
            this.btnSaveCam.Size = new System.Drawing.Size(94, 23);
            this.btnSaveCam.TabIndex = 9;
            this.btnSaveCam.Text = "Save";
            this.btnSaveCam.UseVisualStyleBackColor = true;
            this.btnSaveCam.Click += new System.EventHandler(this.btnSaveCam_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(13, 86);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(92, 20);
            this.checkBox1.TabIndex = 18;
            this.checkBox1.Text = "Grab Asyn";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // lbCurrentDevice
            // 
            this.lbCurrentDevice.AutoSize = true;
            this.lbCurrentDevice.Location = new System.Drawing.Point(131, 62);
            this.lbCurrentDevice.Name = "lbCurrentDevice";
            this.lbCurrentDevice.Size = new System.Drawing.Size(40, 16);
            this.lbCurrentDevice.TabIndex = 17;
            this.lbCurrentDevice.Text = "None";
            // 
            // lbCurrentInterface
            // 
            this.lbCurrentInterface.AutoSize = true;
            this.lbCurrentInterface.Location = new System.Drawing.Point(131, 33);
            this.lbCurrentInterface.Name = "lbCurrentInterface";
            this.lbCurrentInterface.Size = new System.Drawing.Size(40, 16);
            this.lbCurrentInterface.TabIndex = 16;
            this.lbCurrentInterface.Text = "None";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 62);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(50, 16);
            this.label11.TabIndex = 9;
            this.label11.Text = "Device";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 33);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 16);
            this.label10.TabIndex = 9;
            this.label10.Text = "Interface";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(106, 16);
            this.label9.TabIndex = 15;
            this.label9.Text = "CURRENT CAM";
            // 
            // WindowControl
            // 
            this.WindowControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WindowControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.WindowControl.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.WindowControl.BackColor = System.Drawing.Color.Transparent;
            this.WindowControl.HDoubleClickToFitContent = true;
            this.WindowControl.HDrawingObjectsModifier = HalconDotNet.HSmartWindowControl.DrawingObjectsModifier.None;
            this.WindowControl.HImagePart = new System.Drawing.Rectangle(-31, -87, 702, 654);
            this.WindowControl.HKeepAspectRatio = true;
            this.WindowControl.HMoveContent = true;
            this.WindowControl.HZoomContent = HalconDotNet.HSmartWindowControl.ZoomContent.WheelForwardZoomsIn;
            this.WindowControl.Location = new System.Drawing.Point(12, 46);
            this.WindowControl.Margin = new System.Windows.Forms.Padding(0);
            this.WindowControl.Name = "WindowControl";
            this.WindowControl.Size = new System.Drawing.Size(644, 383);
            this.WindowControl.TabIndex = 56;
            this.WindowControl.WindowSize = new System.Drawing.Size(644, 383);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 449);
            this.Controls.Add(this.WindowControl);
            this.Controls.Add(this.pnCurrentCam);
            this.Controls.Add(this.pnSetCam);
            this.Controls.Add(this.pnFindCam);
            this.Controls.Add(this.pnHeader);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.WindowControl_Load);
            this.pnFindCam.ResumeLayout(false);
            this.pnFindCam.PerformLayout();
            this.pnSetCam.ResumeLayout(false);
            this.pnSetCam.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbGain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbExTime)).EndInit();
            this.pnCurrentCam.ResumeLayout(false);
            this.pnCurrentCam.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel pnHeader;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Panel pnFindCam;
        private Panel pnSetCam;
        private Panel pnCurrentCam;
        private Label label1;
        private Label label4;
        private Label label3;
        private Label label2;
        private ComboBox cbxCamera;
        private ComboBox cbxInterface;
        private ComboBox cbxCamIndex;
        private Button btnTestConnect;
        private Button button1;
        private HalconDotNet.HSmartWindowControl WindowControl;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label lbCurrentDevice;
        private Label lbCurrentInterface;
        private Label label11;
        private Label label10;
        private Label label9;
        private Button btnSaveCam;
        private CheckBox checkBox1;
        private NumericUpDown nbTimeout;
        private NumericUpDown nbGain;
        private NumericUpDown nbExTime;
        private Button btnLiveCam;
    }
}