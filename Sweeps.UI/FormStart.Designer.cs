namespace Sweeps.UI
{
    partial class FormStart
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
            this.btn_Start = new System.Windows.Forms.Button();
            this.numeric_Width = new System.Windows.Forms.NumericUpDown();
            this.numeric_Height = new System.Windows.Forms.NumericUpDown();
            this.numeric_Mines = new System.Windows.Forms.NumericUpDown();
            this.lbl_X = new System.Windows.Forms.Label();
            this.lbl_Height = new System.Windows.Forms.Label();
            this.lbl_MineCount = new System.Windows.Forms.Label();
            this.lbl_DifficultyPrefix = new System.Windows.Forms.Label();
            this.lbl_Difficulty = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_Width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_Height)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_Mines)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(197, 227);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(75, 23);
            this.btn_Start.TabIndex = 0;
            this.btn_Start.Text = "Start";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // numeric_Width
            // 
            this.numeric_Width.Location = new System.Drawing.Point(125, 34);
            this.numeric_Width.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numeric_Width.Name = "numeric_Width";
            this.numeric_Width.Size = new System.Drawing.Size(120, 20);
            this.numeric_Width.TabIndex = 1;
            this.numeric_Width.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numeric_Width.ValueChanged += new System.EventHandler(this.numeric_Width_ValueChanged);
            // 
            // numeric_Height
            // 
            this.numeric_Height.Location = new System.Drawing.Point(125, 78);
            this.numeric_Height.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numeric_Height.Name = "numeric_Height";
            this.numeric_Height.Size = new System.Drawing.Size(120, 20);
            this.numeric_Height.TabIndex = 2;
            this.numeric_Height.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numeric_Height.ValueChanged += new System.EventHandler(this.numeric_Height_ValueChanged);
            // 
            // numeric_Mines
            // 
            this.numeric_Mines.Location = new System.Drawing.Point(125, 118);
            this.numeric_Mines.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numeric_Mines.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numeric_Mines.Name = "numeric_Mines";
            this.numeric_Mines.Size = new System.Drawing.Size(120, 20);
            this.numeric_Mines.TabIndex = 3;
            this.numeric_Mines.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numeric_Mines.ValueChanged += new System.EventHandler(this.numeric_Mines_ValueChanged);
            // 
            // lbl_X
            // 
            this.lbl_X.AutoSize = true;
            this.lbl_X.Location = new System.Drawing.Point(30, 36);
            this.lbl_X.Name = "lbl_X";
            this.lbl_X.Size = new System.Drawing.Size(77, 13);
            this.lbl_X.TabIndex = 4;
            this.lbl_X.Text = "Width (5 - 100)";
            // 
            // lbl_Height
            // 
            this.lbl_Height.AutoSize = true;
            this.lbl_Height.Location = new System.Drawing.Point(30, 80);
            this.lbl_Height.Name = "lbl_Height";
            this.lbl_Height.Size = new System.Drawing.Size(80, 13);
            this.lbl_Height.TabIndex = 5;
            this.lbl_Height.Text = "Height (5 - 100)";
            // 
            // lbl_MineCount
            // 
            this.lbl_MineCount.AutoSize = true;
            this.lbl_MineCount.Location = new System.Drawing.Point(30, 120);
            this.lbl_MineCount.MaximumSize = new System.Drawing.Size(77, 13);
            this.lbl_MineCount.MinimumSize = new System.Drawing.Size(77, 13);
            this.lbl_MineCount.Name = "lbl_MineCount";
            this.lbl_MineCount.Size = new System.Drawing.Size(77, 13);
            this.lbl_MineCount.TabIndex = 6;
            this.lbl_MineCount.Text = "Mines (5 - 500)";
            // 
            // lbl_DifficultyPrefix
            // 
            this.lbl_DifficultyPrefix.AutoSize = true;
            this.lbl_DifficultyPrefix.Location = new System.Drawing.Point(12, 232);
            this.lbl_DifficultyPrefix.Name = "lbl_DifficultyPrefix";
            this.lbl_DifficultyPrefix.Size = new System.Drawing.Size(53, 13);
            this.lbl_DifficultyPrefix.TabIndex = 7;
            this.lbl_DifficultyPrefix.Text = "Difficulty: ";
            // 
            // lbl_Difficulty
            // 
            this.lbl_Difficulty.AutoSize = true;
            this.lbl_Difficulty.Location = new System.Drawing.Point(71, 232);
            this.lbl_Difficulty.Name = "lbl_Difficulty";
            this.lbl_Difficulty.Size = new System.Drawing.Size(0, 13);
            this.lbl_Difficulty.TabIndex = 8;
            // 
            // FormStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.lbl_Difficulty);
            this.Controls.Add(this.lbl_DifficultyPrefix);
            this.Controls.Add(this.lbl_MineCount);
            this.Controls.Add(this.lbl_Height);
            this.Controls.Add(this.lbl_X);
            this.Controls.Add(this.numeric_Mines);
            this.Controls.Add(this.numeric_Height);
            this.Controls.Add(this.numeric_Width);
            this.Controls.Add(this.btn_Start);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(300, 300);
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "FormStart";
            this.Text = "Mines Setup";
            ((System.ComponentModel.ISupportInitialize)(this.numeric_Width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_Height)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_Mines)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.NumericUpDown numeric_Width;
        private System.Windows.Forms.NumericUpDown numeric_Height;
        private System.Windows.Forms.NumericUpDown numeric_Mines;
        private System.Windows.Forms.Label lbl_X;
        private System.Windows.Forms.Label lbl_Height;
        private System.Windows.Forms.Label lbl_MineCount;
        private System.Windows.Forms.Label lbl_DifficultyPrefix;
        private System.Windows.Forms.Label lbl_Difficulty;
    }
}