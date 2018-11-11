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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.WorkspacePictureBox = new System.Windows.Forms.PictureBox();
            this.EditionGroupBox = new System.Windows.Forms.GroupBox();
            this.ImportExportGroupBox = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WorkspacePictureBox)).BeginInit();
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
            this.splitContainer1.Panel2.Controls.Add(this.ImportExportGroupBox);
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
            this.EditionGroupBox.Name = "EditionGroupBox";
            this.EditionGroupBox.TabStop = false;
            // 
            // ImportExportGroupBox
            // 
            resources.ApplyResources(this.ImportExportGroupBox, "ImportExportGroupBox");
            this.ImportExportGroupBox.Name = "ImportExportGroupBox";
            this.ImportExportGroupBox.TabStop = false;
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox ImportExportGroupBox;
        private System.Windows.Forms.GroupBox EditionGroupBox;
        private System.Windows.Forms.PictureBox WorkspacePictureBox;
    }
}

