namespace AmazonPriceChecker
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.btnSaveLink = new System.Windows.Forms.Button();
            this.btnGetPrice = new System.Windows.Forms.Button();
            this.lstLinks = new System.Windows.Forms.ListBox();
            this.lblResult = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(12, 12);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(300, 20);
            this.txtUrl.TabIndex = 0;

            // 
            // btnSaveLink
            // 
            this.btnSaveLink.Location = new System.Drawing.Point(330, 10);
            this.btnSaveLink.Name = "btnSaveLink";
            this.btnSaveLink.Size = new System.Drawing.Size(100, 23);
            this.btnSaveLink.TabIndex = 1;
            this.btnSaveLink.Text = "Guardar Enlace";
            this.btnSaveLink.UseVisualStyleBackColor = true;
            this.btnSaveLink.Click += new System.EventHandler(this.btnSaveLink_Click);

            // 
            // btnGetPrice
            // 
            this.btnGetPrice.Location = new System.Drawing.Point(12, 220);
            this.btnGetPrice.Name = "btnGetPrice";
            this.btnGetPrice.Size = new System.Drawing.Size(100, 23);
            this.btnGetPrice.TabIndex = 2;
            this.btnGetPrice.Text = "Obtener Precio";
            this.btnGetPrice.UseVisualStyleBackColor = true;
            this.btnGetPrice.Click += new System.EventHandler(this.btnGetPrice_Click);

            // 
            // lstLinks
            // 
            this.lstLinks.FormattingEnabled = true;
            this.lstLinks.Location = new System.Drawing.Point(12, 50);
            this.lstLinks.Name = "lstLinks";
            this.lstLinks.Size = new System.Drawing.Size(418, 147);
            this.lstLinks.TabIndex = 3;

            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(12, 260);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(55, 13);
            this.lblResult.TabIndex = 4;
            this.lblResult.Text = "Resultado";

            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(450, 300);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.lstLinks);
            this.Controls.Add(this.btnGetPrice);
            this.Controls.Add(this.btnSaveLink);
            this.Controls.Add(this.txtUrl);
            this.Name = "MainForm";
            this.Text = "Amazon Price Checker";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button btnSaveLink;
        private System.Windows.Forms.Button btnGetPrice;
        private System.Windows.Forms.ListBox lstLinks;
        private System.Windows.Forms.Label lblResult;
    }
}
