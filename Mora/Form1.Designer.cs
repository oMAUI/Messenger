
namespace Mora
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pMsgBox = new System.Windows.Forms.FlowLayoutPanel();
            this.pMsgBoxUser = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tbMsgBox = new System.Windows.Forms.TextBox();
            this.UserCardBox = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pMsgBox
            // 
            this.pMsgBox.AutoScroll = true;
            this.pMsgBox.Location = new System.Drawing.Point(289, 126);
            this.pMsgBox.Name = "pMsgBox";
            this.pMsgBox.Size = new System.Drawing.Size(200, 100);
            this.pMsgBox.TabIndex = 8;
            this.pMsgBox.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.pMsgBox_Wheel);
            // 
            // pMsgBoxUser
            // 
            this.pMsgBoxUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pMsgBoxUser.AutoScroll = true;
            this.pMsgBoxUser.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.pMsgBoxUser.Location = new System.Drawing.Point(514, 126);
            this.pMsgBoxUser.Name = "pMsgBoxUser";
            this.pMsgBoxUser.Size = new System.Drawing.Size(200, 100);
            this.pMsgBoxUser.TabIndex = 9;
            this.pMsgBoxUser.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.pMsgBoxUser_Wheel);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(289, 232);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 100);
            this.panel1.TabIndex = 10;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(514, 232);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 100);
            this.panel2.TabIndex = 11;
            // 
            // tbMsgBox
            // 
            this.tbMsgBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbMsgBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbMsgBox.Location = new System.Drawing.Point(12, 378);
            this.tbMsgBox.Multiline = true;
            this.tbMsgBox.Name = "tbMsgBox";
            this.tbMsgBox.Size = new System.Drawing.Size(507, 60);
            this.tbMsgBox.TabIndex = 4;
            this.tbMsgBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbMsgBox_KeyUp);
            // 
            // UserCardBox
            // 
            this.UserCardBox.ForeColor = System.Drawing.Color.Transparent;
            this.UserCardBox.Location = new System.Drawing.Point(29, 126);
            this.UserCardBox.Name = "UserCardBox";
            this.UserCardBox.Size = new System.Drawing.Size(200, 100);
            this.UserCardBox.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.UserCardBox);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pMsgBoxUser);
            this.Controls.Add(this.pMsgBox);
            this.Controls.Add(this.tbMsgBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.FlowLayoutPanel pMsgBox;
        private System.Windows.Forms.FlowLayoutPanel pMsgBoxUser;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox tbMsgBox;
        private System.Windows.Forms.FlowLayoutPanel UserCardBox;
    }
}

