namespace GK_P2_Filling
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.WorkspacePictureBox = new System.Windows.Forms.PictureBox();
            this.EditionGroupBox = new System.Windows.Forms.GroupBox();
            this.LighSourVectGB = new System.Windows.Forms.GroupBox();
            this.LighSourVectAnimRB = new System.Windows.Forms.RadioButton();
            this.LighSourVectConstRB = new System.Windows.Forms.RadioButton();
            this.NormalVectWDGB = new System.Windows.Forms.GroupBox();
            this.DisturbanceGB = new System.Windows.Forms.GroupBox();
            this.DisturbTextPB = new System.Windows.Forms.PictureBox();
            this.DisturbTextRB = new System.Windows.Forms.RadioButton();
            this.DisturbNoRB = new System.Windows.Forms.RadioButton();
            this.NormalVectGB = new System.Windows.Forms.GroupBox();
            this.NormVectTextPB = new System.Windows.Forms.PictureBox();
            this.NormalVectTextRB = new System.Windows.Forms.RadioButton();
            this.NormalVectConstRB = new System.Windows.Forms.RadioButton();
            this.LightColorGB = new System.Windows.Forms.GroupBox();
            this.LightColorBoxPB = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ObjColTextPB = new System.Windows.Forms.PictureBox();
            this.ObjColTextRB = new System.Windows.Forms.RadioButton();
            this.ObjColConstRB = new System.Windows.Forms.RadioButton();
            this.ColorBoxPB = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WorkspacePictureBox)).BeginInit();
            this.EditionGroupBox.SuspendLayout();
            this.LighSourVectGB.SuspendLayout();
            this.NormalVectWDGB.SuspendLayout();
            this.DisturbanceGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DisturbTextPB)).BeginInit();
            this.NormalVectGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NormVectTextPB)).BeginInit();
            this.LightColorGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LightColorBoxPB)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ObjColTextPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColorBoxPB)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.WorkspacePictureBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.EditionGroupBox);
            this.splitContainer1.TabStop = false;
            // 
            // WorkspacePictureBox
            // 
            resources.ApplyResources(this.WorkspacePictureBox, "WorkspacePictureBox");
            this.WorkspacePictureBox.BackColor = System.Drawing.Color.White;
            this.WorkspacePictureBox.Cursor = System.Windows.Forms.Cursors.Cross;
            this.WorkspacePictureBox.Name = "WorkspacePictureBox";
            this.WorkspacePictureBox.TabStop = false;
            this.WorkspacePictureBox.SizeChanged += new System.EventHandler(this.WorkspacePictureBox_SizeChanged);
            this.WorkspacePictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.WorkspacePictureBox_Paint);
            this.WorkspacePictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WorkspacePictureBox_MouseDown);
            this.WorkspacePictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.WorkspacePictureBox_MouseMove);
            this.WorkspacePictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.WorkspacePictureBox_MouseUp);
            // 
            // EditionGroupBox
            // 
            resources.ApplyResources(this.EditionGroupBox, "EditionGroupBox");
            this.EditionGroupBox.Controls.Add(this.LighSourVectGB);
            this.EditionGroupBox.Controls.Add(this.NormalVectWDGB);
            this.EditionGroupBox.Controls.Add(this.LightColorGB);
            this.EditionGroupBox.Controls.Add(this.groupBox1);
            this.EditionGroupBox.Name = "EditionGroupBox";
            this.EditionGroupBox.TabStop = false;
            // 
            // LighSourVectGB
            // 
            this.LighSourVectGB.Controls.Add(this.LighSourVectAnimRB);
            this.LighSourVectGB.Controls.Add(this.LighSourVectConstRB);
            resources.ApplyResources(this.LighSourVectGB, "LighSourVectGB");
            this.LighSourVectGB.Name = "LighSourVectGB";
            this.LighSourVectGB.TabStop = false;
            // 
            // LighSourVectAnimRB
            // 
            resources.ApplyResources(this.LighSourVectAnimRB, "LighSourVectAnimRB");
            this.LighSourVectAnimRB.Name = "LighSourVectAnimRB";
            this.LighSourVectAnimRB.UseVisualStyleBackColor = true;
            this.LighSourVectAnimRB.CheckedChanged += new System.EventHandler(this.LighSourVectAnimRB_CheckedChanged);
            // 
            // LighSourVectConstRB
            // 
            resources.ApplyResources(this.LighSourVectConstRB, "LighSourVectConstRB");
            this.LighSourVectConstRB.Checked = true;
            this.LighSourVectConstRB.Name = "LighSourVectConstRB";
            this.LighSourVectConstRB.TabStop = true;
            this.LighSourVectConstRB.UseVisualStyleBackColor = true;
            this.LighSourVectConstRB.CheckedChanged += new System.EventHandler(this.LighSourVectConstRB_CheckedChanged);
            // 
            // NormalVectWDGB
            // 
            this.NormalVectWDGB.Controls.Add(this.DisturbanceGB);
            this.NormalVectWDGB.Controls.Add(this.NormalVectGB);
            resources.ApplyResources(this.NormalVectWDGB, "NormalVectWDGB");
            this.NormalVectWDGB.Name = "NormalVectWDGB";
            this.NormalVectWDGB.TabStop = false;
            // 
            // DisturbanceGB
            // 
            this.DisturbanceGB.Controls.Add(this.DisturbTextPB);
            this.DisturbanceGB.Controls.Add(this.DisturbTextRB);
            this.DisturbanceGB.Controls.Add(this.DisturbNoRB);
            resources.ApplyResources(this.DisturbanceGB, "DisturbanceGB");
            this.DisturbanceGB.Name = "DisturbanceGB";
            this.DisturbanceGB.TabStop = false;
            // 
            // DisturbTextPB
            // 
            this.DisturbTextPB.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.DisturbTextPB, "DisturbTextPB");
            this.DisturbTextPB.Name = "DisturbTextPB";
            this.DisturbTextPB.TabStop = false;
            this.DisturbTextPB.Click += new System.EventHandler(this.DisturbTextPB_Click);
            // 
            // DisturbTextRB
            // 
            resources.ApplyResources(this.DisturbTextRB, "DisturbTextRB");
            this.DisturbTextRB.Checked = true;
            this.DisturbTextRB.Name = "DisturbTextRB";
            this.DisturbTextRB.TabStop = true;
            this.DisturbTextRB.UseVisualStyleBackColor = true;
            // 
            // DisturbNoRB
            // 
            resources.ApplyResources(this.DisturbNoRB, "DisturbNoRB");
            this.DisturbNoRB.Name = "DisturbNoRB";
            this.DisturbNoRB.UseVisualStyleBackColor = true;
            this.DisturbNoRB.CheckedChanged += new System.EventHandler(this.DisturbNoRB_CheckedChanged);
            // 
            // NormalVectGB
            // 
            this.NormalVectGB.Controls.Add(this.NormVectTextPB);
            this.NormalVectGB.Controls.Add(this.NormalVectTextRB);
            this.NormalVectGB.Controls.Add(this.NormalVectConstRB);
            resources.ApplyResources(this.NormalVectGB, "NormalVectGB");
            this.NormalVectGB.Name = "NormalVectGB";
            this.NormalVectGB.TabStop = false;
            // 
            // NormVectTextPB
            // 
            this.NormVectTextPB.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.NormVectTextPB, "NormVectTextPB");
            this.NormVectTextPB.Name = "NormVectTextPB";
            this.NormVectTextPB.TabStop = false;
            this.NormVectTextPB.Click += new System.EventHandler(this.NormVectTextPB_Click);
            // 
            // NormalVectTextRB
            // 
            resources.ApplyResources(this.NormalVectTextRB, "NormalVectTextRB");
            this.NormalVectTextRB.Checked = true;
            this.NormalVectTextRB.Name = "NormalVectTextRB";
            this.NormalVectTextRB.TabStop = true;
            this.NormalVectTextRB.UseVisualStyleBackColor = true;
            // 
            // NormalVectConstRB
            // 
            resources.ApplyResources(this.NormalVectConstRB, "NormalVectConstRB");
            this.NormalVectConstRB.Name = "NormalVectConstRB";
            this.NormalVectConstRB.UseVisualStyleBackColor = true;
            this.NormalVectConstRB.CheckedChanged += new System.EventHandler(this.NormalVectConstRB_CheckedChanged);
            // 
            // LightColorGB
            // 
            this.LightColorGB.Controls.Add(this.LightColorBoxPB);
            resources.ApplyResources(this.LightColorGB, "LightColorGB");
            this.LightColorGB.Name = "LightColorGB";
            this.LightColorGB.TabStop = false;
            // 
            // LightColorBoxPB
            // 
            this.LightColorBoxPB.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.LightColorBoxPB, "LightColorBoxPB");
            this.LightColorBoxPB.Name = "LightColorBoxPB";
            this.LightColorBoxPB.TabStop = false;
            this.LightColorBoxPB.Click += new System.EventHandler(this.LightColorBoxPB_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ObjColTextPB);
            this.groupBox1.Controls.Add(this.ObjColTextRB);
            this.groupBox1.Controls.Add(this.ObjColConstRB);
            this.groupBox1.Controls.Add(this.ColorBoxPB);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // ObjColTextPB
            // 
            this.ObjColTextPB.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.ObjColTextPB, "ObjColTextPB");
            this.ObjColTextPB.Name = "ObjColTextPB";
            this.ObjColTextPB.TabStop = false;
            this.ObjColTextPB.Click += new System.EventHandler(this.ObjColTextPB_Click);
            // 
            // ObjColTextRB
            // 
            resources.ApplyResources(this.ObjColTextRB, "ObjColTextRB");
            this.ObjColTextRB.Checked = true;
            this.ObjColTextRB.Name = "ObjColTextRB";
            this.ObjColTextRB.TabStop = true;
            this.ObjColTextRB.UseVisualStyleBackColor = true;
            this.ObjColTextRB.CheckedChanged += new System.EventHandler(this.ObjColTextRB_CheckedChanged);
            // 
            // ObjColConstRB
            // 
            resources.ApplyResources(this.ObjColConstRB, "ObjColConstRB");
            this.ObjColConstRB.Name = "ObjColConstRB";
            this.ObjColConstRB.UseVisualStyleBackColor = true;
            this.ObjColConstRB.CheckedChanged += new System.EventHandler(this.ObjColTextRB_CheckedChanged);
            // 
            // ColorBoxPB
            // 
            this.ColorBoxPB.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.ColorBoxPB, "ColorBoxPB");
            this.ColorBoxPB.Name = "ColorBoxPB";
            this.ColorBoxPB.TabStop = false;
            this.ColorBoxPB.Click += new System.EventHandler(this.ColorBoxPB_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.KeyPreview = true;
            this.Name = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.WorkspacePictureBox)).EndInit();
            this.EditionGroupBox.ResumeLayout(false);
            this.LighSourVectGB.ResumeLayout(false);
            this.LighSourVectGB.PerformLayout();
            this.NormalVectWDGB.ResumeLayout(false);
            this.DisturbanceGB.ResumeLayout(false);
            this.DisturbanceGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DisturbTextPB)).EndInit();
            this.NormalVectGB.ResumeLayout(false);
            this.NormalVectGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NormVectTextPB)).EndInit();
            this.LightColorGB.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LightColorBoxPB)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ObjColTextPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColorBoxPB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox EditionGroupBox;
        private System.Windows.Forms.PictureBox WorkspacePictureBox;
        private System.Windows.Forms.PictureBox ColorBoxPB;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton ObjColTextRB;
        private System.Windows.Forms.RadioButton ObjColConstRB;
        private System.Windows.Forms.GroupBox LightColorGB;
        private System.Windows.Forms.PictureBox LightColorBoxPB;
        private System.Windows.Forms.GroupBox NormalVectWDGB;
        private System.Windows.Forms.GroupBox DisturbanceGB;
        private System.Windows.Forms.GroupBox NormalVectGB;
        private System.Windows.Forms.RadioButton NormalVectTextRB;
        private System.Windows.Forms.RadioButton NormalVectConstRB;
        private System.Windows.Forms.RadioButton DisturbTextRB;
        private System.Windows.Forms.RadioButton DisturbNoRB;
        private System.Windows.Forms.GroupBox LighSourVectGB;
        private System.Windows.Forms.RadioButton LighSourVectAnimRB;
        private System.Windows.Forms.RadioButton LighSourVectConstRB;
        private System.Windows.Forms.PictureBox DisturbTextPB;
        private System.Windows.Forms.PictureBox NormVectTextPB;
        private System.Windows.Forms.PictureBox ObjColTextPB;
        private System.Windows.Forms.Timer timer1;
    }
}

