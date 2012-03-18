partial class MainForm
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
        this.components = new System.ComponentModel.Container();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.chkNotifyPropertyChanges = new System.Windows.Forms.CheckBox();
        this.ChkConsiderChildsOnlyInCurrent = new System.Windows.Forms.CheckBox();
        this.chkAutoCreateObjects = new System.Windows.Forms.CheckBox();
        this.chkNotifyChangesChildLists = new System.Windows.Forms.CheckBox();
        this.dataGridView1 = new System.Windows.Forms.DataGridView();
        this.orderNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.Customer = new System.Windows.Forms.DataGridViewComboBoxColumn();
        this.customersBindingSource = new UI.ObjectBindingSource.ObjectBindingSource(this.components);
        this.orderDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.customerNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.customerBillingAddressStreetAddressDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.customerBillingAddressCityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.deliveryAddressStreetAddressDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.deliveryAddressCityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.ordersBindingSource = new UI.ObjectBindingSource.ObjectBindingSource(this.components);
        this.groupBox2 = new System.Windows.Forms.GroupBox();
        this.chkNotifyPropertyChanges2 = new System.Windows.Forms.CheckBox();
        this.ChkConsiderChildsOnlyInCurrent2 = new System.Windows.Forms.CheckBox();
        this.chkAutoCreateObjects2 = new System.Windows.Forms.CheckBox();
        this.chkNotifyChangesChildLists2 = new System.Windows.Forms.CheckBox();
        this.dataGridView2 = new System.Windows.Forms.DataGridView();
        this.Product = new System.Windows.Forms.DataGridViewComboBoxColumn();
        this.productsBindingSource = new UI.ObjectBindingSource.ObjectBindingSource(this.components);
        this.productDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.quantityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.productProductIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.Product_UnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.Product_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.orderlinesBindingSource = new UI.ObjectBindingSource.ObjectBindingSource(this.components);
        this.btnChanges = new System.Windows.Forms.Button();
        this.textBox1 = new System.Windows.Forms.TextBox();
        this.label1 = new System.Windows.Forms.Label();
        this.comboBox1 = new System.Windows.Forms.ComboBox();
        this.otherItemsBindingSource = new UI.ObjectBindingSource.ObjectBindingSource(this.components);
        this.comboBox2 = new System.Windows.Forms.ComboBox();
        this.simpleClassesBindingSource = new UI.ObjectBindingSource.ObjectBindingSource(this.components);
        this.btnGC = new System.Windows.Forms.Button();
        this.cbAction = new System.Windows.Forms.ComboBox();
        this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
        this.chkConsiderNonNested_Product = new System.Windows.Forms.CheckBox();
        this.btnObjectsAlive = new System.Windows.Forms.Button();
        this.btnShowOBS = new System.Windows.Forms.Button();
        this.btnHookedObjects = new System.Windows.Forms.Button();
        this.lblDebugFile = new System.Windows.Forms.Label();
        this.chkConsiderOnlyDetails = new System.Windows.Forms.CheckBox();
        this.groupBox1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.customersBindingSource)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.ordersBindingSource)).BeginInit();
        this.groupBox2.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.productsBindingSource)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.orderlinesBindingSource)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.otherItemsBindingSource)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.simpleClassesBindingSource)).BeginInit();
        this.SuspendLayout();
        // 
        // groupBox1
        // 
        this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.groupBox1.Controls.Add(this.chkNotifyPropertyChanges);
        this.groupBox1.Controls.Add(this.ChkConsiderChildsOnlyInCurrent);
        this.groupBox1.Controls.Add(this.chkAutoCreateObjects);
        this.groupBox1.Controls.Add(this.chkNotifyChangesChildLists);
        this.groupBox1.Controls.Add(this.dataGridView1);
        this.groupBox1.Location = new System.Drawing.Point(12, 45);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(968, 238);
        this.groupBox1.TabIndex = 2;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "Orders";
        // 
        // chkNotifyPropertyChanges
        // 
        this.chkNotifyPropertyChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.chkNotifyPropertyChanges.AutoSize = true;
        this.chkNotifyPropertyChanges.Checked = true;
        this.chkNotifyPropertyChanges.CheckState = System.Windows.Forms.CheckState.Checked;
        this.chkNotifyPropertyChanges.Location = new System.Drawing.Point(823, 215);
        this.chkNotifyPropertyChanges.Name = "chkNotifyPropertyChanges";
        this.chkNotifyPropertyChanges.Size = new System.Drawing.Size(134, 17);
        this.chkNotifyPropertyChanges.TabIndex = 31;
        this.chkNotifyPropertyChanges.Text = "NotifyPropertyChanges";
        this.chkNotifyPropertyChanges.UseVisualStyleBackColor = true;
        this.chkNotifyPropertyChanges.CheckedChanged += new System.EventHandler(this.chkNotifyPropertyChanges_CheckedChanged);
        // 
        // ChkConsiderChildsOnlyInCurrent
        // 
        this.ChkConsiderChildsOnlyInCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.ChkConsiderChildsOnlyInCurrent.AutoSize = true;
        this.ChkConsiderChildsOnlyInCurrent.Checked = true;
        this.ChkConsiderChildsOnlyInCurrent.CheckState = System.Windows.Forms.CheckState.Checked;
        this.ChkConsiderChildsOnlyInCurrent.Location = new System.Drawing.Point(317, 215);
        this.ChkConsiderChildsOnlyInCurrent.Name = "ChkConsiderChildsOnlyInCurrent";
        this.ChkConsiderChildsOnlyInCurrent.Size = new System.Drawing.Size(159, 17);
        this.ChkConsiderChildsOnlyInCurrent.TabIndex = 27;
        this.ChkConsiderChildsOnlyInCurrent.Text = "ConsiderChildsOnlyInCurrent";
        this.ChkConsiderChildsOnlyInCurrent.UseVisualStyleBackColor = true;
        this.ChkConsiderChildsOnlyInCurrent.CheckedChanged += new System.EventHandler(this.ChkConsiderChildsOnlyInCurrent_CheckedChanged);
        // 
        // chkAutoCreateObjects
        // 
        this.chkAutoCreateObjects.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.chkAutoCreateObjects.AutoSize = true;
        this.chkAutoCreateObjects.Checked = true;
        this.chkAutoCreateObjects.CheckState = System.Windows.Forms.CheckState.Checked;
        this.chkAutoCreateObjects.Location = new System.Drawing.Point(16, 215);
        this.chkAutoCreateObjects.Name = "chkAutoCreateObjects";
        this.chkAutoCreateObjects.Size = new System.Drawing.Size(115, 17);
        this.chkAutoCreateObjects.TabIndex = 25;
        this.chkAutoCreateObjects.Text = "AutoCreateObjects";
        this.chkAutoCreateObjects.UseVisualStyleBackColor = true;
        this.chkAutoCreateObjects.CheckedChanged += new System.EventHandler(this.chkAutoCreateObjects_CheckedChanged);
        // 
        // chkNotifyChangesChildLists
        // 
        this.chkNotifyChangesChildLists.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.chkNotifyChangesChildLists.AutoSize = true;
        this.chkNotifyChangesChildLists.Location = new System.Drawing.Point(162, 215);
        this.chkNotifyChangesChildLists.Name = "chkNotifyChangesChildLists";
        this.chkNotifyChangesChildLists.Size = new System.Drawing.Size(139, 17);
        this.chkNotifyChangesChildLists.TabIndex = 26;
        this.chkNotifyChangesChildLists.Text = "NotifyChangesChildLists";
        this.chkNotifyChangesChildLists.UseVisualStyleBackColor = true;
        this.chkNotifyChangesChildLists.CheckedChanged += new System.EventHandler(this.chkNotifyChangesChildLists_CheckedChanged);
        // 
        // dataGridView1
        // 
        this.dataGridView1.AllowUserToOrderColumns = true;
        this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.dataGridView1.AutoGenerateColumns = false;
        this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
        dataGridViewCellStyle3.BackColor = System.Drawing.Color.NavajoWhite;
        dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
        dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
        this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.orderNumberDataGridViewTextBoxColumn,
            this.Customer,
            this.orderDateDataGridViewTextBoxColumn,
            this.customerNameDataGridViewTextBoxColumn,
            this.customerBillingAddressStreetAddressDataGridViewTextBoxColumn,
            this.customerBillingAddressCityDataGridViewTextBoxColumn,
            this.deliveryAddressStreetAddressDataGridViewTextBoxColumn,
            this.deliveryAddressCityDataGridViewTextBoxColumn});
        this.dataGridView1.DataSource = this.ordersBindingSource;
        this.dataGridView1.EnableHeadersVisualStyles = false;
        this.dataGridView1.GridColor = System.Drawing.Color.MistyRose;
        this.dataGridView1.Location = new System.Drawing.Point(7, 19);
        this.dataGridView1.Name = "dataGridView1";
        this.dataGridView1.Size = new System.Drawing.Size(956, 185);
        this.dataGridView1.TabIndex = 0;
        // 
        // orderNumberDataGridViewTextBoxColumn
        // 
        this.orderNumberDataGridViewTextBoxColumn.DataPropertyName = "OrderNumber";
        this.orderNumberDataGridViewTextBoxColumn.HeaderText = "OrderNumber";
        this.orderNumberDataGridViewTextBoxColumn.Name = "orderNumberDataGridViewTextBoxColumn";
        this.orderNumberDataGridViewTextBoxColumn.Width = 95;
        // 
        // Customer
        // 
        this.Customer.DataPropertyName = "Customer";
        this.Customer.DataSource = this.customersBindingSource;
        this.Customer.DisplayMember = "Name";
        this.Customer.HeaderText = "Customer";
        this.Customer.Name = "Customer";
        this.Customer.Resizable = System.Windows.Forms.DataGridViewTriState.True;
        this.Customer.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.Customer.ValueMember = "Self";
        // 
        // customersBindingSource
        // 
        this.customersBindingSource.AutoCreateObjects = false;
        this.customersBindingSource.BindableNestedProperties = new string[0];
        this.customersBindingSource.ChildListsToConsider = null;
        this.customersBindingSource.ConsiderChildsOnlyInCurrent = true;
        this.customersBindingSource.DataSource = typeof(Customer);
        this.customersBindingSource.NonNestedPropertiesToSupervise = null;
        this.customersBindingSource.NotifyChangesInNestedPropertiesFromChildlists = false;
        this.customersBindingSource.NotifyPropertyChanges = true;
        this.customersBindingSource.RelatedObjectBindingSources = new UI.ObjectBindingSource.ObjectBindingSource[0];
        // 
        // orderDateDataGridViewTextBoxColumn
        // 
        this.orderDateDataGridViewTextBoxColumn.DataPropertyName = "OrderDate";
        this.orderDateDataGridViewTextBoxColumn.HeaderText = "OrderDate";
        this.orderDateDataGridViewTextBoxColumn.Name = "orderDateDataGridViewTextBoxColumn";
        // 
        // customerNameDataGridViewTextBoxColumn
        // 
        this.customerNameDataGridViewTextBoxColumn.DataPropertyName = "Customer_Name";
        this.customerNameDataGridViewTextBoxColumn.HeaderText = "Customer Name";
        this.customerNameDataGridViewTextBoxColumn.Name = "customerNameDataGridViewTextBoxColumn";
        // 
        // customerBillingAddressStreetAddressDataGridViewTextBoxColumn
        // 
        this.customerBillingAddressStreetAddressDataGridViewTextBoxColumn.DataPropertyName = "Customer_BillingAddress_StreetAddress";
        this.customerBillingAddressStreetAddressDataGridViewTextBoxColumn.HeaderText = "Customer BillingAddress StreetAddress";
        this.customerBillingAddressStreetAddressDataGridViewTextBoxColumn.Name = "customerBillingAddressStreetAddressDataGridViewTextBoxColumn";
        // 
        // customerBillingAddressCityDataGridViewTextBoxColumn
        // 
        this.customerBillingAddressCityDataGridViewTextBoxColumn.DataPropertyName = "Customer_BillingAddress_City";
        this.customerBillingAddressCityDataGridViewTextBoxColumn.HeaderText = "Customer BillingAddress City";
        this.customerBillingAddressCityDataGridViewTextBoxColumn.Name = "customerBillingAddressCityDataGridViewTextBoxColumn";
        this.customerBillingAddressCityDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
        this.customerBillingAddressCityDataGridViewTextBoxColumn.Width = 164;
        // 
        // deliveryAddressStreetAddressDataGridViewTextBoxColumn
        // 
        this.deliveryAddressStreetAddressDataGridViewTextBoxColumn.DataPropertyName = "DeliveryAddress_StreetAddress";
        this.deliveryAddressStreetAddressDataGridViewTextBoxColumn.HeaderText = "DeliveryAddress StreetAddress";
        this.deliveryAddressStreetAddressDataGridViewTextBoxColumn.Name = "deliveryAddressStreetAddressDataGridViewTextBoxColumn";
        this.deliveryAddressStreetAddressDataGridViewTextBoxColumn.Width = 177;
        // 
        // deliveryAddressCityDataGridViewTextBoxColumn
        // 
        this.deliveryAddressCityDataGridViewTextBoxColumn.DataPropertyName = "DeliveryAddress_City";
        this.deliveryAddressCityDataGridViewTextBoxColumn.HeaderText = "DeliveryAddress City";
        this.deliveryAddressCityDataGridViewTextBoxColumn.Name = "deliveryAddressCityDataGridViewTextBoxColumn";
        this.deliveryAddressCityDataGridViewTextBoxColumn.Width = 128;
        // 
        // ordersBindingSource
        // 
        this.ordersBindingSource.AllowNew = true;
        this.ordersBindingSource.AutoCreateObjects = true;
        this.ordersBindingSource.BindableNestedProperties = new string[] {
        "Customer.Name",
        "Customer.BillingAddress.StreetAddress",
        "Customer.BillingAddress.City",
        "DeliveryAddress.StreetAddress",
        "DeliveryAddress.City"};
        this.ordersBindingSource.ChildListsToConsider = null;
        this.ordersBindingSource.ConsiderChildsOnlyInCurrent = true;
        this.ordersBindingSource.DataSource = typeof(Order);
        this.ordersBindingSource.NonNestedPropertiesToSupervise = null;
        this.ordersBindingSource.NotifyChangesInNestedPropertiesFromChildlists = false;
        this.ordersBindingSource.NotifyPropertyChanges = true;
        this.ordersBindingSource.RelatedObjectBindingSources = new UI.ObjectBindingSource.ObjectBindingSource[0];
        this.ordersBindingSource.CreatingObject += new UI.ObjectBindingSource.ObjectBindingSource.CreatingObjectEventHandler(this.ordersBindingSource_CreatingObject);
        this.ordersBindingSource.NestedError += new UI.ObjectBindingSource.ObjectBindingSource.NestedErrorEventHandler(this.ordersBindingSource_NestedError);
        // 
        // groupBox2
        // 
        this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.groupBox2.Controls.Add(this.chkNotifyPropertyChanges2);
        this.groupBox2.Controls.Add(this.ChkConsiderChildsOnlyInCurrent2);
        this.groupBox2.Controls.Add(this.chkAutoCreateObjects2);
        this.groupBox2.Controls.Add(this.chkNotifyChangesChildLists2);
        this.groupBox2.Controls.Add(this.dataGridView2);
        this.groupBox2.Location = new System.Drawing.Point(12, 283);
        this.groupBox2.Name = "groupBox2";
        this.groupBox2.Size = new System.Drawing.Size(968, 211);
        this.groupBox2.TabIndex = 3;
        this.groupBox2.TabStop = false;
        this.groupBox2.Text = "Order Lines";
        // 
        // chkNotifyPropertyChanges2
        // 
        this.chkNotifyPropertyChanges2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.chkNotifyPropertyChanges2.AutoSize = true;
        this.chkNotifyPropertyChanges2.Checked = true;
        this.chkNotifyPropertyChanges2.CheckState = System.Windows.Forms.CheckState.Checked;
        this.chkNotifyPropertyChanges2.Location = new System.Drawing.Point(823, 188);
        this.chkNotifyPropertyChanges2.Name = "chkNotifyPropertyChanges2";
        this.chkNotifyPropertyChanges2.Size = new System.Drawing.Size(134, 17);
        this.chkNotifyPropertyChanges2.TabIndex = 30;
        this.chkNotifyPropertyChanges2.Text = "NotifyPropertyChanges";
        this.chkNotifyPropertyChanges2.UseVisualStyleBackColor = true;
        this.chkNotifyPropertyChanges2.CheckedChanged += new System.EventHandler(this.chkNotifyPropertyChanges2_CheckedChanged);
        // 
        // ChkConsiderChildsOnlyInCurrent2
        // 
        this.ChkConsiderChildsOnlyInCurrent2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.ChkConsiderChildsOnlyInCurrent2.AutoSize = true;
        this.ChkConsiderChildsOnlyInCurrent2.Checked = true;
        this.ChkConsiderChildsOnlyInCurrent2.CheckState = System.Windows.Forms.CheckState.Checked;
        this.ChkConsiderChildsOnlyInCurrent2.Location = new System.Drawing.Point(314, 188);
        this.ChkConsiderChildsOnlyInCurrent2.Name = "ChkConsiderChildsOnlyInCurrent2";
        this.ChkConsiderChildsOnlyInCurrent2.Size = new System.Drawing.Size(159, 17);
        this.ChkConsiderChildsOnlyInCurrent2.TabIndex = 29;
        this.ChkConsiderChildsOnlyInCurrent2.Text = "ConsiderChildsOnlyInCurrent";
        this.ChkConsiderChildsOnlyInCurrent2.UseVisualStyleBackColor = true;
        this.ChkConsiderChildsOnlyInCurrent2.CheckedChanged += new System.EventHandler(this.ChkConsiderChildsOnlyInCurrent2_CheckedChanged);
        // 
        // chkAutoCreateObjects2
        // 
        this.chkAutoCreateObjects2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.chkAutoCreateObjects2.AutoSize = true;
        this.chkAutoCreateObjects2.Location = new System.Drawing.Point(12, 188);
        this.chkAutoCreateObjects2.Name = "chkAutoCreateObjects2";
        this.chkAutoCreateObjects2.Size = new System.Drawing.Size(115, 17);
        this.chkAutoCreateObjects2.TabIndex = 27;
        this.chkAutoCreateObjects2.Text = "AutoCreateObjects";
        this.chkAutoCreateObjects2.UseVisualStyleBackColor = true;
        this.chkAutoCreateObjects2.CheckedChanged += new System.EventHandler(this.chkAutoCreateObjects2_CheckedChanged);
        // 
        // chkNotifyChangesChildLists2
        // 
        this.chkNotifyChangesChildLists2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.chkNotifyChangesChildLists2.AutoSize = true;
        this.chkNotifyChangesChildLists2.Location = new System.Drawing.Point(154, 188);
        this.chkNotifyChangesChildLists2.Name = "chkNotifyChangesChildLists2";
        this.chkNotifyChangesChildLists2.Size = new System.Drawing.Size(139, 17);
        this.chkNotifyChangesChildLists2.TabIndex = 28;
        this.chkNotifyChangesChildLists2.Text = "NotifyChangesChildLists";
        this.chkNotifyChangesChildLists2.UseVisualStyleBackColor = true;
        this.chkNotifyChangesChildLists2.CheckedChanged += new System.EventHandler(this.chkNotifyChangesChildLists2_CheckedChanged);
        // 
        // dataGridView2
        // 
        this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.dataGridView2.AutoGenerateColumns = false;
        this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
        dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle4.BackColor = System.Drawing.Color.NavajoWhite;
        dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
        dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
        this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Product,
            this.productDataGridViewTextBoxColumn,
            this.quantityDataGridViewTextBoxColumn,
            this.productProductIdDataGridViewTextBoxColumn,
            this.Product_UnitPrice,
            this.Product_Name});
        this.dataGridView2.DataSource = this.orderlinesBindingSource;
        this.dataGridView2.EnableHeadersVisualStyles = false;
        this.dataGridView2.GridColor = System.Drawing.Color.MistyRose;
        this.dataGridView2.Location = new System.Drawing.Point(6, 19);
        this.dataGridView2.Name = "dataGridView2";
        this.dataGridView2.RowHeadersVisible = false;
        this.dataGridView2.Size = new System.Drawing.Size(956, 161);
        this.dataGridView2.TabIndex = 2;
        // 
        // Product
        // 
        this.Product.DataPropertyName = "Product";
        this.Product.DataSource = this.productsBindingSource;
        this.Product.DisplayMember = "Name";
        this.Product.HeaderText = "Product";
        this.Product.Name = "Product";
        this.Product.Resizable = System.Windows.Forms.DataGridViewTriState.True;
        this.Product.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.Product.ValueMember = "Self";
        // 
        // productsBindingSource
        // 
        this.productsBindingSource.AutoCreateObjects = false;
        this.productsBindingSource.BindableNestedProperties = new string[0];
        this.productsBindingSource.ChildListsToConsider = null;
        this.productsBindingSource.ConsiderChildsOnlyInCurrent = true;
        this.productsBindingSource.DataSource = typeof(Product);
        this.productsBindingSource.NonNestedPropertiesToSupervise = null;
        this.productsBindingSource.NotifyChangesInNestedPropertiesFromChildlists = false;
        this.productsBindingSource.NotifyPropertyChanges = true;
        this.productsBindingSource.RelatedObjectBindingSources = new UI.ObjectBindingSource.ObjectBindingSource[0];
        // 
        // productDataGridViewTextBoxColumn
        // 
        this.productDataGridViewTextBoxColumn.DataPropertyName = "Product";
        this.productDataGridViewTextBoxColumn.HeaderText = "Product";
        this.productDataGridViewTextBoxColumn.Name = "productDataGridViewTextBoxColumn";
        // 
        // quantityDataGridViewTextBoxColumn
        // 
        this.quantityDataGridViewTextBoxColumn.DataPropertyName = "Quantity";
        this.quantityDataGridViewTextBoxColumn.HeaderText = "Quantity";
        this.quantityDataGridViewTextBoxColumn.Name = "quantityDataGridViewTextBoxColumn";
        // 
        // productProductIdDataGridViewTextBoxColumn
        // 
        this.productProductIdDataGridViewTextBoxColumn.DataPropertyName = "Product_ProductId";
        this.productProductIdDataGridViewTextBoxColumn.HeaderText = "Product_ProductId";
        this.productProductIdDataGridViewTextBoxColumn.Name = "productProductIdDataGridViewTextBoxColumn";
        // 
        // Product_UnitPrice
        // 
        this.Product_UnitPrice.DataPropertyName = "Product_UnitPrice";
        this.Product_UnitPrice.HeaderText = "Product_UnitPrice";
        this.Product_UnitPrice.Name = "Product_UnitPrice";
        // 
        // Product_Name
        // 
        this.Product_Name.DataPropertyName = "Product_Name";
        this.Product_Name.HeaderText = "Product_Name";
        this.Product_Name.Name = "Product_Name";
        // 
        // orderlinesBindingSource
        // 
        this.orderlinesBindingSource.AllowNew = true;
        this.orderlinesBindingSource.AutoCreateObjects = false;
        this.orderlinesBindingSource.BindableNestedProperties = new string[] {
        "Product.ProductId",
        "Product.UnitPrice",
        "Product.Name"};
        this.orderlinesBindingSource.ChildListsToConsider = null;
        this.orderlinesBindingSource.ConsiderChildsOnlyInCurrent = true;
        this.orderlinesBindingSource.DataMember = "OrderLines";
        this.orderlinesBindingSource.DataSource = this.ordersBindingSource;
        this.orderlinesBindingSource.NonNestedPropertiesToSupervise = null;
        this.orderlinesBindingSource.NotifyChangesInNestedPropertiesFromChildlists = false;
        this.orderlinesBindingSource.NotifyPropertyChanges = true;
        this.orderlinesBindingSource.RelatedObjectBindingSources = new UI.ObjectBindingSource.ObjectBindingSource[0];
        this.orderlinesBindingSource.CreatingObject += new UI.ObjectBindingSource.ObjectBindingSource.CreatingObjectEventHandler(this.ordersBindingSource_2_CreatingObject);
        // 
        // btnChanges
        // 
        this.btnChanges.Location = new System.Drawing.Point(923, 11);
        this.btnChanges.Name = "btnChanges";
        this.btnChanges.Size = new System.Drawing.Size(57, 23);
        this.btnChanges.TabIndex = 6;
        this.btnChanges.Text = "Apply";
        this.btnChanges.UseVisualStyleBackColor = true;
        this.btnChanges.Click += new System.EventHandler(this.btnChanges_Click);
        // 
        // textBox1
        // 
        this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.textBox1.Location = new System.Drawing.Point(948, 501);
        this.textBox1.Name = "textBox1";
        this.textBox1.Size = new System.Drawing.Size(31, 20);
        this.textBox1.TabIndex = 7;
        this.textBox1.Validated += new System.EventHandler(this.textBox1_Validated);
        // 
        // label1
        // 
        this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.label1.Location = new System.Drawing.Point(844, 502);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(103, 19);
        this.label1.TabIndex = 8;
        this.label1.Text = "Max. Debug Level:";
        // 
        // comboBox1
        // 
        this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.comboBox1.DataSource = this.otherItemsBindingSource;
        this.comboBox1.DisplayMember = "Product_Name";
        this.comboBox1.FormattingEnabled = true;
        this.comboBox1.Location = new System.Drawing.Point(244, 514);
        this.comboBox1.Name = "comboBox1";
        this.comboBox1.Size = new System.Drawing.Size(121, 21);
        this.comboBox1.TabIndex = 10;
        // 
        // otherItemsBindingSource
        // 
        this.otherItemsBindingSource.AutoCreateObjects = false;
        this.otherItemsBindingSource.BindableNestedProperties = new string[] {
        "Product.Name"};
        this.otherItemsBindingSource.ChildListsToConsider = null;
        this.otherItemsBindingSource.ConsiderChildsOnlyInCurrent = true;
        this.otherItemsBindingSource.DataSource = typeof(OtherItem);
        this.otherItemsBindingSource.NonNestedPropertiesToSupervise = null;
        this.otherItemsBindingSource.NotifyChangesInNestedPropertiesFromChildlists = false;
        this.otherItemsBindingSource.NotifyPropertyChanges = true;
        this.otherItemsBindingSource.RelatedObjectBindingSources = new UI.ObjectBindingSource.ObjectBindingSource[0];
        // 
        // comboBox2
        // 
        this.comboBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.comboBox2.DataSource = this.simpleClassesBindingSource;
        this.comboBox2.DisplayMember = "MyProperty";
        this.comboBox2.FormattingEnabled = true;
        this.comboBox2.Location = new System.Drawing.Point(400, 514);
        this.comboBox2.Name = "comboBox2";
        this.comboBox2.Size = new System.Drawing.Size(121, 21);
        this.comboBox2.TabIndex = 11;
        // 
        // simpleClassesBindingSource
        // 
        this.simpleClassesBindingSource.AutoCreateObjects = true;
        this.simpleClassesBindingSource.BindableNestedProperties = new string[0];
        this.simpleClassesBindingSource.ChildListsToConsider = null;
        this.simpleClassesBindingSource.ConsiderChildsOnlyInCurrent = true;
        this.simpleClassesBindingSource.DataSource = typeof(SimpleClass);
        this.simpleClassesBindingSource.NonNestedPropertiesToSupervise = null;
        this.simpleClassesBindingSource.NotifyChangesInNestedPropertiesFromChildlists = false;
        this.simpleClassesBindingSource.NotifyPropertyChanges = true;
        this.simpleClassesBindingSource.RelatedObjectBindingSources = new UI.ObjectBindingSource.ObjectBindingSource[0];
        // 
        // btnGC
        // 
        this.btnGC.Location = new System.Drawing.Point(375, 12);
        this.btnGC.Name = "btnGC";
        this.btnGC.Size = new System.Drawing.Size(75, 23);
        this.btnGC.TabIndex = 12;
        this.btnGC.Text = "GC.Collect";
        this.btnGC.UseVisualStyleBackColor = true;
        this.btnGC.Click += new System.EventHandler(this.btnGC_Click);
        // 
        // cbAction
        // 
        this.cbAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cbAction.FormattingEnabled = true;
        this.cbAction.Items.AddRange(new object[] {
            " 0: MOD \"Keyboard\"",
            " 1: MOD \"Jane Wilson\" & \"Bill Smith\" ",
            " 2: MOD \"Keyboard\" & \"Mouse\"",
            " 3: INC OrderDate: Order-1 & Order-2",
            " 4: Order-1.OrderLines[1].Product <==  Order-1.OrderLines[0].Product",
            " 5: INC OrderLines[0].Quantity:  Order-1 & Order-2",
            " 6: INC Customer.Age  <Jane Wilson> & <Bill Smith>",
            "20: MOD Orders[0].OrderLines[0].Details[0].NotShownProperty",
            " 7: INC Product.UnitPrice: <Keyboard> & <Mouse>",
            " 8: MOD <Keyboard>.ChangeMultiple (Property=Nothing)",
            "----------",
            "13: Add New Order (4: <Jane Wilson>)",
            "14: Remove New Order (4: <Jane Wilson>)",
            "24: INSERT New Order (4: <Jane Wilson>) at position 1",
            "-----",
            "15: MOD ordersBindingSource.BindableNestedProperties: A",
            "16: MOD ordersBindingSource.BindableNestedProperties: B",
            "17: MOD orderlinesBindingSource.BindableNestedProperties: A",
            "25: MOD orderlinesBindingSource.BindableNestedProperties: B",
            "--------",
            "23: MOD ordersBindingSource.BindableNestedProperties: C",
            "------",
            "26: MOD orderlinesBindingSource.BindableNestedProperties: C",
            "-----",
            "21: Orders[0].OrderLines = Orders[0].OrderLines",
            "22: Orders[0].OrderLines = Orders[1].OrderLines",
            "-----------------",
            " 9: Remove <Samantha Brown>",
            "10: Remove <LapTop>",
            "11: Delete All",
            "12: BindingSourceS.Dispose"});
        this.cbAction.Location = new System.Drawing.Point(474, 12);
        this.cbAction.Name = "cbAction";
        this.cbAction.Size = new System.Drawing.Size(443, 21);
        this.cbAction.TabIndex = 13;
        // 
        // chkConsiderNonNested_Product
        // 
        this.chkConsiderNonNested_Product.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.chkConsiderNonNested_Product.AutoSize = true;
        this.chkConsiderNonNested_Product.Location = new System.Drawing.Point(18, 519);
        this.chkConsiderNonNested_Product.Name = "chkConsiderNonNested_Product";
        this.chkConsiderNonNested_Product.Size = new System.Drawing.Size(167, 17);
        this.chkConsiderNonNested_Product.TabIndex = 26;
        this.chkConsiderNonNested_Product.Text = "Consider non nested \'Product\'";
        this.toolTip1.SetToolTip(this.chkConsiderNonNested_Product, "To see how it works make sure you select action \'26\' (no nested properties in Ord" +
                "er Lines grid)");
        this.chkConsiderNonNested_Product.UseVisualStyleBackColor = true;
        this.chkConsiderNonNested_Product.CheckedChanged += new System.EventHandler(this.chkConsiderNonNested_Product_CheckedChanged);
        // 
        // btnObjectsAlive
        // 
        this.btnObjectsAlive.Location = new System.Drawing.Point(149, 12);
        this.btnObjectsAlive.Name = "btnObjectsAlive";
        this.btnObjectsAlive.Size = new System.Drawing.Size(108, 23);
        this.btnObjectsAlive.TabIndex = 15;
        this.btnObjectsAlive.Text = "Show Objects Alive";
        this.btnObjectsAlive.UseVisualStyleBackColor = true;
        this.btnObjectsAlive.Click += new System.EventHandler(this.btnObjectsAlive_Click);
        // 
        // btnShowOBS
        // 
        this.btnShowOBS.Location = new System.Drawing.Point(264, 12);
        this.btnShowOBS.Name = "btnShowOBS";
        this.btnShowOBS.Size = new System.Drawing.Size(101, 23);
        this.btnShowOBS.TabIndex = 25;
        this.btnShowOBS.Text = "Show oBS alive";
        this.btnShowOBS.UseVisualStyleBackColor = true;
        this.btnShowOBS.Click += new System.EventHandler(this.btnShowOBS_Click);
        // 
        // btnHookedObjects
        // 
        this.btnHookedObjects.Location = new System.Drawing.Point(18, 12);
        this.btnHookedObjects.Name = "btnHookedObjects";
        this.btnHookedObjects.Size = new System.Drawing.Size(125, 23);
        this.btnHookedObjects.TabIndex = 27;
        this.btnHookedObjects.Text = "Show Hooked Objects";
        this.btnHookedObjects.UseVisualStyleBackColor = true;
        this.btnHookedObjects.Click += new System.EventHandler(this.btnHookedObjects_Click);
        // 
        // lblDebugFile
        // 
        this.lblDebugFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.lblDebugFile.Location = new System.Drawing.Point(782, 524);
        this.lblDebugFile.Name = "lblDebugFile";
        this.lblDebugFile.Size = new System.Drawing.Size(192, 19);
        this.lblDebugFile.TabIndex = 28;
        this.lblDebugFile.Text = "FILE";
        // 
        // chkConsiderOnlyDetails
        // 
        this.chkConsiderOnlyDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.chkConsiderOnlyDetails.AutoSize = true;
        this.chkConsiderOnlyDetails.Checked = true;
        this.chkConsiderOnlyDetails.CheckState = System.Windows.Forms.CheckState.Checked;
        this.chkConsiderOnlyDetails.Location = new System.Drawing.Point(18, 501);
        this.chkConsiderOnlyDetails.Name = "chkConsiderOnlyDetails";
        this.chkConsiderOnlyDetails.Size = new System.Drawing.Size(172, 17);
        this.chkConsiderOnlyDetails.TabIndex = 29;
        this.chkConsiderOnlyDetails.Text = "ConsiderOnly OrderLine.Details";
        this.chkConsiderOnlyDetails.UseVisualStyleBackColor = true;
        this.chkConsiderOnlyDetails.CheckedChanged += new System.EventHandler(this.chkConsiderOnlyDetails_CheckedChanged);
        // 
        // MainForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.WhiteSmoke;
        this.ClientSize = new System.Drawing.Size(992, 545);
        this.Controls.Add(this.chkConsiderOnlyDetails);
        this.Controls.Add(this.lblDebugFile);
        this.Controls.Add(this.btnHookedObjects);
        this.Controls.Add(this.chkConsiderNonNested_Product);
        this.Controls.Add(this.btnShowOBS);
        this.Controls.Add(this.btnObjectsAlive);
        this.Controls.Add(this.cbAction);
        this.Controls.Add(this.btnGC);
        this.Controls.Add(this.comboBox2);
        this.Controls.Add(this.comboBox1);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.textBox1);
        this.Controls.Add(this.btnChanges);
        this.Controls.Add(this.groupBox2);
        this.Controls.Add(this.groupBox1);
        this.Name = "MainForm";
        this.Text = "ObjectBindingSource Demo (Nested Property Binding)";
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.customersBindingSource)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.ordersBindingSource)).EndInit();
        this.groupBox2.ResumeLayout(false);
        this.groupBox2.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.productsBindingSource)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.orderlinesBindingSource)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.otherItemsBindingSource)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.simpleClassesBindingSource)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion


    private System.Windows.Forms.GroupBox groupBox1;

    private UI.ObjectBindingSource.ObjectBindingSource ordersBindingSource;
    private UI.ObjectBindingSource.ObjectBindingSource orderlinesBindingSource;
    private System.Windows.Forms.DataGridView dataGridView1;
    private UI.ObjectBindingSource.ObjectBindingSource customersBindingSource;
    private UI.ObjectBindingSource.ObjectBindingSource productsBindingSource;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.DataGridView dataGridView2;
    private System.Windows.Forms.Button btnChanges;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Label label1;
    private UI.ObjectBindingSource.ObjectBindingSource otherItemsBindingSource;
    private UI.ObjectBindingSource.ObjectBindingSource simpleClassesBindingSource;
    private System.Windows.Forms.ComboBox comboBox1;
    private System.Windows.Forms.ComboBox comboBox2;
    private System.Windows.Forms.Button btnGC;
    private System.Windows.Forms.ComboBox cbAction;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.Button btnObjectsAlive;
    private System.Windows.Forms.Button btnShowOBS;
    private System.Windows.Forms.CheckBox ChkConsiderChildsOnlyInCurrent;
    private System.Windows.Forms.CheckBox chkAutoCreateObjects;
    private System.Windows.Forms.CheckBox chkNotifyChangesChildLists;
    private System.Windows.Forms.CheckBox ChkConsiderChildsOnlyInCurrent2;
    private System.Windows.Forms.CheckBox chkAutoCreateObjects2;
    private System.Windows.Forms.CheckBox chkNotifyChangesChildLists2;
    private System.Windows.Forms.CheckBox chkConsiderNonNested_Product;
    private System.Windows.Forms.DataGridViewTextBoxColumn orderNumberDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewComboBoxColumn Customer;
    private System.Windows.Forms.DataGridViewTextBoxColumn orderDateDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn customerNameDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn customerBillingAddressStreetAddressDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn customerBillingAddressCityDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn deliveryAddressStreetAddressDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn deliveryAddressCityDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewComboBoxColumn Product;
    private System.Windows.Forms.DataGridViewTextBoxColumn productDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn quantityDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn productProductIdDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn Product_UnitPrice;
    private System.Windows.Forms.DataGridViewTextBoxColumn Product_Name;
    private System.Windows.Forms.Button btnHookedObjects;
    private System.Windows.Forms.Label lblDebugFile;
    private System.Windows.Forms.CheckBox chkNotifyPropertyChanges2;
    private System.Windows.Forms.CheckBox chkNotifyPropertyChanges;
    private System.Windows.Forms.CheckBox chkConsiderOnlyDetails;


}


