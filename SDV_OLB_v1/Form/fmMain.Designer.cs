namespace SDV_OLB_v1
{
    partial class fmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.WindowControl11 = new HalconDotNet.HSmartWindowControl();
            this.WindowControl12 = new HalconDotNet.HSmartWindowControl();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnHeader = new System.Windows.Forms.Panel();
            this.btnPLCconnect = new System.Windows.Forms.Button();
            this.btnCam2 = new System.Windows.Forms.Button();
            this.btnCam1 = new System.Windows.Forms.Button();
            this.btnSettingCam1 = new System.Windows.Forms.Button();
            this.btnSettingCam2 = new System.Windows.Forms.Button();
            this.btnSnapCam1 = new System.Windows.Forms.Button();
            this.btnSnapCam2 = new System.Windows.Forms.Button();
            this.pnHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // WindowControl11
            // 
            this.WindowControl11.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.WindowControl11.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.WindowControl11.HDoubleClickToFitContent = true;
            this.WindowControl11.HDrawingObjectsModifier = HalconDotNet.HSmartWindowControl.DrawingObjectsModifier.None;
            this.WindowControl11.HImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.WindowControl11.HKeepAspectRatio = true;
            this.WindowControl11.HMoveContent = true;
            this.WindowControl11.HZoomContent = HalconDotNet.HSmartWindowControl.ZoomContent.WheelForwardZoomsIn;
            this.WindowControl11.Location = new System.Drawing.Point(9, 256);
            this.WindowControl11.Margin = new System.Windows.Forms.Padding(0);
            this.WindowControl11.Name = "WindowControl11";
            this.WindowControl11.Size = new System.Drawing.Size(571, 392);
            this.WindowControl11.TabIndex = 0;
            this.WindowControl11.WindowSize = new System.Drawing.Size(571, 392);
            // 
            // WindowControl12
            // 
            this.WindowControl12.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.WindowControl12.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.WindowControl12.HDoubleClickToFitContent = true;
            this.WindowControl12.HDrawingObjectsModifier = HalconDotNet.HSmartWindowControl.DrawingObjectsModifier.None;
            this.WindowControl12.HImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.WindowControl12.HKeepAspectRatio = true;
            this.WindowControl12.HMoveContent = true;
            this.WindowControl12.HZoomContent = HalconDotNet.HSmartWindowControl.ZoomContent.WheelForwardZoomsIn;
            this.WindowControl12.Location = new System.Drawing.Point(588, 256);
            this.WindowControl12.Margin = new System.Windows.Forms.Padding(0);
            this.WindowControl12.Name = "WindowControl12";
            this.WindowControl12.Size = new System.Drawing.Size(563, 392);
            this.WindowControl12.TabIndex = 1;
            this.WindowControl12.WindowSize = new System.Drawing.Size(563, 392);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 225);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(568, 31);
            this.label1.TabIndex = 2;
            this.label1.Text = "Cam1";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(586, 225);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(565, 31);
            this.label2.TabIndex = 3;
            this.label2.Text = "Cam2";
            // 
            // pnHeader
            // 
            this.pnHeader.Controls.Add(this.btnPLCconnect);
            this.pnHeader.Controls.Add(this.btnCam2);
            this.pnHeader.Controls.Add(this.btnCam1);
            this.pnHeader.Location = new System.Drawing.Point(9, 12);
            this.pnHeader.Name = "pnHeader";
            this.pnHeader.Size = new System.Drawing.Size(1139, 210);
            this.pnHeader.TabIndex = 4;
            // 
            // btnPLCconnect
            // 
            this.btnPLCconnect.BackColor = System.Drawing.Color.Red;
            this.btnPLCconnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPLCconnect.ForeColor = System.Drawing.Color.Black;
            this.btnPLCconnect.Location = new System.Drawing.Point(1009, 0);
            this.btnPLCconnect.Name = "btnPLCconnect";
            this.btnPLCconnect.Size = new System.Drawing.Size(130, 46);
            this.btnPLCconnect.TabIndex = 2;
            this.btnPLCconnect.Text = "PLC";
            this.btnPLCconnect.UseVisualStyleBackColor = false;
            this.btnPLCconnect.Click += new System.EventHandler(this.btnPLCconnect_Click);
            // 
            // btnCam2
            // 
            this.btnCam2.BackColor = System.Drawing.Color.Red;
            this.btnCam2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCam2.ForeColor = System.Drawing.Color.Black;
            this.btnCam2.Location = new System.Drawing.Point(854, 0);
            this.btnCam2.Name = "btnCam2";
            this.btnCam2.Size = new System.Drawing.Size(159, 46);
            this.btnCam2.TabIndex = 1;
            this.btnCam2.Text = "CAMERA2";
            this.btnCam2.UseVisualStyleBackColor = false;
            // 
            // btnCam1
            // 
            this.btnCam1.BackColor = System.Drawing.Color.Red;
            this.btnCam1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCam1.ForeColor = System.Drawing.Color.Black;
            this.btnCam1.Location = new System.Drawing.Point(699, 0);
            this.btnCam1.Name = "btnCam1";
            this.btnCam1.Size = new System.Drawing.Size(159, 46);
            this.btnCam1.TabIndex = 0;
            this.btnCam1.Text = "CAMERA1";
            this.btnCam1.UseVisualStyleBackColor = false;
            // 
            // btnSettingCam1
            // 
            this.btnSettingCam1.Location = new System.Drawing.Point(555, 229);
            this.btnSettingCam1.Name = "btnSettingCam1";
            this.btnSettingCam1.Size = new System.Drawing.Size(24, 23);
            this.btnSettingCam1.TabIndex = 5;
            this.btnSettingCam1.UseVisualStyleBackColor = true;
            this.btnSettingCam1.Click += new System.EventHandler(this.btnSettingCam1_Click);
            // 
            // btnSettingCam2
            // 
            this.btnSettingCam2.Location = new System.Drawing.Point(1124, 230);
            this.btnSettingCam2.Name = "btnSettingCam2";
            this.btnSettingCam2.Size = new System.Drawing.Size(24, 23);
            this.btnSettingCam2.TabIndex = 6;
            this.btnSettingCam2.UseVisualStyleBackColor = true;
            // 
            // btnSnapCam1
            // 
            this.btnSnapCam1.Location = new System.Drawing.Point(525, 229);
            this.btnSnapCam1.Name = "btnSnapCam1";
            this.btnSnapCam1.Size = new System.Drawing.Size(24, 23);
            this.btnSnapCam1.TabIndex = 7;
            this.btnSnapCam1.UseVisualStyleBackColor = true;
            this.btnSnapCam1.Click += new System.EventHandler(this.btnSnapCam1_Click);
            // 
            // btnSnapCam2
            // 
            this.btnSnapCam2.Location = new System.Drawing.Point(1094, 229);
            this.btnSnapCam2.Name = "btnSnapCam2";
            this.btnSnapCam2.Size = new System.Drawing.Size(24, 23);
            this.btnSnapCam2.TabIndex = 8;
            this.btnSnapCam2.UseVisualStyleBackColor = true;
            this.btnSnapCam2.Click += new System.EventHandler(this.btnSnapCam2_Click);
            // 
            // fmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1160, 669);
            this.Controls.Add(this.btnSnapCam2);
            this.Controls.Add(this.btnSnapCam1);
            this.Controls.Add(this.btnSettingCam2);
            this.Controls.Add(this.btnSettingCam1);
            this.Controls.Add(this.pnHeader);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.WindowControl12);
            this.Controls.Add(this.WindowControl11);
            this.Name = "fmMain";
            this.Text = "RTC";
            this.Load += new System.EventHandler(this.fmMain_Load);
            this.pnHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private HalconDotNet.HSmartWindowControl WindowControl11;
        private HalconDotNet.HSmartWindowControl WindowControl12;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnHeader;
        private System.Windows.Forms.Button btnCam2;
        private System.Windows.Forms.Button btnCam1;
        private System.Windows.Forms.Button btnSettingCam1;
        private System.Windows.Forms.Button btnSettingCam2;
        private System.Windows.Forms.Button btnSnapCam1;
        private System.Windows.Forms.Button btnSnapCam2;
        private System.Windows.Forms.Button btnPLCconnect;
    }
}