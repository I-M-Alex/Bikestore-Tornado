namespace bikestoreTornado
{
    partial class bikestorTornado
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(bikestorTornado));
            mainPanel = new Panel();
            documentIconPanel = new Panel();
            toDatePicker = new DateTimePicker();
            fromDatePicker = new DateTimePicker();
            viewOrdersButton = new Button();
            clearDataGridButton = new Button();
            loadOrdPerCustButton = new Button();
            AddNewCustomerButton = new Button();
            orderDataGridView = new DataGridView();
            OrderDate = new DataGridViewTextBoxColumn();
            CustomerName = new DataGridViewTextBoxColumn();
            BikeID = new DataGridViewTextBoxColumn();
            UnitPrice = new DataGridViewTextBoxColumn();
            Quantity = new DataGridViewTextBoxColumn();
            TotalPrice = new DataGridViewTextBoxColumn();
            saleButton = new Button();
            clearButton = new Button();
            quantityComboBox = new ComboBox();
            quantityLabel = new Label();
            emailComboBox = new ComboBox();
            phoneComboBox = new ComboBox();
            fullNameComboBox = new ComboBox();
            madeInComboBox = new ComboBox();
            bikeSizeComboBox = new ComboBox();
            bikeColorCombobox = new ComboBox();
            bikeTypeComboBox = new ComboBox();
            madeInLable = new Label();
            EmailLabel = new Label();
            bikeTypeLable = new Label();
            bikeColorLabel = new Label();
            fullNameLabel = new Label();
            bikeSizeLabel = new Label();
            phoneLabel = new Label();
            customerIdComboBox = new ComboBox();
            customerIdLabel = new Label();
            customerImagePanel = new Panel();
            bikeImagePanel = new Panel();
            mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)orderDataGridView).BeginInit();
            SuspendLayout();
            // 
            // mainPanel
            // 
            mainPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mainPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            mainPanel.BackgroundImage = Properties.Resources.mainPanel;
            mainPanel.BackgroundImageLayout = ImageLayout.Stretch;
            mainPanel.Controls.Add(documentIconPanel);
            mainPanel.Controls.Add(toDatePicker);
            mainPanel.Controls.Add(fromDatePicker);
            mainPanel.Controls.Add(viewOrdersButton);
            mainPanel.Controls.Add(clearDataGridButton);
            mainPanel.Controls.Add(loadOrdPerCustButton);
            mainPanel.Controls.Add(AddNewCustomerButton);
            mainPanel.Controls.Add(orderDataGridView);
            mainPanel.Controls.Add(saleButton);
            mainPanel.Controls.Add(clearButton);
            mainPanel.Controls.Add(quantityComboBox);
            mainPanel.Controls.Add(quantityLabel);
            mainPanel.Controls.Add(emailComboBox);
            mainPanel.Controls.Add(phoneComboBox);
            mainPanel.Controls.Add(fullNameComboBox);
            mainPanel.Controls.Add(madeInComboBox);
            mainPanel.Controls.Add(bikeSizeComboBox);
            mainPanel.Controls.Add(bikeColorCombobox);
            mainPanel.Controls.Add(bikeTypeComboBox);
            mainPanel.Controls.Add(madeInLable);
            mainPanel.Controls.Add(EmailLabel);
            mainPanel.Controls.Add(bikeTypeLable);
            mainPanel.Controls.Add(bikeColorLabel);
            mainPanel.Controls.Add(fullNameLabel);
            mainPanel.Controls.Add(bikeSizeLabel);
            mainPanel.Controls.Add(phoneLabel);
            mainPanel.Controls.Add(customerIdComboBox);
            mainPanel.Controls.Add(customerIdLabel);
            mainPanel.Controls.Add(customerImagePanel);
            mainPanel.Controls.Add(bikeImagePanel);
            mainPanel.Location = new Point(29, 26);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(1458, 669);
            mainPanel.TabIndex = 0;
            // 
            // documentIconPanel
            // 
            documentIconPanel.BackgroundImage = Properties.Resources.documentediting_editdocuments_text_documentedi_2820;
            documentIconPanel.BackgroundImageLayout = ImageLayout.Stretch;
            documentIconPanel.Location = new Point(44, 345);
            documentIconPanel.Name = "documentIconPanel";
            documentIconPanel.Size = new Size(78, 112);
            documentIconPanel.TabIndex = 38;
            // 
            // toDatePicker
            // 
            toDatePicker.Location = new Point(1152, 454);
            toDatePicker.Name = "toDatePicker";
            toDatePicker.Size = new Size(216, 31);
            toDatePicker.TabIndex = 37;
            // 
            // fromDatePicker
            // 
            fromDatePicker.Location = new Point(1152, 426);
            fromDatePicker.Name = "fromDatePicker";
            fromDatePicker.Size = new Size(217, 31);
            fromDatePicker.TabIndex = 36;
            // 
            // viewOrdersButton
            // 
            viewOrdersButton.Cursor = Cursors.Hand;
            viewOrdersButton.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            viewOrdersButton.Location = new Point(1152, 390);
            viewOrdersButton.Name = "viewOrdersButton";
            viewOrdersButton.Size = new Size(216, 39);
            viewOrdersButton.TabIndex = 35;
            viewOrdersButton.Text = "View Orders";
            viewOrdersButton.UseVisualStyleBackColor = true;
            viewOrdersButton.Click += viewOrdersButton_Click;
            // 
            // clearDataGridButton
            // 
            clearDataGridButton.BackColor = Color.IndianRed;
            clearDataGridButton.Cursor = Cursors.Hand;
            clearDataGridButton.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
            clearDataGridButton.Location = new Point(177, 300);
            clearDataGridButton.Name = "clearDataGridButton";
            clearDataGridButton.Size = new Size(61, 56);
            clearDataGridButton.TabIndex = 34;
            clearDataGridButton.Text = "X";
            clearDataGridButton.UseVisualStyleBackColor = false;
            clearDataGridButton.Click += clearDataGridButton_Click;
            // 
            // loadOrdPerCustButton
            // 
            loadOrdPerCustButton.Cursor = Cursors.Hand;
            loadOrdPerCustButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            loadOrdPerCustButton.Location = new Point(1152, 345);
            loadOrdPerCustButton.Name = "loadOrdPerCustButton";
            loadOrdPerCustButton.Size = new Size(216, 39);
            loadOrdPerCustButton.TabIndex = 33;
            loadOrdPerCustButton.Text = "Orders per Customer";
            loadOrdPerCustButton.UseVisualStyleBackColor = true;
            loadOrdPerCustButton.Click += loadOrdPerCustButton_Click;
            // 
            // AddNewCustomerButton
            // 
            AddNewCustomerButton.Cursor = Cursors.Hand;
            AddNewCustomerButton.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            AddNewCustomerButton.Location = new Point(1152, 300);
            AddNewCustomerButton.Name = "AddNewCustomerButton";
            AddNewCustomerButton.Size = new Size(216, 39);
            AddNewCustomerButton.TabIndex = 32;
            AddNewCustomerButton.Text = "Create Customer";
            AddNewCustomerButton.UseVisualStyleBackColor = true;
            AddNewCustomerButton.Click += AddNewCustomerButton_Click;
            // 
            // orderDataGridView
            // 
            orderDataGridView.AllowUserToOrderColumns = true;
            orderDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            orderDataGridView.Columns.AddRange(new DataGridViewColumn[] { OrderDate, CustomerName, BikeID, UnitPrice, Quantity, TotalPrice });
            orderDataGridView.Location = new Point(177, 300);
            orderDataGridView.Name = "orderDataGridView";
            orderDataGridView.RowHeadersWidth = 62;
            orderDataGridView.Size = new Size(942, 185);
            orderDataGridView.TabIndex = 31;
            // 
            // OrderDate
            // 
            OrderDate.HeaderText = "Order Date";
            OrderDate.MinimumWidth = 8;
            OrderDate.Name = "OrderDate";
            OrderDate.ReadOnly = true;
            OrderDate.Width = 130;
            // 
            // CustomerName
            // 
            CustomerName.HeaderText = "Customer Name";
            CustomerName.MinimumWidth = 8;
            CustomerName.Name = "CustomerName";
            CustomerName.ReadOnly = true;
            CustomerName.Width = 150;
            // 
            // BikeID
            // 
            BikeID.HeaderText = "Bike ID";
            BikeID.MinimumWidth = 8;
            BikeID.Name = "BikeID";
            BikeID.ReadOnly = true;
            BikeID.Width = 150;
            // 
            // UnitPrice
            // 
            UnitPrice.HeaderText = " Unit Price";
            UnitPrice.MinimumWidth = 8;
            UnitPrice.Name = "UnitPrice";
            UnitPrice.ReadOnly = true;
            UnitPrice.Width = 150;
            // 
            // Quantity
            // 
            Quantity.HeaderText = "Quantity";
            Quantity.MinimumWidth = 8;
            Quantity.Name = "Quantity";
            Quantity.ReadOnly = true;
            Quantity.Width = 150;
            // 
            // TotalPrice
            // 
            TotalPrice.HeaderText = "Total Price";
            TotalPrice.MinimumWidth = 8;
            TotalPrice.Name = "TotalPrice";
            TotalPrice.ReadOnly = true;
            TotalPrice.Width = 150;
            // 
            // saleButton
            // 
            saleButton.Cursor = Cursors.Hand;
            saleButton.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            saleButton.Location = new Point(1152, 213);
            saleButton.Name = "saleButton";
            saleButton.Size = new Size(217, 39);
            saleButton.TabIndex = 30;
            saleButton.Text = "Sale";
            saleButton.UseVisualStyleBackColor = true;
            saleButton.Click += saleButton_Click;
            // 
            // clearButton
            // 
            clearButton.Cursor = Cursors.Hand;
            clearButton.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            clearButton.Location = new Point(1152, 173);
            clearButton.Name = "clearButton";
            clearButton.Size = new Size(216, 39);
            clearButton.TabIndex = 29;
            clearButton.Text = "Clear";
            clearButton.UseVisualStyleBackColor = true;
            clearButton.Click += clearButton_Click;
            // 
            // quantityComboBox
            // 
            quantityComboBox.Cursor = Cursors.Hand;
            quantityComboBox.FormattingEnabled = true;
            quantityComboBox.Location = new Point(1152, 91);
            quantityComboBox.Name = "quantityComboBox";
            quantityComboBox.Size = new Size(217, 33);
            quantityComboBox.TabIndex = 28;
            // 
            // quantityLabel
            // 
            quantityLabel.BackColor = SystemColors.Control;
            quantityLabel.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            quantityLabel.Location = new Point(1152, 46);
            quantityLabel.Name = "quantityLabel";
            quantityLabel.Size = new Size(216, 78);
            quantityLabel.TabIndex = 27;
            quantityLabel.Text = "Quantity";
            quantityLabel.TextAlign = ContentAlignment.TopCenter;
            quantityLabel.UseCompatibleTextRendering = true;
            // 
            // emailComboBox
            // 
            emailComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            emailComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            emailComboBox.Cursor = Cursors.Hand;
            emailComboBox.FormattingEnabled = true;
            emailComboBox.Location = new Point(909, 218);
            emailComboBox.Name = "emailComboBox";
            emailComboBox.Size = new Size(210, 33);
            emailComboBox.TabIndex = 11;
            emailComboBox.SelectedIndexChanged += emailComboBox_SelectedIndexChanged;
            // 
            // phoneComboBox
            // 
            phoneComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            phoneComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            phoneComboBox.Cursor = Cursors.Hand;
            phoneComboBox.FormattingEnabled = true;
            phoneComboBox.Location = new Point(663, 218);
            phoneComboBox.Name = "phoneComboBox";
            phoneComboBox.Size = new Size(210, 33);
            phoneComboBox.TabIndex = 15;
            phoneComboBox.SelectedIndexChanged += phoneComboBox_SelectedIndexChanged;
            // 
            // fullNameComboBox
            // 
            fullNameComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            fullNameComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            fullNameComboBox.Cursor = Cursors.Hand;
            fullNameComboBox.FormattingEnabled = true;
            fullNameComboBox.Location = new Point(414, 218);
            fullNameComboBox.Name = "fullNameComboBox";
            fullNameComboBox.Size = new Size(210, 33);
            fullNameComboBox.TabIndex = 16;
            fullNameComboBox.SelectedIndexChanged += fullNameComboBox_SelectedIndexChanged;
            // 
            // madeInComboBox
            // 
            madeInComboBox.Cursor = Cursors.Hand;
            madeInComboBox.FormattingEnabled = true;
            madeInComboBox.Location = new Point(909, 91);
            madeInComboBox.Name = "madeInComboBox";
            madeInComboBox.Size = new Size(210, 33);
            madeInComboBox.TabIndex = 12;
            madeInComboBox.SelectedIndexChanged += madeInComboBox_SelectedIndexChanged;
            // 
            // bikeSizeComboBox
            // 
            bikeSizeComboBox.Cursor = Cursors.Hand;
            bikeSizeComboBox.FormattingEnabled = true;
            bikeSizeComboBox.Location = new Point(663, 91);
            bikeSizeComboBox.Name = "bikeSizeComboBox";
            bikeSizeComboBox.Size = new Size(210, 33);
            bikeSizeComboBox.TabIndex = 18;
            bikeSizeComboBox.SelectedIndexChanged += bikeSizeComboBox_SelectedIndexChanged;
            // 
            // bikeColorCombobox
            // 
            bikeColorCombobox.Cursor = Cursors.Hand;
            bikeColorCombobox.FormattingEnabled = true;
            bikeColorCombobox.Location = new Point(414, 91);
            bikeColorCombobox.Name = "bikeColorCombobox";
            bikeColorCombobox.Size = new Size(210, 33);
            bikeColorCombobox.TabIndex = 13;
            bikeColorCombobox.SelectedIndexChanged += bikeColorCombobox_SelectedIndexChanged;
            // 
            // bikeTypeComboBox
            // 
            bikeTypeComboBox.Cursor = Cursors.Hand;
            bikeTypeComboBox.FormattingEnabled = true;
            bikeTypeComboBox.Location = new Point(163, 91);
            bikeTypeComboBox.Name = "bikeTypeComboBox";
            bikeTypeComboBox.Size = new Size(210, 33);
            bikeTypeComboBox.TabIndex = 17;
            bikeTypeComboBox.SelectedIndexChanged += bikeTypeComboBox_SelectedIndexChanged;
            // 
            // madeInLable
            // 
            madeInLable.BackColor = SystemColors.Control;
            madeInLable.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            madeInLable.Location = new Point(909, 46);
            madeInLable.Name = "madeInLable";
            madeInLable.Size = new Size(210, 78);
            madeInLable.TabIndex = 26;
            madeInLable.Text = "Made In";
            madeInLable.TextAlign = ContentAlignment.TopCenter;
            madeInLable.UseCompatibleTextRendering = true;
            // 
            // EmailLabel
            // 
            EmailLabel.BackColor = SystemColors.Control;
            EmailLabel.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            EmailLabel.Location = new Point(909, 173);
            EmailLabel.Name = "EmailLabel";
            EmailLabel.Size = new Size(210, 78);
            EmailLabel.TabIndex = 25;
            EmailLabel.Text = "Email";
            EmailLabel.TextAlign = ContentAlignment.TopCenter;
            EmailLabel.UseCompatibleTextRendering = true;
            // 
            // bikeTypeLable
            // 
            bikeTypeLable.BackColor = SystemColors.Control;
            bikeTypeLable.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            bikeTypeLable.Location = new Point(163, 46);
            bikeTypeLable.Name = "bikeTypeLable";
            bikeTypeLable.Size = new Size(210, 78);
            bikeTypeLable.TabIndex = 24;
            bikeTypeLable.Text = "Select Bike Type";
            bikeTypeLable.TextAlign = ContentAlignment.TopCenter;
            bikeTypeLable.UseCompatibleTextRendering = true;
            // 
            // bikeColorLabel
            // 
            bikeColorLabel.BackColor = SystemColors.Control;
            bikeColorLabel.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            bikeColorLabel.Location = new Point(414, 46);
            bikeColorLabel.Name = "bikeColorLabel";
            bikeColorLabel.Size = new Size(210, 78);
            bikeColorLabel.TabIndex = 23;
            bikeColorLabel.Text = "Select Bike Color";
            bikeColorLabel.TextAlign = ContentAlignment.TopCenter;
            bikeColorLabel.UseCompatibleTextRendering = true;
            // 
            // fullNameLabel
            // 
            fullNameLabel.BackColor = SystemColors.Control;
            fullNameLabel.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            fullNameLabel.Location = new Point(414, 173);
            fullNameLabel.Name = "fullNameLabel";
            fullNameLabel.Size = new Size(210, 78);
            fullNameLabel.TabIndex = 22;
            fullNameLabel.Text = "Full Name";
            fullNameLabel.TextAlign = ContentAlignment.TopCenter;
            fullNameLabel.UseCompatibleTextRendering = true;
            // 
            // bikeSizeLabel
            // 
            bikeSizeLabel.BackColor = SystemColors.Control;
            bikeSizeLabel.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            bikeSizeLabel.Location = new Point(663, 46);
            bikeSizeLabel.Name = "bikeSizeLabel";
            bikeSizeLabel.Size = new Size(210, 78);
            bikeSizeLabel.TabIndex = 21;
            bikeSizeLabel.Text = "Select Bike Size";
            bikeSizeLabel.TextAlign = ContentAlignment.TopCenter;
            bikeSizeLabel.UseCompatibleTextRendering = true;
            // 
            // phoneLabel
            // 
            phoneLabel.BackColor = SystemColors.Control;
            phoneLabel.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            phoneLabel.Location = new Point(663, 173);
            phoneLabel.Name = "phoneLabel";
            phoneLabel.Size = new Size(210, 78);
            phoneLabel.TabIndex = 20;
            phoneLabel.Text = "Phone";
            phoneLabel.TextAlign = ContentAlignment.TopCenter;
            phoneLabel.UseCompatibleTextRendering = true;
            // 
            // customerIdComboBox
            // 
            customerIdComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            customerIdComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            customerIdComboBox.Cursor = Cursors.Hand;
            customerIdComboBox.FormattingEnabled = true;
            customerIdComboBox.Location = new Point(163, 218);
            customerIdComboBox.Name = "customerIdComboBox";
            customerIdComboBox.Size = new Size(210, 33);
            customerIdComboBox.TabIndex = 14;
            customerIdComboBox.SelectedIndexChanged += customerIdComboBox_SelectedIndexChanged;
            // 
            // customerIdLabel
            // 
            customerIdLabel.BackColor = SystemColors.Control;
            customerIdLabel.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            customerIdLabel.Location = new Point(163, 173);
            customerIdLabel.Name = "customerIdLabel";
            customerIdLabel.Size = new Size(210, 78);
            customerIdLabel.TabIndex = 19;
            customerIdLabel.Text = "Customer ID";
            customerIdLabel.TextAlign = ContentAlignment.TopCenter;
            customerIdLabel.UseCompatibleTextRendering = true;
            // 
            // customerImagePanel
            // 
            customerImagePanel.BackgroundImage = (Image)resources.GetObject("customerImagePanel.BackgroundImage");
            customerImagePanel.BackgroundImageLayout = ImageLayout.Stretch;
            customerImagePanel.Location = new Point(44, 173);
            customerImagePanel.Name = "customerImagePanel";
            customerImagePanel.Size = new Size(78, 78);
            customerImagePanel.TabIndex = 10;
            // 
            // bikeImagePanel
            // 
            bikeImagePanel.BackgroundImage = Properties.Resources.Aha_Soft_Transport_Bike_256;
            bikeImagePanel.BackgroundImageLayout = ImageLayout.Stretch;
            bikeImagePanel.Location = new Point(44, 46);
            bikeImagePanel.Name = "bikeImagePanel";
            bikeImagePanel.Size = new Size(78, 78);
            bikeImagePanel.TabIndex = 1;
            // 
            // bikestorTornado
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1436, 613);
            Controls.Add(mainPanel);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "bikestorTornado";
            Text = "Bikestore \"Tornado\"";
            mainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)orderDataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel mainPanel;
        private Panel bikeImagePanel;
        private Panel panel10;
        protected Panel customerImagePanel;
        private ComboBox bikeTypeComboBox;
        private ComboBox fullNameComboBox;
        private ComboBox phoneComboBox;
        private ComboBox customerIdComboBox;
        private ComboBox bikeColorCombobox;
        private ComboBox madeInComboBox;
        private ComboBox emailComboBox;
        private Label customerIdLabel;
        private ComboBox bikeSizeComboBox;
        private Label bikeTypeLable;
        private Label bikeColorLabel;
        private Label fullNameLabel;
        private Label bikeSizeLabel;
        private Label phoneLabel;
        private Label madeInLable;
        private Label EmailLabel;
        private Label quantityLabel;
        private ComboBox quantityComboBox;
        private Button clearButton;
        private DataGridView orderDataGridView;
        private Button saleButton;
        private DataGridViewTextBoxColumn OrderDate;
        private DataGridViewTextBoxColumn CustomerName;
        private DataGridViewTextBoxColumn BikeID;
        private DataGridViewTextBoxColumn UnitPrice;
        private DataGridViewTextBoxColumn Quantity;
        private DataGridViewTextBoxColumn TotalPrice;
        private Button AddNewCustomerButton;
        private Button loadOrdPerCustButton;
        private Button clearDataGridButton;
        private Button viewOrdersButton;
        private DateTimePicker toDatePicker;
        private DateTimePicker fromDatePicker;
        private Panel documentIconPanel;
    }
}
