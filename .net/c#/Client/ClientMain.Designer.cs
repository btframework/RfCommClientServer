
namespace RfCommClient
{
    partial class fmClientMain
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
            this.btDiscover = new System.Windows.Forms.Button();
            this.lvDevices = new System.Windows.Forms.ListView();
            this.chAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btConnect = new System.Windows.Forms.Button();
            this.btDisconnect = new System.Windows.Forms.Button();
            this.lbLog = new System.Windows.Forms.ListBox();
            this.btClear = new System.Windows.Forms.Button();
            this.btSendByte = new System.Windows.Forms.Button();
            this.btSendUInt16 = new System.Windows.Forms.Button();
            this.btSendUInt32 = new System.Windows.Forms.Button();
            this.btSendUInt64 = new System.Windows.Forms.Button();
            this.btSendSByte = new System.Windows.Forms.Button();
            this.btSendInt16 = new System.Windows.Forms.Button();
            this.btSendInt32 = new System.Windows.Forms.Button();
            this.btSendInt64 = new System.Windows.Forms.Button();
            this.btSendArray = new System.Windows.Forms.Button();
            this.btGetByte = new System.Windows.Forms.Button();
            this.btGetUInt16 = new System.Windows.Forms.Button();
            this.btGetUInt32 = new System.Windows.Forms.Button();
            this.btGetUInt64 = new System.Windows.Forms.Button();
            this.btGetSByte = new System.Windows.Forms.Button();
            this.btGetInt16 = new System.Windows.Forms.Button();
            this.btGetInt32 = new System.Windows.Forms.Button();
            this.btGetInt64 = new System.Windows.Forms.Button();
            this.btGetArray = new System.Windows.Forms.Button();
            this.btSendString = new System.Windows.Forms.Button();
            this.btGetString = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btDiscover
            // 
            this.btDiscover.Location = new System.Drawing.Point(12, 12);
            this.btDiscover.Name = "btDiscover";
            this.btDiscover.Size = new System.Drawing.Size(75, 23);
            this.btDiscover.TabIndex = 0;
            this.btDiscover.Text = "Discover";
            this.btDiscover.UseVisualStyleBackColor = true;
            this.btDiscover.Click += new System.EventHandler(this.btDiscover_Click);
            // 
            // lvDevices
            // 
            this.lvDevices.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chAddress,
            this.chName});
            this.lvDevices.FullRowSelect = true;
            this.lvDevices.GridLines = true;
            this.lvDevices.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvDevices.HideSelection = false;
            this.lvDevices.Location = new System.Drawing.Point(12, 41);
            this.lvDevices.MultiSelect = false;
            this.lvDevices.Name = "lvDevices";
            this.lvDevices.Size = new System.Drawing.Size(332, 105);
            this.lvDevices.TabIndex = 1;
            this.lvDevices.UseCompatibleStateImageBehavior = false;
            this.lvDevices.View = System.Windows.Forms.View.Details;
            // 
            // chAddress
            // 
            this.chAddress.Text = "Address";
            this.chAddress.Width = 150;
            // 
            // chName
            // 
            this.chName.Text = "Name";
            this.chName.Width = 150;
            // 
            // btConnect
            // 
            this.btConnect.Location = new System.Drawing.Point(350, 41);
            this.btConnect.Name = "btConnect";
            this.btConnect.Size = new System.Drawing.Size(75, 23);
            this.btConnect.TabIndex = 2;
            this.btConnect.Text = "Connect";
            this.btConnect.UseVisualStyleBackColor = true;
            this.btConnect.Click += new System.EventHandler(this.btConnect_Click);
            // 
            // btDisconnect
            // 
            this.btDisconnect.Location = new System.Drawing.Point(350, 70);
            this.btDisconnect.Name = "btDisconnect";
            this.btDisconnect.Size = new System.Drawing.Size(75, 23);
            this.btDisconnect.TabIndex = 3;
            this.btDisconnect.Text = "Disconnect";
            this.btDisconnect.UseVisualStyleBackColor = true;
            this.btDisconnect.Click += new System.EventHandler(this.btDisconnect_Click);
            // 
            // lbLog
            // 
            this.lbLog.FormattingEnabled = true;
            this.lbLog.Location = new System.Drawing.Point(10, 316);
            this.lbLog.Name = "lbLog";
            this.lbLog.Size = new System.Drawing.Size(413, 173);
            this.lbLog.TabIndex = 4;
            // 
            // btClear
            // 
            this.btClear.Location = new System.Drawing.Point(350, 290);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(75, 23);
            this.btClear.TabIndex = 5;
            this.btClear.Text = "Clear";
            this.btClear.UseVisualStyleBackColor = true;
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // btSendByte
            // 
            this.btSendByte.Location = new System.Drawing.Point(12, 152);
            this.btSendByte.Name = "btSendByte";
            this.btSendByte.Size = new System.Drawing.Size(75, 23);
            this.btSendByte.TabIndex = 6;
            this.btSendByte.Text = "Send Byte";
            this.btSendByte.UseVisualStyleBackColor = true;
            this.btSendByte.Click += new System.EventHandler(this.btSendByte_Click);
            // 
            // btSendUInt16
            // 
            this.btSendUInt16.Location = new System.Drawing.Point(93, 152);
            this.btSendUInt16.Name = "btSendUInt16";
            this.btSendUInt16.Size = new System.Drawing.Size(75, 23);
            this.btSendUInt16.TabIndex = 7;
            this.btSendUInt16.Text = "Send UInt16";
            this.btSendUInt16.UseVisualStyleBackColor = true;
            this.btSendUInt16.Click += new System.EventHandler(this.btSendUInt16_Click);
            // 
            // btSendUInt32
            // 
            this.btSendUInt32.Location = new System.Drawing.Point(174, 152);
            this.btSendUInt32.Name = "btSendUInt32";
            this.btSendUInt32.Size = new System.Drawing.Size(75, 23);
            this.btSendUInt32.TabIndex = 8;
            this.btSendUInt32.Text = "Send UInt32";
            this.btSendUInt32.UseVisualStyleBackColor = true;
            this.btSendUInt32.Click += new System.EventHandler(this.btSendUInt32_Click);
            // 
            // btSendUInt64
            // 
            this.btSendUInt64.Location = new System.Drawing.Point(255, 152);
            this.btSendUInt64.Name = "btSendUInt64";
            this.btSendUInt64.Size = new System.Drawing.Size(75, 23);
            this.btSendUInt64.TabIndex = 9;
            this.btSendUInt64.Text = "Send UInt64";
            this.btSendUInt64.UseVisualStyleBackColor = true;
            this.btSendUInt64.Click += new System.EventHandler(this.btSendUInt64_Click);
            // 
            // btSendSByte
            // 
            this.btSendSByte.Location = new System.Drawing.Point(12, 181);
            this.btSendSByte.Name = "btSendSByte";
            this.btSendSByte.Size = new System.Drawing.Size(75, 23);
            this.btSendSByte.TabIndex = 10;
            this.btSendSByte.Text = "Send SByte";
            this.btSendSByte.UseVisualStyleBackColor = true;
            this.btSendSByte.Click += new System.EventHandler(this.btSendSByte_Click);
            // 
            // btSendInt16
            // 
            this.btSendInt16.Location = new System.Drawing.Point(93, 181);
            this.btSendInt16.Name = "btSendInt16";
            this.btSendInt16.Size = new System.Drawing.Size(75, 23);
            this.btSendInt16.TabIndex = 11;
            this.btSendInt16.Text = "Send Int16";
            this.btSendInt16.UseVisualStyleBackColor = true;
            this.btSendInt16.Click += new System.EventHandler(this.btSendInt16_Click);
            // 
            // btSendInt32
            // 
            this.btSendInt32.Location = new System.Drawing.Point(174, 181);
            this.btSendInt32.Name = "btSendInt32";
            this.btSendInt32.Size = new System.Drawing.Size(75, 23);
            this.btSendInt32.TabIndex = 12;
            this.btSendInt32.Text = "Send Int32";
            this.btSendInt32.UseVisualStyleBackColor = true;
            this.btSendInt32.Click += new System.EventHandler(this.btSendInt32_Click);
            // 
            // btSendInt64
            // 
            this.btSendInt64.Location = new System.Drawing.Point(255, 181);
            this.btSendInt64.Name = "btSendInt64";
            this.btSendInt64.Size = new System.Drawing.Size(75, 23);
            this.btSendInt64.TabIndex = 13;
            this.btSendInt64.Text = "Send Int64";
            this.btSendInt64.UseVisualStyleBackColor = true;
            this.btSendInt64.Click += new System.EventHandler(this.btSendInt64_Click);
            // 
            // btSendArray
            // 
            this.btSendArray.Location = new System.Drawing.Point(336, 152);
            this.btSendArray.Name = "btSendArray";
            this.btSendArray.Size = new System.Drawing.Size(75, 23);
            this.btSendArray.TabIndex = 14;
            this.btSendArray.Text = "Send Array";
            this.btSendArray.UseVisualStyleBackColor = true;
            this.btSendArray.Click += new System.EventHandler(this.btSendArray_Click);
            // 
            // btGetByte
            // 
            this.btGetByte.Location = new System.Drawing.Point(12, 226);
            this.btGetByte.Name = "btGetByte";
            this.btGetByte.Size = new System.Drawing.Size(75, 23);
            this.btGetByte.TabIndex = 15;
            this.btGetByte.Text = "Get Byte";
            this.btGetByte.UseVisualStyleBackColor = true;
            this.btGetByte.Click += new System.EventHandler(this.btGetByte_Click);
            // 
            // btGetUInt16
            // 
            this.btGetUInt16.Location = new System.Drawing.Point(93, 226);
            this.btGetUInt16.Name = "btGetUInt16";
            this.btGetUInt16.Size = new System.Drawing.Size(75, 23);
            this.btGetUInt16.TabIndex = 16;
            this.btGetUInt16.Text = "Get UInt16";
            this.btGetUInt16.UseVisualStyleBackColor = true;
            this.btGetUInt16.Click += new System.EventHandler(this.btGetUInt16_Click);
            // 
            // btGetUInt32
            // 
            this.btGetUInt32.Location = new System.Drawing.Point(174, 226);
            this.btGetUInt32.Name = "btGetUInt32";
            this.btGetUInt32.Size = new System.Drawing.Size(75, 23);
            this.btGetUInt32.TabIndex = 17;
            this.btGetUInt32.Text = "Get UInt32";
            this.btGetUInt32.UseVisualStyleBackColor = true;
            this.btGetUInt32.Click += new System.EventHandler(this.btGetUInt32_Click);
            // 
            // btGetUInt64
            // 
            this.btGetUInt64.Location = new System.Drawing.Point(255, 226);
            this.btGetUInt64.Name = "btGetUInt64";
            this.btGetUInt64.Size = new System.Drawing.Size(75, 23);
            this.btGetUInt64.TabIndex = 18;
            this.btGetUInt64.Text = "Get UInt64";
            this.btGetUInt64.UseVisualStyleBackColor = true;
            this.btGetUInt64.Click += new System.EventHandler(this.btGetUInt64_Click);
            // 
            // btGetSByte
            // 
            this.btGetSByte.Location = new System.Drawing.Point(10, 255);
            this.btGetSByte.Name = "btGetSByte";
            this.btGetSByte.Size = new System.Drawing.Size(75, 23);
            this.btGetSByte.TabIndex = 19;
            this.btGetSByte.Text = "Get SByte";
            this.btGetSByte.UseVisualStyleBackColor = true;
            this.btGetSByte.Click += new System.EventHandler(this.btGetSByte_Click);
            // 
            // btGetInt16
            // 
            this.btGetInt16.Location = new System.Drawing.Point(93, 255);
            this.btGetInt16.Name = "btGetInt16";
            this.btGetInt16.Size = new System.Drawing.Size(75, 23);
            this.btGetInt16.TabIndex = 20;
            this.btGetInt16.Text = "Get Int16";
            this.btGetInt16.UseVisualStyleBackColor = true;
            this.btGetInt16.Click += new System.EventHandler(this.btGetInt16_Click);
            // 
            // btGetInt32
            // 
            this.btGetInt32.Location = new System.Drawing.Point(174, 255);
            this.btGetInt32.Name = "btGetInt32";
            this.btGetInt32.Size = new System.Drawing.Size(75, 23);
            this.btGetInt32.TabIndex = 21;
            this.btGetInt32.Text = "Get Int32";
            this.btGetInt32.UseVisualStyleBackColor = true;
            this.btGetInt32.Click += new System.EventHandler(this.btGetInt32_Click);
            // 
            // btGetInt64
            // 
            this.btGetInt64.Location = new System.Drawing.Point(255, 255);
            this.btGetInt64.Name = "btGetInt64";
            this.btGetInt64.Size = new System.Drawing.Size(75, 23);
            this.btGetInt64.TabIndex = 22;
            this.btGetInt64.Text = "Get Int64";
            this.btGetInt64.UseVisualStyleBackColor = true;
            this.btGetInt64.Click += new System.EventHandler(this.btGetInt64_Click);
            // 
            // btGetArray
            // 
            this.btGetArray.Location = new System.Drawing.Point(336, 226);
            this.btGetArray.Name = "btGetArray";
            this.btGetArray.Size = new System.Drawing.Size(75, 23);
            this.btGetArray.TabIndex = 23;
            this.btGetArray.Text = "Get Array";
            this.btGetArray.UseVisualStyleBackColor = true;
            this.btGetArray.Click += new System.EventHandler(this.btGetArray_Click);
            // 
            // btSendString
            // 
            this.btSendString.Location = new System.Drawing.Point(336, 181);
            this.btSendString.Name = "btSendString";
            this.btSendString.Size = new System.Drawing.Size(75, 23);
            this.btSendString.TabIndex = 24;
            this.btSendString.Text = "Send String";
            this.btSendString.UseVisualStyleBackColor = true;
            this.btSendString.Click += new System.EventHandler(this.btSendString_Click);
            // 
            // btGetString
            // 
            this.btGetString.Location = new System.Drawing.Point(336, 255);
            this.btGetString.Name = "btGetString";
            this.btGetString.Size = new System.Drawing.Size(75, 23);
            this.btGetString.TabIndex = 25;
            this.btGetString.Text = "Get String";
            this.btGetString.UseVisualStyleBackColor = true;
            this.btGetString.Click += new System.EventHandler(this.btGetString_Click);
            // 
            // fmClientMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 501);
            this.Controls.Add(this.btGetString);
            this.Controls.Add(this.btSendString);
            this.Controls.Add(this.btGetArray);
            this.Controls.Add(this.btGetInt64);
            this.Controls.Add(this.btGetInt32);
            this.Controls.Add(this.btGetInt16);
            this.Controls.Add(this.btGetSByte);
            this.Controls.Add(this.btGetUInt64);
            this.Controls.Add(this.btGetUInt32);
            this.Controls.Add(this.btGetUInt16);
            this.Controls.Add(this.btGetByte);
            this.Controls.Add(this.btSendArray);
            this.Controls.Add(this.btSendInt64);
            this.Controls.Add(this.btSendInt32);
            this.Controls.Add(this.btSendInt16);
            this.Controls.Add(this.btSendSByte);
            this.Controls.Add(this.btSendUInt64);
            this.Controls.Add(this.btSendUInt32);
            this.Controls.Add(this.btSendUInt16);
            this.Controls.Add(this.btSendByte);
            this.Controls.Add(this.btClear);
            this.Controls.Add(this.lbLog);
            this.Controls.Add(this.btDisconnect);
            this.Controls.Add(this.btConnect);
            this.Controls.Add(this.lvDevices);
            this.Controls.Add(this.btDiscover);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "fmClientMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RFCOMM Client Demo Application";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.fmClientMain_FormClosed);
            this.Load += new System.EventHandler(this.fmClientMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btDiscover;
        private System.Windows.Forms.ListView lvDevices;
        private System.Windows.Forms.ColumnHeader chAddress;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.Button btConnect;
        private System.Windows.Forms.Button btDisconnect;
        private System.Windows.Forms.ListBox lbLog;
        private System.Windows.Forms.Button btClear;
        private System.Windows.Forms.Button btSendByte;
        private System.Windows.Forms.Button btSendUInt16;
        private System.Windows.Forms.Button btSendUInt32;
        private System.Windows.Forms.Button btSendUInt64;
        private System.Windows.Forms.Button btSendSByte;
        private System.Windows.Forms.Button btSendInt16;
        private System.Windows.Forms.Button btSendInt32;
        private System.Windows.Forms.Button btSendInt64;
        private System.Windows.Forms.Button btSendArray;
        private System.Windows.Forms.Button btGetByte;
        private System.Windows.Forms.Button btGetUInt16;
        private System.Windows.Forms.Button btGetUInt32;
        private System.Windows.Forms.Button btGetUInt64;
        private System.Windows.Forms.Button btGetSByte;
        private System.Windows.Forms.Button btGetInt16;
        private System.Windows.Forms.Button btGetInt32;
        private System.Windows.Forms.Button btGetInt64;
        private System.Windows.Forms.Button btGetArray;
        private System.Windows.Forms.Button btSendString;
        private System.Windows.Forms.Button btGetString;
    }
}

