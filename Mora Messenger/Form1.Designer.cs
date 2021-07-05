
namespace Mora_Messenger
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
            this.tbMsgBox = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.UserCardBox = new System.Windows.Forms.FlowLayoutPanel();
            this.pMsgBox = new System.Windows.Forms.FlowLayoutPanel();
            this.pMsgBoxUser = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // tbMsgBox
            // 
            this.tbMsgBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbMsgBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbMsgBox.Location = new System.Drawing.Point(12, 12);
            this.tbMsgBox.Name = "tbMsgBox";
            this.tbMsgBox.Size = new System.Drawing.Size(100, 38);
            this.tbMsgBox.TabIndex = 0;
            this.tbMsgBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbMsgBox_KeyUp);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // UserCardBox
            // 
            this.UserCardBox.AutoScroll = true;
            this.UserCardBox.Location = new System.Drawing.Point(12, 173);
            this.UserCardBox.Name = "UserCardBox";
            this.UserCardBox.Size = new System.Drawing.Size(200, 100);
            this.UserCardBox.TabIndex = 2;
            // 
            // pMsgBox
            // 
            this.pMsgBox.AutoScroll = true;
            this.pMsgBox.Location = new System.Drawing.Point(353, 173);
            this.pMsgBox.Name = "pMsgBox";
            this.pMsgBox.Size = new System.Drawing.Size(200, 100);
            this.pMsgBox.TabIndex = 5;
            this.pMsgBox.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.pMsgBox_Wheel);
            // 
            // pMsgBoxUser
            // 
            this.pMsgBoxUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pMsgBoxUser.AutoScroll = true;
            this.pMsgBoxUser.AutoScrollMargin = new System.Drawing.Size(50, 0);
            this.pMsgBoxUser.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.pMsgBoxUser.Location = new System.Drawing.Point(594, 173);
            this.pMsgBoxUser.Name = "pMsgBoxUser";
            this.pMsgBoxUser.Size = new System.Drawing.Size(194, 100);
            this.pMsgBoxUser.TabIndex = 6;
            this.pMsgBoxUser.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.pMsgBoxUser_Wheel);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(353, 49);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 100);
            this.panel1.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(594, 49);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 100);
            this.panel2.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.UserCardBox);
            this.Controls.Add(this.tbMsgBox);
            this.Controls.Add(this.pMsgBox);
            this.Controls.Add(this.pMsgBoxUser);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Scroll += new System.Windows.Forms.ScrollEventHandler(this.Form1_Scroll);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbMsgBox;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.FlowLayoutPanel UserCardBox;
        private System.Windows.Forms.FlowLayoutPanel pMsgBox;
        private System.Windows.Forms.FlowLayoutPanel pMsgBoxUser;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}

