﻿
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
            this.buttonAdvancedGeneration = new System.Windows.Forms.Button();
            this.buttonStartStop = new System.Windows.Forms.Button();
            this.nudGameSpeed = new System.Windows.Forms.NumericUpDown();
            this.labelGameSpeed = new System.Windows.Forms.Label();
            this.labelGenerationCount = new System.Windows.Forms.Label();
            this.labelLiveCellCount = new System.Windows.Forms.Label();
            this.labelDeadCellCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCellSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGameSpeed)).BeginInit();
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
            this.pbGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbGrid_MouseDown);
            this.pbGrid.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbGrid_MouseMove);
            this.pbGrid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbGrid_MouseUp);
            // 
            // nudCellSize
            // 
            this.nudCellSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudCellSize.Location = new System.Drawing.Point(80, 588);
            this.nudCellSize.Maximum = new decimal(new int[] {
            35,
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
            this.buttonReset.Location = new System.Drawing.Point(156, 588);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 23);
            this.buttonReset.TabIndex = 4;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // buttonAdvancedGeneration
            // 
            this.buttonAdvancedGeneration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdvancedGeneration.Location = new System.Drawing.Point(1065, 590);
            this.buttonAdvancedGeneration.Name = "buttonAdvancedGeneration";
            this.buttonAdvancedGeneration.Size = new System.Drawing.Size(155, 23);
            this.buttonAdvancedGeneration.TabIndex = 5;
            this.buttonAdvancedGeneration.Text = "Advance Generation ";
            this.buttonAdvancedGeneration.UseVisualStyleBackColor = true;
            this.buttonAdvancedGeneration.Click += new System.EventHandler(this.buttonAdvancedGeneration_Click);
            // 
            // buttonStartStop
            // 
            this.buttonStartStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStartStop.Location = new System.Drawing.Point(1182, 623);
            this.buttonStartStop.Name = "buttonStartStop";
            this.buttonStartStop.Size = new System.Drawing.Size(208, 33);
            this.buttonStartStop.TabIndex = 6;
            this.buttonStartStop.Text = "Start Simulation";
            this.buttonStartStop.UseVisualStyleBackColor = true;
            this.buttonStartStop.Click += new System.EventHandler(this.buttonStartStop_Click);
            // 
            // nudGameSpeed
            // 
            this.nudGameSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nudGameSpeed.Location = new System.Drawing.Point(1344, 590);
            this.nudGameSpeed.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.nudGameSpeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudGameSpeed.Name = "nudGameSpeed";
            this.nudGameSpeed.Size = new System.Drawing.Size(46, 22);
            this.nudGameSpeed.TabIndex = 7;
            this.nudGameSpeed.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudGameSpeed.ValueChanged += new System.EventHandler(this.nudGameSpeed_ValueChanged);
            // 
            // labelGameSpeed
            // 
            this.labelGameSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelGameSpeed.AutoSize = true;
            this.labelGameSpeed.Location = new System.Drawing.Point(1247, 590);
            this.labelGameSpeed.Name = "labelGameSpeed";
            this.labelGameSpeed.Size = new System.Drawing.Size(91, 17);
            this.labelGameSpeed.TabIndex = 8;
            this.labelGameSpeed.Text = "Game Speed";
            // 
            // labelGenerationCount
            // 
            this.labelGenerationCount.AutoSize = true;
            this.labelGenerationCount.Location = new System.Drawing.Point(580, 22);
            this.labelGenerationCount.Name = "labelGenerationCount";
            this.labelGenerationCount.Size = new System.Drawing.Size(95, 17);
            this.labelGenerationCount.TabIndex = 9;
            this.labelGenerationCount.Text = "Generation: 0";
            this.labelGenerationCount.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelLiveCellCount
            // 
            this.labelLiveCellCount.AutoSize = true;
            this.labelLiveCellCount.Location = new System.Drawing.Point(314, 21);
            this.labelLiveCellCount.Name = "labelLiveCellCount";
            this.labelLiveCellCount.Size = new System.Drawing.Size(118, 17);
            this.labelLiveCellCount.TabIndex = 10;
            this.labelLiveCellCount.Text = "Live Cell Count: 0";
            // 
            // labelDeadCellCount
            // 
            this.labelDeadCellCount.AutoSize = true;
            this.labelDeadCellCount.Location = new System.Drawing.Point(175, 22);
            this.labelDeadCellCount.Name = "labelDeadCellCount";
            this.labelDeadCellCount.Size = new System.Drawing.Size(126, 17);
            this.labelDeadCellCount.TabIndex = 11;
            this.labelDeadCellCount.Text = "Dead Cell Count: 0";
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Linen;
            this.ClientSize = new System.Drawing.Size(1429, 658);
            this.Controls.Add(this.labelDeadCellCount);
            this.Controls.Add(this.labelLiveCellCount);
            this.Controls.Add(this.labelGenerationCount);
            this.Controls.Add(this.labelGameSpeed);
            this.Controls.Add(this.nudGameSpeed);
            this.Controls.Add(this.buttonStartStop);
            this.Controls.Add(this.buttonAdvancedGeneration);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.labelCellSize);
            this.Controls.Add(this.nudCellSize);
            this.Controls.Add(this.pbGrid);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "GameForm";
            this.Text = "Game of Life";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameForm_FormClosing);
            this.Load += new System.EventHandler(this.GameForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCellSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGameSpeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbGrid;
        private System.Windows.Forms.NumericUpDown nudCellSize;
        private System.Windows.Forms.Label labelCellSize;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonAdvancedGeneration;
        private System.Windows.Forms.Button buttonStartStop;
        private System.Windows.Forms.NumericUpDown nudGameSpeed;
        private System.Windows.Forms.Label labelGameSpeed;
        private System.Windows.Forms.Label labelGenerationCount;
        private System.Windows.Forms.Label labelLiveCellCount;
        private System.Windows.Forms.Label labelDeadCellCount;
    }
}

