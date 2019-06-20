namespace XO
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.InfoW = new System.Windows.Forms.GroupBox();
            this.lInfo = new System.Windows.Forms.Label();
            this.b2 = new System.Windows.Forms.Button();
            this.b1 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.IWRC = new System.Windows.Forms.Timer(this.components);
            this.obedit = new System.Windows.Forms.Timer(this.components);
            this.InfoW.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // InfoW
            // 
            this.InfoW.Controls.Add(this.lInfo);
            this.InfoW.Controls.Add(this.b2);
            this.InfoW.Controls.Add(this.b1);
            this.InfoW.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InfoW.Location = new System.Drawing.Point(183, 107);
            this.InfoW.Name = "InfoW";
            this.InfoW.Size = new System.Drawing.Size(202, 95);
            this.InfoW.TabIndex = 0;
            this.InfoW.TabStop = false;
            this.InfoW.Text = "infoW";
            this.InfoW.Visible = false;
            // 
            // lInfo
            // 
            this.lInfo.AutoSize = true;
            this.lInfo.Location = new System.Drawing.Point(12, 24);
            this.lInfo.Name = "lInfo";
            this.lInfo.Size = new System.Drawing.Size(46, 17);
            this.lInfo.TabIndex = 2;
            this.lInfo.Text = "label1";
            // 
            // b2
            // 
            this.b2.Location = new System.Drawing.Point(101, 58);
            this.b2.Name = "b2";
            this.b2.Size = new System.Drawing.Size(93, 24);
            this.b2.TabIndex = 1;
            this.b2.Text = "button2";
            this.b2.UseVisualStyleBackColor = true;
            this.b2.Click += new System.EventHandler(this.B2_Click);
            // 
            // b1
            // 
            this.b1.Location = new System.Drawing.Point(10, 58);
            this.b1.Name = "b1";
            this.b1.Size = new System.Drawing.Size(86, 24);
            this.b1.TabIndex = 0;
            this.b1.Text = "button1";
            this.b1.UseVisualStyleBackColor = true;
            this.b1.Click += new System.EventHandler(this.B1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(545, 1019);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 36);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // IWRC
            // 
            this.IWRC.Enabled = true;
            this.IWRC.Interval = 200;
            this.IWRC.Tick += new System.EventHandler(this.IWRC_Tick);
            // 
            // obedit
            // 
            this.obedit.Enabled = true;
            this.obedit.Interval = 10;
            this.obedit.Tick += new System.EventHandler(this.Obedit_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1488, 972);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.InfoW);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.Form1_HelpButtonClicked);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.Leave += new System.EventHandler(this.Form1_Leave);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.InfoW.ResumeLayout(false);
            this.InfoW.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox InfoW;
        private System.Windows.Forms.Label lInfo;
        private System.Windows.Forms.Button b2;
        private System.Windows.Forms.Button b1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer IWRC;
        private System.Windows.Forms.Timer obedit;
    }
}

