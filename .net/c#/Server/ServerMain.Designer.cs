
namespace RfCommServer
{
    partial class fmServerMain
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
            this.btListen = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.lbLog = new System.Windows.Forms.ListBox();
            this.btClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btListen
            // 
            this.btListen.Location = new System.Drawing.Point(12, 12);
            this.btListen.Name = "btListen";
            this.btListen.Size = new System.Drawing.Size(75, 23);
            this.btListen.TabIndex = 0;
            this.btListen.Text = "Listen";
            this.btListen.UseVisualStyleBackColor = true;
            this.btListen.Click += new System.EventHandler(this.btListen_Click);
            // 
            // btClose
            // 
            this.btClose.Location = new System.Drawing.Point(93, 12);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(75, 23);
            this.btClose.TabIndex = 1;
            this.btClose.Text = "Close";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // lbLog
            // 
            this.lbLog.FormattingEnabled = true;
            this.lbLog.Location = new System.Drawing.Point(12, 41);
            this.lbLog.Name = "lbLog";
            this.lbLog.Size = new System.Drawing.Size(589, 394);
            this.lbLog.TabIndex = 2;
            // 
            // btClear
            // 
            this.btClear.Location = new System.Drawing.Point(526, 12);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(75, 23);
            this.btClear.TabIndex = 3;
            this.btClear.Text = "Clear";
            this.btClear.UseVisualStyleBackColor = true;
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // fmServerMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 450);
            this.Controls.Add(this.btClear);
            this.Controls.Add(this.lbLog);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.btListen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "fmServerMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RFCOMM Server Demo Application";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.fmServerMain_FormClosed);
            this.Load += new System.EventHandler(this.fmServerMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btListen;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.ListBox lbLog;
        private System.Windows.Forms.Button btClear;
    }
}

