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
            this.AddVerBut = new System.Windows.Forms.Button();
            this.edgesGB = new System.Windows.Forms.GroupBox();
            this.ConstLenRB = new System.Windows.Forms.RadioButton();
            this.VerticalRB = new System.Windows.Forms.RadioButton();
            this.HorizontalRB = new System.Windows.Forms.RadioButton();
            this.NoRestRB = new System.Windows.Forms.RadioButton();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.ImportExportGroupBox = new System.Windows.Forms.GroupBox();
            this.OpenButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.AutoRelCB = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WorkspacePictureBox)).BeginInit();
            this.EditionGroupBox.SuspendLayout();
            this.edgesGB.SuspendLayout();
            this.ImportExportGroupBox.SuspendLayout();
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
            this.WorkspacePictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WorkspacePictureBox_MouseDown);
            this.WorkspacePictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.WorkspacePictureBox_MouseMove);
            this.WorkspacePictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.WorkspacePictureBox_MouseUp);
            // 
            // EditionGroupBox
            // 
            resources.ApplyResources(this.EditionGroupBox, "EditionGroupBox");
            this.EditionGroupBox.Controls.Add(this.AddVerBut);
            this.EditionGroupBox.Controls.Add(this.edgesGB);
            this.EditionGroupBox.Controls.Add(this.DeleteButton);
            this.EditionGroupBox.Controls.Add(this.ClearButton);
            this.EditionGroupBox.Name = "EditionGroupBox";
            this.EditionGroupBox.TabStop = false;
            // 
            // AddVerBut
            // 
            resources.ApplyResources(this.AddVerBut, "AddVerBut");
            this.AddVerBut.Name = "AddVerBut";
            this.AddVerBut.UseVisualStyleBackColor = true;
            this.AddVerBut.Click += new System.EventHandler(this.AddVerBut_Click);
            // 
            // edgesGB
            // 
            this.edgesGB.Controls.Add(this.AutoRelCB);
            this.edgesGB.Controls.Add(this.ConstLenRB);
            this.edgesGB.Controls.Add(this.VerticalRB);
            this.edgesGB.Controls.Add(this.HorizontalRB);
            this.edgesGB.Controls.Add(this.NoRestRB);
            resources.ApplyResources(this.edgesGB, "edgesGB");
            this.edgesGB.Name = "edgesGB";
            this.edgesGB.TabStop = false;
            // 
            // ConstLenRB
            // 
            resources.ApplyResources(this.ConstLenRB, "ConstLenRB");
            this.ConstLenRB.Name = "ConstLenRB";
            this.ConstLenRB.UseVisualStyleBackColor = true;
            this.ConstLenRB.CheckedChanged += new System.EventHandler(this.ConstLenRB_CheckedChanged);
            // 
            // VerticalRB
            // 
            resources.ApplyResources(this.VerticalRB, "VerticalRB");
            this.VerticalRB.Name = "VerticalRB";
            this.VerticalRB.UseVisualStyleBackColor = true;
            this.VerticalRB.CheckedChanged += new System.EventHandler(this.VerticalRB_CheckedChanged);
            // 
            // HorizontalRB
            // 
            resources.ApplyResources(this.HorizontalRB, "HorizontalRB");
            this.HorizontalRB.Name = "HorizontalRB";
            this.HorizontalRB.UseVisualStyleBackColor = true;
            this.HorizontalRB.CheckedChanged += new System.EventHandler(this.HorizontalRB_CheckedChanged);
            // 
            // NoRestRB
            // 
            resources.ApplyResources(this.NoRestRB, "NoRestRB");
            this.NoRestRB.Checked = true;
            this.NoRestRB.Name = "NoRestRB";
            this.NoRestRB.TabStop = true;
            this.NoRestRB.UseVisualStyleBackColor = true;
            this.NoRestRB.CheckedChanged += new System.EventHandler(this.NoRestRB_CheckedChanged);
            // 
            // DeleteButton
            // 
            resources.ApplyResources(this.DeleteButton, "DeleteButton");
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // ClearButton
            // 
            resources.ApplyResources(this.ClearButton, "ClearButton");
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // ImportExportGroupBox
            // 
            resources.ApplyResources(this.ImportExportGroupBox, "ImportExportGroupBox");
            this.ImportExportGroupBox.Controls.Add(this.OpenButton);
            this.ImportExportGroupBox.Controls.Add(this.SaveButton);
            this.ImportExportGroupBox.Name = "ImportExportGroupBox";
            this.ImportExportGroupBox.TabStop = false;
            // 
            // OpenButton
            // 
            resources.ApplyResources(this.OpenButton, "OpenButton");
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.UseVisualStyleBackColor = true;
            this.OpenButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // SaveButton
            // 
            resources.ApplyResources(this.SaveButton, "SaveButton");
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // AutoRelCB
            // 
            resources.ApplyResources(this.AutoRelCB, "AutoRelCB");
            this.AutoRelCB.Name = "AutoRelCB";
            this.AutoRelCB.UseVisualStyleBackColor = true;
            // 
            // GraphEditor
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.KeyPreview = true;
            this.Name = "GraphEditor";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GraphEditor_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.WorkspacePictureBox)).EndInit();
            this.EditionGroupBox.ResumeLayout(false);
            this.edgesGB.ResumeLayout(false);
            this.edgesGB.PerformLayout();
            this.ImportExportGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox ImportExportGroupBox;
        private System.Windows.Forms.Button OpenButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.GroupBox EditionGroupBox;
        private System.Windows.Forms.PictureBox WorkspacePictureBox;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.GroupBox edgesGB;
        private System.Windows.Forms.RadioButton ConstLenRB;
        private System.Windows.Forms.RadioButton VerticalRB;
        private System.Windows.Forms.RadioButton HorizontalRB;
        private System.Windows.Forms.RadioButton NoRestRB;
        private System.Windows.Forms.Button AddVerBut;
        private System.Windows.Forms.CheckBox AutoRelCB;
    }
}

