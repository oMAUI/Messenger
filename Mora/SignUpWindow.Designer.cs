
using Mora.Designs.Button;
using Mora.Designs.UIcontrols;
using System.Windows.Forms;

namespace Mora
{
    partial class SignUpWindow
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
            this.SuspendLayout();
            //
            //lbCheck
            //
            lbCheck = new Label();
            lbCheck.AutoSize = true;
            lbCheck.Font = new System.Drawing.Font("Arial", 11.25F);
            //
            // tb
            //
            tbLogin = new RegisterTextBox();
            tbPass = new RegisterTextBox();
            //
            // btnSignUp
            //
            btnSignUp = new RegisterButton();
            btnSignUp.Click += new System.EventHandler(this.btnSignUp_Click);
            //
            // btnReg
            //
            btnReg = new RegisterButton();
            btnReg.Click += new System.EventHandler(this.btnReg_Click);
            // 
            // SignUpWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 517);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SignUpWindow";
            this.Text = "SignUpWindow";
            this.Load += new System.EventHandler(this.SignUpWindow_Load);
            this.ResumeLayout(false);

            

        }
        Label lbCheck;

        RegisterTextBox tbLogin;
        RegisterTextBox tbPass;

        RegisterButton btnReg;
        RegisterButton btnSignUp;

        #endregion
    }
}