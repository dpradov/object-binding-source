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
        Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand5 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Customer", -1);
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn26 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CustomerId");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn27 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Name");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn28 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("BillingAddress");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn29 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Self");
        Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand6 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Product", -1);
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn30 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ProductId");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn31 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Name");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn32 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UnitPrice");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn33 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Type");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn34 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Self");
        Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
        Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand7 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Order", -1);
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn35 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OrderNumber");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn36 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DeliveryAddress");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn37 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OrderDate");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn38 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Customer", -1, "uddCustomer");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn39 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OrderLines");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn40 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Customer_Name", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Ascending, false);
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn41 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Customer_BillingAddress_StreetAddress");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn42 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Customer_BillingAddress_City");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn43 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DeliveryAddress_StreetAddress");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn44 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DeliveryAddress_City");
        Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand8 = new Infragistics.Win.UltraWinGrid.UltraGridBand("OrderLines", 0);
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn45 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Product", -1, "uddProduct");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn46 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Quantity");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn47 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Product_ProductId");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn48 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Product_UnitPrice", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Ascending, false);
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn49 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Product_Name");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn50 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Product_Type");
        Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
        Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
        Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
        Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
        Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
        Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
        Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
        Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
        Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
        Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
        Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
        this.customersBindingSource = new UI.ObjectBindingSource.ObjectBindingSource(this.components);
        this.ordersBindingSource = new UI.ObjectBindingSource.ObjectBindingSource(this.components);
        this.orderlinesBindingSource = new UI.ObjectBindingSource.ObjectBindingSource(this.components);
        this.dataGridViewComboBoxColumn1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
        this.dataGridViewComboBoxColumn2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
        this.dataGridViewComboBoxColumn3 = new System.Windows.Forms.DataGridViewComboBoxColumn();
        this.dataGridViewComboBoxColumn4 = new System.Windows.Forms.DataGridViewComboBoxColumn();
        this.dataGridViewComboBoxColumn5 = new System.Windows.Forms.DataGridViewComboBoxColumn();
        this.dataGridViewComboBoxColumn6 = new System.Windows.Forms.DataGridViewComboBoxColumn();
        this.dataGridViewComboBoxColumn7 = new System.Windows.Forms.DataGridViewComboBoxColumn();
        this.dataGridViewComboBoxColumn8 = new System.Windows.Forms.DataGridViewComboBoxColumn();
        this.dataGridViewComboBoxColumn9 = new System.Windows.Forms.DataGridViewComboBoxColumn();
        this.dataGridViewComboBoxColumn10 = new System.Windows.Forms.DataGridViewComboBoxColumn();
        this.dataGridViewComboBoxColumn11 = new System.Windows.Forms.DataGridViewComboBoxColumn();
        this.dataGridViewComboBoxColumn12 = new System.Windows.Forms.DataGridViewComboBoxColumn();
        this.dataGridViewComboBoxColumn13 = new System.Windows.Forms.DataGridViewComboBoxColumn();
        this.dataGridViewComboBoxColumn14 = new System.Windows.Forms.DataGridViewComboBoxColumn();
        this.dataGridViewComboBoxColumn15 = new System.Windows.Forms.DataGridViewComboBoxColumn();
        this.dataGridViewComboBoxColumn16 = new System.Windows.Forms.DataGridViewComboBoxColumn();
        this.dataGridViewComboBoxColumn17 = new System.Windows.Forms.DataGridViewComboBoxColumn();
        this.dataGridViewComboBoxColumn18 = new System.Windows.Forms.DataGridViewComboBoxColumn();
        this.dataGridViewComboBoxColumn19 = new System.Windows.Forms.DataGridViewComboBoxColumn();
        this.productsBindingSource = new UI.ObjectBindingSource.ObjectBindingSource(this.components);
        this.btnDeleteAll = new System.Windows.Forms.Button();
        this.btnChange2 = new System.Windows.Forms.Button();
        this.dataGridViewComboBoxColumn20 = new System.Windows.Forms.DataGridViewComboBoxColumn();
        this.dataGridViewComboBoxColumn21 = new System.Windows.Forms.DataGridViewComboBoxColumn();
        this.dataGridViewComboBoxColumn22 = new System.Windows.Forms.DataGridViewComboBoxColumn();
        this.dataGridViewComboBoxColumn23 = new System.Windows.Forms.DataGridViewComboBoxColumn();
        this.dataGridViewComboBoxColumn24 = new System.Windows.Forms.DataGridViewComboBoxColumn();
        this.dataGridViewComboBoxColumn25 = new System.Windows.Forms.DataGridViewComboBoxColumn();
        this.btnChange1 = new System.Windows.Forms.Button();
        this.dataGridViewComboBoxColumn26 = new System.Windows.Forms.DataGridViewComboBoxColumn();
        this.textBox1 = new System.Windows.Forms.TextBox();
        this.label1 = new System.Windows.Forms.Label();
        this.dataGridViewComboBoxColumn27 = new System.Windows.Forms.DataGridViewComboBoxColumn();
        this.btnDeleteCustomer = new System.Windows.Forms.Button();
        this.uddCustomer = new Infragistics.Win.UltraWinGrid.UltraDropDown();
        this.uddProduct = new Infragistics.Win.UltraWinGrid.UltraDropDown();
        this.ultraGrid1 = new Infragistics.Win.UltraWinGrid.UltraGrid();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.btnChange3 = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)(this.customersBindingSource)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.ordersBindingSource)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.orderlinesBindingSource)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.productsBindingSource)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.uddCustomer)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.uddProduct)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).BeginInit();
        this.groupBox1.SuspendLayout();
        this.SuspendLayout();
        // 
        // customersBindingSource
        // 
        this.customersBindingSource.AutoCreateObjects = false;
        this.customersBindingSource.BindableNestedProperties = new string[0];
        this.customersBindingSource.DataSource = typeof(Customer);
        this.customersBindingSource.NotifyChangesInNestedPropertiesFromChildlists = false;
        this.customersBindingSource.NotifyListChangesFromNestedBindingSources = false;
        this.customersBindingSource.RelatedObjectBindingSources = new UI.ObjectBindingSource.ObjectBindingSource[0];
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
        this.ordersBindingSource.DataSource = typeof(Order);
        this.ordersBindingSource.NotifyChangesInNestedPropertiesFromChildlists = true;
        this.ordersBindingSource.NotifyListChangesFromNestedBindingSources = false;
        this.ordersBindingSource.RelatedObjectBindingSources = new UI.ObjectBindingSource.ObjectBindingSource[] {
        this.orderlinesBindingSource};
        this.ordersBindingSource.ListChangedOnChildList += new UI.ObjectBindingSource.ObjectBindingSource.ListChangedOnChildListEventHandler(this.ordersBindingSource_ListChangedOnChildList);
        this.ordersBindingSource.CreatingObject += new UI.ObjectBindingSource.ObjectBindingSource.CreatingObjectEventHandler(this.ordersBindingSource_CreatingObject);
        // 
        // orderlinesBindingSource
        // 
        this.orderlinesBindingSource.AllowNew = true;
        this.orderlinesBindingSource.AutoCreateObjects = false;
        this.orderlinesBindingSource.BindableNestedProperties = new string[] {
        "Product.ProductId",
        "Product.UnitPrice",
        "Product.Name",
        "Product.Type"};
        this.orderlinesBindingSource.DataSource = typeof(OrderLine);
        this.orderlinesBindingSource.NotifyChangesInNestedPropertiesFromChildlists = false;
        this.orderlinesBindingSource.NotifyListChangesFromNestedBindingSources = false;
        this.orderlinesBindingSource.RelatedObjectBindingSources = new UI.ObjectBindingSource.ObjectBindingSource[0];
        // 
        // dataGridViewComboBoxColumn1
        // 
        this.dataGridViewComboBoxColumn1.DataPropertyName = "Customer";
        this.dataGridViewComboBoxColumn1.HeaderText = "Customer";
        this.dataGridViewComboBoxColumn1.Name = "dataGridViewComboBoxColumn1";
        this.dataGridViewComboBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewComboBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        // 
        // dataGridViewComboBoxColumn2
        // 
        this.dataGridViewComboBoxColumn2.DataPropertyName = "Customer";
        this.dataGridViewComboBoxColumn2.HeaderText = "Customer";
        this.dataGridViewComboBoxColumn2.Name = "dataGridViewComboBoxColumn2";
        this.dataGridViewComboBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewComboBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        // 
        // dataGridViewComboBoxColumn3
        // 
        this.dataGridViewComboBoxColumn3.DataPropertyName = "Customer";
        this.dataGridViewComboBoxColumn3.HeaderText = "Customer";
        this.dataGridViewComboBoxColumn3.Name = "dataGridViewComboBoxColumn3";
        this.dataGridViewComboBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewComboBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        // 
        // dataGridViewComboBoxColumn4
        // 
        this.dataGridViewComboBoxColumn4.DataPropertyName = "Customer";
        this.dataGridViewComboBoxColumn4.HeaderText = "Customer";
        this.dataGridViewComboBoxColumn4.Name = "dataGridViewComboBoxColumn4";
        this.dataGridViewComboBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewComboBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        // 
        // dataGridViewComboBoxColumn5
        // 
        this.dataGridViewComboBoxColumn5.DataPropertyName = "Customer";
        this.dataGridViewComboBoxColumn5.HeaderText = "Customer";
        this.dataGridViewComboBoxColumn5.Name = "dataGridViewComboBoxColumn5";
        this.dataGridViewComboBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewComboBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        // 
        // dataGridViewComboBoxColumn6
        // 
        this.dataGridViewComboBoxColumn6.DataPropertyName = "Customer";
        this.dataGridViewComboBoxColumn6.HeaderText = "Customer";
        this.dataGridViewComboBoxColumn6.Name = "dataGridViewComboBoxColumn6";
        this.dataGridViewComboBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewComboBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        // 
        // dataGridViewComboBoxColumn7
        // 
        this.dataGridViewComboBoxColumn7.DataPropertyName = "Customer";
        this.dataGridViewComboBoxColumn7.HeaderText = "Customer";
        this.dataGridViewComboBoxColumn7.Name = "dataGridViewComboBoxColumn7";
        this.dataGridViewComboBoxColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewComboBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        // 
        // dataGridViewComboBoxColumn8
        // 
        this.dataGridViewComboBoxColumn8.DataPropertyName = "Product";
        this.dataGridViewComboBoxColumn8.HeaderText = "Product";
        this.dataGridViewComboBoxColumn8.Name = "dataGridViewComboBoxColumn8";
        this.dataGridViewComboBoxColumn8.Resizable = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewComboBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        // 
        // dataGridViewComboBoxColumn9
        // 
        this.dataGridViewComboBoxColumn9.DataPropertyName = "Customer";
        this.dataGridViewComboBoxColumn9.HeaderText = "Customer";
        this.dataGridViewComboBoxColumn9.Name = "dataGridViewComboBoxColumn9";
        this.dataGridViewComboBoxColumn9.Resizable = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewComboBoxColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        // 
        // dataGridViewComboBoxColumn10
        // 
        this.dataGridViewComboBoxColumn10.DataPropertyName = "Customer";
        this.dataGridViewComboBoxColumn10.HeaderText = "Customer";
        this.dataGridViewComboBoxColumn10.Name = "dataGridViewComboBoxColumn10";
        this.dataGridViewComboBoxColumn10.Resizable = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewComboBoxColumn10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        // 
        // dataGridViewComboBoxColumn11
        // 
        this.dataGridViewComboBoxColumn11.DataPropertyName = "Customer";
        this.dataGridViewComboBoxColumn11.HeaderText = "Customer";
        this.dataGridViewComboBoxColumn11.Name = "dataGridViewComboBoxColumn11";
        this.dataGridViewComboBoxColumn11.Resizable = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewComboBoxColumn11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        // 
        // dataGridViewComboBoxColumn12
        // 
        this.dataGridViewComboBoxColumn12.DataPropertyName = "Product";
        this.dataGridViewComboBoxColumn12.HeaderText = "Product";
        this.dataGridViewComboBoxColumn12.Name = "dataGridViewComboBoxColumn12";
        this.dataGridViewComboBoxColumn12.Resizable = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewComboBoxColumn12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        // 
        // dataGridViewComboBoxColumn13
        // 
        this.dataGridViewComboBoxColumn13.DataPropertyName = "Customer";
        this.dataGridViewComboBoxColumn13.HeaderText = "Customer";
        this.dataGridViewComboBoxColumn13.Name = "dataGridViewComboBoxColumn13";
        this.dataGridViewComboBoxColumn13.Resizable = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewComboBoxColumn13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        // 
        // dataGridViewComboBoxColumn14
        // 
        this.dataGridViewComboBoxColumn14.DataPropertyName = "Customer";
        this.dataGridViewComboBoxColumn14.HeaderText = "Customer";
        this.dataGridViewComboBoxColumn14.Name = "dataGridViewComboBoxColumn14";
        this.dataGridViewComboBoxColumn14.Resizable = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewComboBoxColumn14.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        // 
        // dataGridViewComboBoxColumn15
        // 
        this.dataGridViewComboBoxColumn15.DataPropertyName = "Customer";
        this.dataGridViewComboBoxColumn15.HeaderText = "Customer";
        this.dataGridViewComboBoxColumn15.Name = "dataGridViewComboBoxColumn15";
        this.dataGridViewComboBoxColumn15.Resizable = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewComboBoxColumn15.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        // 
        // dataGridViewComboBoxColumn16
        // 
        this.dataGridViewComboBoxColumn16.DataPropertyName = "Product";
        this.dataGridViewComboBoxColumn16.HeaderText = "Product";
        this.dataGridViewComboBoxColumn16.Name = "dataGridViewComboBoxColumn16";
        this.dataGridViewComboBoxColumn16.Resizable = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewComboBoxColumn16.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        // 
        // dataGridViewComboBoxColumn17
        // 
        this.dataGridViewComboBoxColumn17.DataPropertyName = "Customer";
        this.dataGridViewComboBoxColumn17.HeaderText = "Customer";
        this.dataGridViewComboBoxColumn17.Name = "dataGridViewComboBoxColumn17";
        this.dataGridViewComboBoxColumn17.Resizable = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewComboBoxColumn17.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        // 
        // dataGridViewComboBoxColumn18
        // 
        this.dataGridViewComboBoxColumn18.DataPropertyName = "Customer";
        this.dataGridViewComboBoxColumn18.HeaderText = "Customer";
        this.dataGridViewComboBoxColumn18.Name = "dataGridViewComboBoxColumn18";
        this.dataGridViewComboBoxColumn18.Resizable = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewComboBoxColumn18.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        // 
        // dataGridViewComboBoxColumn19
        // 
        this.dataGridViewComboBoxColumn19.DataPropertyName = "Product";
        this.dataGridViewComboBoxColumn19.HeaderText = "Product";
        this.dataGridViewComboBoxColumn19.Name = "dataGridViewComboBoxColumn19";
        this.dataGridViewComboBoxColumn19.Resizable = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewComboBoxColumn19.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        // 
        // productsBindingSource
        // 
        this.productsBindingSource.AutoCreateObjects = false;
        this.productsBindingSource.BindableNestedProperties = new string[0];
        this.productsBindingSource.DataSource = typeof(Product);
        this.productsBindingSource.NotifyChangesInNestedPropertiesFromChildlists = false;
        this.productsBindingSource.NotifyListChangesFromNestedBindingSources = false;
        this.productsBindingSource.RelatedObjectBindingSources = new UI.ObjectBindingSource.ObjectBindingSource[0];
        // 
        // btnDeleteAll
        // 
        this.btnDeleteAll.Location = new System.Drawing.Point(18, 12);
        this.btnDeleteAll.Name = "btnDeleteAll";
        this.btnDeleteAll.Size = new System.Drawing.Size(108, 23);
        this.btnDeleteAll.TabIndex = 5;
        this.btnDeleteAll.Text = "Delete All";
        this.btnDeleteAll.UseVisualStyleBackColor = true;
        this.btnDeleteAll.Click += new System.EventHandler(this.DeleteAll_Click);
        // 
        // btnChange2
        // 
        this.btnChange2.Location = new System.Drawing.Point(800, 12);
        this.btnChange2.Name = "btnChange2";
        this.btnChange2.Size = new System.Drawing.Size(83, 23);
        this.btnChange2.TabIndex = 4;
        this.btnChange2.Text = "Change 2";
        this.btnChange2.UseVisualStyleBackColor = true;
        this.btnChange2.Click += new System.EventHandler(this.btnChange2_Click);
        // 
        // dataGridViewComboBoxColumn20
        // 
        this.dataGridViewComboBoxColumn20.DataPropertyName = "Product";
        this.dataGridViewComboBoxColumn20.HeaderText = "Product";
        this.dataGridViewComboBoxColumn20.Name = "dataGridViewComboBoxColumn20";
        this.dataGridViewComboBoxColumn20.Resizable = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewComboBoxColumn20.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        // 
        // dataGridViewComboBoxColumn21
        // 
        this.dataGridViewComboBoxColumn21.DataPropertyName = "Product";
        this.dataGridViewComboBoxColumn21.HeaderText = "Product";
        this.dataGridViewComboBoxColumn21.Name = "dataGridViewComboBoxColumn21";
        this.dataGridViewComboBoxColumn21.Resizable = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewComboBoxColumn21.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        // 
        // dataGridViewComboBoxColumn22
        // 
        this.dataGridViewComboBoxColumn22.DataPropertyName = "Customer";
        this.dataGridViewComboBoxColumn22.HeaderText = "Customer";
        this.dataGridViewComboBoxColumn22.Name = "dataGridViewComboBoxColumn22";
        this.dataGridViewComboBoxColumn22.Resizable = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewComboBoxColumn22.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        // 
        // dataGridViewComboBoxColumn23
        // 
        this.dataGridViewComboBoxColumn23.DataPropertyName = "Customer";
        this.dataGridViewComboBoxColumn23.HeaderText = "Customer";
        this.dataGridViewComboBoxColumn23.Name = "dataGridViewComboBoxColumn23";
        this.dataGridViewComboBoxColumn23.Resizable = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewComboBoxColumn23.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        // 
        // dataGridViewComboBoxColumn24
        // 
        this.dataGridViewComboBoxColumn24.DataPropertyName = "Customer";
        this.dataGridViewComboBoxColumn24.HeaderText = "Customer";
        this.dataGridViewComboBoxColumn24.Name = "dataGridViewComboBoxColumn24";
        this.dataGridViewComboBoxColumn24.Resizable = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewComboBoxColumn24.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        // 
        // dataGridViewComboBoxColumn25
        // 
        this.dataGridViewComboBoxColumn25.DataPropertyName = "Customer";
        this.dataGridViewComboBoxColumn25.HeaderText = "Customer";
        this.dataGridViewComboBoxColumn25.Name = "dataGridViewComboBoxColumn25";
        this.dataGridViewComboBoxColumn25.Resizable = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewComboBoxColumn25.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        // 
        // btnChange1
        // 
        this.btnChange1.Location = new System.Drawing.Point(713, 12);
        this.btnChange1.Name = "btnChange1";
        this.btnChange1.Size = new System.Drawing.Size(79, 23);
        this.btnChange1.TabIndex = 6;
        this.btnChange1.Text = "Change 1";
        this.btnChange1.UseVisualStyleBackColor = true;
        this.btnChange1.Click += new System.EventHandler(this.btnChange1_Click);
        // 
        // dataGridViewComboBoxColumn26
        // 
        this.dataGridViewComboBoxColumn26.DataPropertyName = "Product";
        this.dataGridViewComboBoxColumn26.HeaderText = "Product";
        this.dataGridViewComboBoxColumn26.Name = "dataGridViewComboBoxColumn26";
        this.dataGridViewComboBoxColumn26.Resizable = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewComboBoxColumn26.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        // 
        // textBox1
        // 
        this.textBox1.Location = new System.Drawing.Point(343, 14);
        this.textBox1.Name = "textBox1";
        this.textBox1.Size = new System.Drawing.Size(31, 20);
        this.textBox1.TabIndex = 7;
        this.textBox1.Validated += new System.EventHandler(this.textBox1_Validated);
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(245, 17);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(97, 13);
        this.label1.TabIndex = 8;
        this.label1.Text = "Max. Debug Level:";
        // 
        // dataGridViewComboBoxColumn27
        // 
        this.dataGridViewComboBoxColumn27.DataPropertyName = "Customer";
        this.dataGridViewComboBoxColumn27.HeaderText = "Customer";
        this.dataGridViewComboBoxColumn27.Name = "dataGridViewComboBoxColumn27";
        this.dataGridViewComboBoxColumn27.Resizable = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewComboBoxColumn27.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        // 
        // btnDeleteCustomer
        // 
        this.btnDeleteCustomer.Location = new System.Drawing.Point(132, 11);
        this.btnDeleteCustomer.Name = "btnDeleteCustomer";
        this.btnDeleteCustomer.Size = new System.Drawing.Size(108, 23);
        this.btnDeleteCustomer.TabIndex = 9;
        this.btnDeleteCustomer.Text = "Delete Customer[0]";
        this.btnDeleteCustomer.UseVisualStyleBackColor = true;
        this.btnDeleteCustomer.Click += new System.EventHandler(this.btnDeleteCustomer_Click);
        // 
        // uddCustomer
        // 
        this.uddCustomer.DataSource = this.customersBindingSource;
        ultraGridColumn26.Header.VisiblePosition = 0;
        ultraGridColumn27.Header.VisiblePosition = 1;
        ultraGridColumn28.Header.VisiblePosition = 2;
        ultraGridColumn28.Hidden = true;
        ultraGridColumn29.Header.VisiblePosition = 3;
        ultraGridColumn29.Hidden = true;
        ultraGridBand5.Columns.AddRange(new object[] {
            ultraGridColumn26,
            ultraGridColumn27,
            ultraGridColumn28,
            ultraGridColumn29});
        this.uddCustomer.DisplayLayout.BandsSerializer.Add(ultraGridBand5);
        this.uddCustomer.DisplayMember = "Name";
        this.uddCustomer.Location = new System.Drawing.Point(389, 15);
        this.uddCustomer.Name = "uddCustomer";
        this.uddCustomer.Size = new System.Drawing.Size(128, 24);
        this.uddCustomer.TabIndex = 11;
        this.uddCustomer.ValueMember = "Self";
        this.uddCustomer.Visible = false;
        // 
        // uddProduct
        // 
        this.uddProduct.DataSource = this.productsBindingSource;
        ultraGridColumn30.Header.VisiblePosition = 0;
        ultraGridColumn31.Header.VisiblePosition = 1;
        ultraGridColumn32.Header.VisiblePosition = 2;
        ultraGridColumn32.Hidden = true;
        ultraGridColumn33.Header.VisiblePosition = 3;
        ultraGridColumn33.Hidden = true;
        ultraGridColumn34.Header.VisiblePosition = 4;
        ultraGridColumn34.Hidden = true;
        ultraGridBand6.Columns.AddRange(new object[] {
            ultraGridColumn30,
            ultraGridColumn31,
            ultraGridColumn32,
            ultraGridColumn33,
            ultraGridColumn34});
        this.uddProduct.DisplayLayout.BandsSerializer.Add(ultraGridBand6);
        this.uddProduct.DisplayMember = "Name";
        this.uddProduct.Location = new System.Drawing.Point(523, 14);
        this.uddProduct.Name = "uddProduct";
        this.uddProduct.Size = new System.Drawing.Size(128, 24);
        this.uddProduct.TabIndex = 12;
        this.uddProduct.ValueMember = "Self";
        this.uddProduct.Visible = false;
        // 
        // ultraGrid1
        // 
        this.ultraGrid1.DataSource = this.ordersBindingSource;
        appearance13.BackColor = System.Drawing.SystemColors.Window;
        appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
        this.ultraGrid1.DisplayLayout.Appearance = appearance13;
        ultraGridColumn35.Header.VisiblePosition = 0;
        ultraGridColumn36.Header.VisiblePosition = 1;
        ultraGridColumn37.Header.VisiblePosition = 2;
        ultraGridColumn38.Header.VisiblePosition = 3;
        ultraGridColumn38.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDown;
        ultraGridColumn39.Header.VisiblePosition = 4;
        ultraGridColumn40.Header.Caption = "Customer Name";
        ultraGridColumn40.Header.VisiblePosition = 5;
        ultraGridColumn41.Header.Caption = "Customer BillingAddress StreetAddress";
        ultraGridColumn41.Header.VisiblePosition = 6;
        ultraGridColumn42.Header.Caption = "Customer BillingAddress City";
        ultraGridColumn42.Header.VisiblePosition = 7;
        ultraGridColumn43.Header.Caption = "DeliveryAddress StreetAddress";
        ultraGridColumn43.Header.VisiblePosition = 8;
        ultraGridColumn44.Header.Caption = "DeliveryAddress City";
        ultraGridColumn44.Header.VisiblePosition = 9;
        ultraGridBand7.Columns.AddRange(new object[] {
            ultraGridColumn35,
            ultraGridColumn36,
            ultraGridColumn37,
            ultraGridColumn38,
            ultraGridColumn39,
            ultraGridColumn40,
            ultraGridColumn41,
            ultraGridColumn42,
            ultraGridColumn43,
            ultraGridColumn44});
        ultraGridColumn45.Header.VisiblePosition = 0;
        ultraGridColumn45.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDown;
        ultraGridColumn46.Header.VisiblePosition = 1;
        ultraGridColumn47.Header.Caption = "ProductId";
        ultraGridColumn47.Header.VisiblePosition = 2;
        ultraGridColumn48.Header.Caption = "UnitPrice";
        ultraGridColumn48.Header.VisiblePosition = 3;
        ultraGridColumn49.Header.Caption = "Product Name";
        ultraGridColumn49.Header.VisiblePosition = 4;
        ultraGridColumn50.Header.Caption = "Product Type";
        ultraGridColumn50.Header.VisiblePosition = 5;
        ultraGridBand8.Columns.AddRange(new object[] {
            ultraGridColumn45,
            ultraGridColumn46,
            ultraGridColumn47,
            ultraGridColumn48,
            ultraGridColumn49,
            ultraGridColumn50});
        this.ultraGrid1.DisplayLayout.BandsSerializer.Add(ultraGridBand7);
        this.ultraGrid1.DisplayLayout.BandsSerializer.Add(ultraGridBand8);
        this.ultraGrid1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
        this.ultraGrid1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
        appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
        appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
        appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
        appearance14.BorderColor = System.Drawing.SystemColors.Window;
        this.ultraGrid1.DisplayLayout.GroupByBox.Appearance = appearance14;
        appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
        this.ultraGrid1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
        this.ultraGrid1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
        appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
        appearance16.BackColor2 = System.Drawing.SystemColors.Control;
        appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
        appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
        this.ultraGrid1.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
        this.ultraGrid1.DisplayLayout.MaxColScrollRegions = 1;
        this.ultraGrid1.DisplayLayout.MaxRowScrollRegions = 1;
        appearance17.BackColor = System.Drawing.SystemColors.Window;
        appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
        this.ultraGrid1.DisplayLayout.Override.ActiveCellAppearance = appearance17;
        appearance18.BackColor = System.Drawing.SystemColors.Highlight;
        appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
        this.ultraGrid1.DisplayLayout.Override.ActiveRowAppearance = appearance18;
        this.ultraGrid1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
        this.ultraGrid1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
        appearance19.BackColor = System.Drawing.SystemColors.Window;
        this.ultraGrid1.DisplayLayout.Override.CardAreaAppearance = appearance19;
        appearance20.BorderColor = System.Drawing.Color.Silver;
        appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
        this.ultraGrid1.DisplayLayout.Override.CellAppearance = appearance20;
        this.ultraGrid1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
        this.ultraGrid1.DisplayLayout.Override.CellPadding = 0;
        appearance21.BackColor = System.Drawing.SystemColors.Control;
        appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
        appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
        appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
        appearance21.BorderColor = System.Drawing.SystemColors.Window;
        this.ultraGrid1.DisplayLayout.Override.GroupByRowAppearance = appearance21;
        appearance22.TextHAlignAsString = "Left";
        this.ultraGrid1.DisplayLayout.Override.HeaderAppearance = appearance22;
        this.ultraGrid1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
        this.ultraGrid1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
        appearance23.BackColor = System.Drawing.SystemColors.Window;
        appearance23.BorderColor = System.Drawing.Color.Silver;
        this.ultraGrid1.DisplayLayout.Override.RowAppearance = appearance23;
        this.ultraGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
        appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
        this.ultraGrid1.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
        this.ultraGrid1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
        this.ultraGrid1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
        this.ultraGrid1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
        this.ultraGrid1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.ultraGrid1.Location = new System.Drawing.Point(6, 19);
        this.ultraGrid1.Name = "ultraGrid1";
        this.ultraGrid1.Size = new System.Drawing.Size(950, 442);
        this.ultraGrid1.TabIndex = 10;
        this.ultraGrid1.Text = "ultraGrid1";
        // 
        // groupBox1
        // 
        this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.groupBox1.Controls.Add(this.ultraGrid1);
        this.groupBox1.Location = new System.Drawing.Point(12, 45);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(968, 467);
        this.groupBox1.TabIndex = 2;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "Orders";
        // 
        // btnChange3
        // 
        this.btnChange3.Location = new System.Drawing.Point(889, 12);
        this.btnChange3.Name = "btnChange3";
        this.btnChange3.Size = new System.Drawing.Size(79, 23);
        this.btnChange3.TabIndex = 13;
        this.btnChange3.Text = "Change 3";
        this.btnChange3.UseVisualStyleBackColor = true;
        this.btnChange3.Click += new System.EventHandler(this.btnChange3_Click);
        // 
        // MainForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.WhiteSmoke;
        this.ClientSize = new System.Drawing.Size(992, 545);
        this.Controls.Add(this.btnChange3);
        this.Controls.Add(this.uddProduct);
        this.Controls.Add(this.uddCustomer);
        this.Controls.Add(this.btnDeleteCustomer);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.textBox1);
        this.Controls.Add(this.btnChange1);
        this.Controls.Add(this.btnChange2);
        this.Controls.Add(this.btnDeleteAll);
        this.Controls.Add(this.groupBox1);
        this.Name = "MainForm";
        this.Text = "ObjectBindingSource Demo (Nested Property Binding)";
        ((System.ComponentModel.ISupportInitialize)(this.customersBindingSource)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.ordersBindingSource)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.orderlinesBindingSource)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.productsBindingSource)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.uddCustomer)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.uddProduct)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).EndInit();
        this.groupBox1.ResumeLayout(false);
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion


    private UI.ObjectBindingSource.ObjectBindingSource  ordersBindingSource;
    private UI.ObjectBindingSource.ObjectBindingSource orderlinesBindingSource;
    private UI.ObjectBindingSource.ObjectBindingSource customersBindingSource;
    private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn1;
    private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn2;
    private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn3;
    private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn4;
    private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn5;
    private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn6;
    private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn7;
    private UI.ObjectBindingSource.ObjectBindingSource productsBindingSource;
    private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn8;
    private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn9;
    private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn10;
    private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn11;
    private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn12;
    private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn13;
    private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn14;
    private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn15;
    private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn16;
    private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn17;
    private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn18;
    private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn19;
    private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn20;
    private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn21;
    private System.Windows.Forms.Button btnChange2;
    private System.Windows.Forms.Button btnDeleteAll;
    private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn22;
    private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn23;
    private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn24;
    private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn25;
    private System.Windows.Forms.Button btnChange1;
    private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn26;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn27;
    private System.Windows.Forms.Button btnDeleteCustomer;
    internal Infragistics.Win.UltraWinGrid.UltraDropDown uddCustomer;
    internal Infragistics.Win.UltraWinGrid.UltraDropDown uddProduct;
    private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid1;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Button btnChange3;
    

}


