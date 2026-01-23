namespace docker_quick_manager
{
    partial class Setup_Form
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            SelectedContainerTextbox = new TextBox();
            StartButton = new Button();
            StopButton = new Button();
            dataGridView1 = new DataGridView();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            CreateButtton = new Button();
            FolderSelectButton = new Button();
            TragetDirTextBox = new TextBox();
            label5 = new Label();
            NameTextBox = new TextBox();
            label4 = new Label();
            ImageComboBox = new ComboBox();
            label3 = new Label();
            tabPage2 = new TabPage();
            ARSelectedContainterTextBox = new TextBox();
            label2 = new Label();
            dataGridView2 = new DataGridView();
            RemoveButton = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 27);
            label1.Name = "label1";
            label1.Size = new Size(51, 15);
            label1.TabIndex = 1;
            label1.Text = "Selected";
            // 
            // SelectedContainerTextbox
            // 
            SelectedContainerTextbox.Location = new Point(62, 22);
            SelectedContainerTextbox.Name = "SelectedContainerTextbox";
            SelectedContainerTextbox.ReadOnly = true;
            SelectedContainerTextbox.Size = new Size(339, 23);
            SelectedContainerTextbox.TabIndex = 2;
            // 
            // StartButton
            // 
            StartButton.Location = new Point(407, 166);
            StartButton.Name = "StartButton";
            StartButton.Size = new Size(75, 23);
            StartButton.TabIndex = 3;
            StartButton.Text = "Start";
            StartButton.UseVisualStyleBackColor = true;
            StartButton.Click += StartButton_Click;
            // 
            // StopButton
            // 
            StopButton.Location = new Point(407, 195);
            StopButton.Name = "StopButton";
            StopButton.Size = new Size(75, 23);
            StopButton.TabIndex = 4;
            StopButton.Text = "Stop";
            StopButton.UseVisualStyleBackColor = true;
            StopButton.Click += StopButton_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 51);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(389, 167);
            dataGridView1.TabIndex = 5;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(SelectedContainerTextbox);
            groupBox1.Controls.Add(StopButton);
            groupBox1.Controls.Add(dataGridView1);
            groupBox1.Controls.Add(StartButton);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(488, 224);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            groupBox1.Text = "Containers";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(tabControl1);
            groupBox2.Location = new Point(12, 242);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(488, 204);
            groupBox2.TabIndex = 7;
            groupBox2.TabStop = false;
            groupBox2.Text = "Add or Remove";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(3, 19);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(482, 182);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(CreateButtton);
            tabPage1.Controls.Add(FolderSelectButton);
            tabPage1.Controls.Add(TragetDirTextBox);
            tabPage1.Controls.Add(label5);
            tabPage1.Controls.Add(NameTextBox);
            tabPage1.Controls.Add(label4);
            tabPage1.Controls.Add(ImageComboBox);
            tabPage1.Controls.Add(label3);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(474, 154);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Add";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // CreateButtton
            // 
            CreateButtton.Location = new Point(391, 114);
            CreateButtton.Name = "CreateButtton";
            CreateButtton.Size = new Size(75, 23);
            CreateButtton.TabIndex = 6;
            CreateButtton.Text = "Create";
            CreateButtton.UseVisualStyleBackColor = true;
            // 
            // FolderSelectButton
            // 
            FolderSelectButton.Location = new Point(383, 75);
            FolderSelectButton.Name = "FolderSelectButton";
            FolderSelectButton.Size = new Size(25, 23);
            FolderSelectButton.TabIndex = 7;
            FolderSelectButton.Text = "...";
            FolderSelectButton.UseVisualStyleBackColor = true;
            // 
            // TragetDirTextBox
            // 
            TragetDirTextBox.Location = new Point(81, 74);
            TragetDirTextBox.Name = "TragetDirTextBox";
            TragetDirTextBox.Size = new Size(299, 23);
            TragetDirTextBox.TabIndex = 6;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(18, 77);
            label5.Name = "label5";
            label5.Size = new Size(57, 15);
            label5.TabIndex = 5;
            label5.Text = "Target Dir";
            // 
            // NameTextBox
            // 
            NameTextBox.Location = new Point(81, 45);
            NameTextBox.Name = "NameTextBox";
            NameTextBox.Size = new Size(299, 23);
            NameTextBox.TabIndex = 4;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(18, 48);
            label4.Name = "label4";
            label4.Size = new Size(39, 15);
            label4.TabIndex = 3;
            label4.Text = "Name";
            // 
            // ImageComboBox
            // 
            ImageComboBox.FormattingEnabled = true;
            ImageComboBox.Location = new Point(81, 16);
            ImageComboBox.Name = "ImageComboBox";
            ImageComboBox.Size = new Size(299, 23);
            ImageComboBox.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(18, 19);
            label3.Name = "label3";
            label3.Size = new Size(40, 15);
            label3.TabIndex = 0;
            label3.Text = "Image";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(ARSelectedContainterTextBox);
            tabPage2.Controls.Add(label2);
            tabPage2.Controls.Add(dataGridView2);
            tabPage2.Controls.Add(RemoveButton);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(474, 154);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Remove";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // ARSelectedContainterTextBox
            // 
            ARSelectedContainterTextBox.Location = new Point(62, 13);
            ARSelectedContainterTextBox.Name = "ARSelectedContainterTextBox";
            ARSelectedContainterTextBox.ReadOnly = true;
            ARSelectedContainterTextBox.Size = new Size(325, 23);
            ARSelectedContainterTextBox.TabIndex = 7;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 18);
            label2.Name = "label2";
            label2.Size = new Size(51, 15);
            label2.TabIndex = 6;
            label2.Text = "Selected";
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(12, 42);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.Size = new Size(375, 100);
            dataGridView2.TabIndex = 10;
            // 
            // RemoveButton
            // 
            RemoveButton.Location = new Point(393, 119);
            RemoveButton.Name = "RemoveButton";
            RemoveButton.Size = new Size(75, 23);
            RemoveButton.TabIndex = 8;
            RemoveButton.Text = "Remove";
            RemoveButton.UseVisualStyleBackColor = true;
            // 
            // Setup_Form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(512, 458);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            MaximumSize = new Size(528, 497);
            MinimumSize = new Size(528, 497);
            Name = "Setup_Form";
            Text = "Setup";
            Shown += Setup_Form_Shown;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Label label1;
        private TextBox SelectedContainerTextbox;
        private Button StartButton;
        private Button StopButton;
        private DataGridView dataGridView1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private ComboBox ImageComboBox;
        private Label label3;
        private TabPage tabPage2;
        private TextBox ARSelectedContainterTextBox;
        private Label label2;
        private DataGridView dataGridView2;
        private Button RemoveButton;
        private Button FolderSelectButton;
        private TextBox TragetDirTextBox;
        private Label label5;
        private TextBox NameTextBox;
        private Label label4;
        private Button CreateButtton;
    }
}
