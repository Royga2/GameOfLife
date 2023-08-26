
namespace GameOfLife.View
{
    partial class GameForm
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
            this.pbGrid = new System.Windows.Forms.PictureBox();
            this.nudCellSize = new System.Windows.Forms.NumericUpDown();
            this.labelCellSize = new System.Windows.Forms.Label();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonAdvanced = new System.Windows.Forms.Button();
            this.buttonStrartStop = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCellSize)).BeginInit();
            this.SuspendLayout();
            // 
            // pbGrid
            // 
            this.pbGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbGrid.Location = new System.Drawing.Point(12, 50);
            this.pbGrid.Name = "pbGrid";
            this.pbGrid.Size = new System.Drawing.Size(1405, 520);
            this.pbGrid.TabIndex = 0;
            this.pbGrid.TabStop = false;
            this.pbGrid.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbGrid_MouseClick);
            // 
            // nudCellSize
            // 
            this.nudCellSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudCellSize.Location = new System.Drawing.Point(80, 588);
            this.nudCellSize.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudCellSize.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudCellSize.Name = "nudCellSize";
            this.nudCellSize.Size = new System.Drawing.Size(46, 22);
            this.nudCellSize.TabIndex = 1;
            this.nudCellSize.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // labelCellSize
            // 
            this.labelCellSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelCellSize.AutoSize = true;
            this.labelCellSize.Location = new System.Drawing.Point(12, 590);
            this.labelCellSize.Name = "labelCellSize";
            this.labelCellSize.Size = new System.Drawing.Size(62, 17);
            this.labelCellSize.TabIndex = 3;
            this.labelCellSize.Text = "Cell Size";
            // 
            // buttonReset
            // 
            this.buttonReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonReset.Location = new System.Drawing.Point(141, 587);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 23);
            this.buttonReset.TabIndex = 4;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // buttonAdvanced
            // 
            this.buttonAdvanced.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdvanced.Location = new System.Drawing.Point(1250, 590);
            this.buttonAdvanced.Name = "buttonAdvanced";
            this.buttonAdvanced.Size = new System.Drawing.Size(75, 23);
            this.buttonAdvanced.TabIndex = 5;
            this.buttonAdvanced.Text = "Advanced";
            this.buttonAdvanced.UseVisualStyleBackColor = true;
            // 
            // buttonStrartStop
            // 
            this.buttonStrartStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStrartStop.Location = new System.Drawing.Point(1342, 590);
            this.buttonStrartStop.Name = "buttonStrartStop";
            this.buttonStrartStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStrartStop.TabIndex = 6;
            this.buttonStrartStop.Text = "Start";
            this.buttonStrartStop.UseVisualStyleBackColor = true;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1429, 658);
            this.Controls.Add(this.buttonStrartStop);
            this.Controls.Add(this.buttonAdvanced);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.labelCellSize);
            this.Controls.Add(this.nudCellSize);
            this.Controls.Add(this.pbGrid);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "GameForm";
            this.Text = "Game of Life Exc";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameForm_FormClosing);
            this.Load += new System.EventHandler(this.GameForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCellSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbGrid;
        private System.Windows.Forms.NumericUpDown nudCellSize;
        private System.Windows.Forms.Label labelCellSize;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonAdvanced;
        private System.Windows.Forms.Button buttonStrartStop;
    }
}

