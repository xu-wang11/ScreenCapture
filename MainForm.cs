using System;
using System.Windows.Forms;
using gma.System.Windows;
using System.Drawing;
using System.Drawing.Imaging;
namespace GlobalHookDemo 
{
	class MainForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button buttonStart;
        private TextBox textBox1;
        private Button button1;
        private FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button buttonStop;
        private int count;
		public MainForm()
		{
			InitializeComponent();
            count = 0;
		}
	
		// THIS METHOD IS MAINTAINED BY THE FORM DESIGNER
		// DO NOT EDIT IT MANUALLY! YOUR CHANGES ARE LIKELY TO BE LOST
		void InitializeComponent() {
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // buttonStop
            // 
            this.buttonStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStop.Location = new System.Drawing.Point(190, 29);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(90, 25);
            this.buttonStop.TabIndex = 1;
            this.buttonStop.Text = "关闭";
            this.buttonStop.Click += new System.EventHandler(this.ButtonStopClick);
            // 
            // buttonStart
            // 
            this.buttonStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStart.Location = new System.Drawing.Point(43, 29);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(90, 25);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "开启";
            this.buttonStart.Click += new System.EventHandler(this.ButtonStartClick);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(43, 84);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(237, 21);
            this.textBox1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(190, 123);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 29);
            this.button1.TabIndex = 3;
            this.button1.Text = "选择文件夹";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(328, 164);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.Name = "MainForm";
            this.Text = "ScreenCapture";
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
			
		[STAThread]
		public static void Main(string[] args)
		{
			Application.Run(new MainForm());
		}
		
		void ButtonStartClick(object sender, System.EventArgs e)
		{
			actHook.Start();
		}
		
		void ButtonStopClick(object sender, System.EventArgs e)
		{
			actHook.Stop();
		}
		
		
		UserActivityHook actHook;
		void MainFormLoad(object sender, System.EventArgs e)
		{
            actHook = new UserActivityHook(); // crate an instance with global hooks
			// hang on events
			//actHook.OnMouseActivity+=new MouseEventHandler(MouseMoved);
			actHook.KeyDown+=new KeyEventHandler(MyKeyDown);
			//actHook.KeyPress+=new KeyPressEventHandler(MyKeyPress);
			//actHook.KeyUp+=new KeyEventHandler(MyKeyUp);
		}
		
		
		
		public void MyKeyDown(object sender, KeyEventArgs e)
		{
            if (e.KeyCode == Keys.F8)
            {
                Bitmap printscreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

                Graphics graphics = Graphics.FromImage(printscreen as Image);

                graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size, CopyPixelOperation.SourceCopy);
                Image newImage = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                Graphics draw = Graphics.FromImage(newImage);
                draw.DrawImage(printscreen, 0, 0);
                printscreen.Dispose();
                if (this.textBox1.Text == "")
                {
                    MessageBox.Show("You should select a directory first.");

                }
                else
                {
                    try
                    {
                        newImage.Save(this.textBox1.Text + "\\" + count + ".jpg", ImageFormat.Jpeg);
                        count++;
                    }
                    catch (Exception _e)
                    {
                        MessageBox.Show("error happened. Maybe you should delete files in the selected directory");
                    }
                }

            }
		}

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
             
                this.textBox1.Text = folderBrowserDialog1.SelectedPath;
               // this.textBox1.Update();
            }
        }
		
		/**public void MyKeyPress(object sender, KeyPressEventArgs e)
		{
		
		}
		
		public void MyKeyUp(object sender, KeyEventArgs e)
		{
			LogWrite("KeyUp 		- " + e.KeyData.ToString());
		}
		
		private void LogWrite(string txt)
		{
			textBox.AppendText(txt + Environment.NewLine);
			textBox.SelectionStart = textBox.Text.Length;
		}*/

	}			
}
