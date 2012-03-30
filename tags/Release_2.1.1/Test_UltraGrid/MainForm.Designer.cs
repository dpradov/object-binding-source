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
        Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
        Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Order", -1);
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn25 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OrderNumber", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Ascending, false);
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn26 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DeliveryAddress");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn27 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OrderDate");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn28 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Customer", -1, "uddCustomer");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn29 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OrderLines");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn56 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Customer_Name");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn57 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Customer_BillingAddress_StreetAddress");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn58 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Customer_BillingAddress_City");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn59 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DeliveryAddress_StreetAddress");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn60 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DeliveryAddress_City");
        Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("OrderLines", 0);
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn30 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Product", -1, "uddProduct");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn31 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Quantity");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn32 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Details");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn33 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OtherDetails");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn34 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Product_ProductId");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn35 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Product_UnitPrice");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn36 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Product_Name");
        Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand3 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Details", 1);
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn37 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RecomendedBy");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn38 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NotShownProperty");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn39 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RecomendedBy_CustomerId");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn40 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RecomendedBy_Name");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn41 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RecomendedBy_FavoriteProduct_Name");
        Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand4 = new Infragistics.Win.UltraWinGrid.UltraGridBand("OtherDetails", 1);
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn42 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RecomendedBy");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn43 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NotShownProperty");
        Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
        Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
        Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
        Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
        Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
        Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
        Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
        Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
        Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
        Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
        Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
        Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
        Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand5 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Order", -1);
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OrderNumber", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Ascending, false);
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DeliveryAddress");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OrderDate");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Customer", -1, "uddCustomer");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OrderLines");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Customer_Name");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Customer_BillingAddress_StreetAddress");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Customer_BillingAddress_City");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DeliveryAddress_StreetAddress");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DeliveryAddress_City");
        Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand6 = new Infragistics.Win.UltraWinGrid.UltraGridBand("OrderLines", 0);
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Product", -1, "uddProduct");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Quantity");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Details");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OtherDetails");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Product_ProductId");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Product_UnitPrice");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Product_Name");
        Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand7 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Details", 1);
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RecomendedBy");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NotShownProperty");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RecomendedBy_CustomerId");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RecomendedBy_Name");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RecomendedBy_FavoriteProduct_Name");
        Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand8 = new Infragistics.Win.UltraWinGrid.UltraGridBand("OtherDetails", 1);
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn23 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RecomendedBy");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn24 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NotShownProperty");
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
        Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand9 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Customer", -1);
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn44 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CustomerId");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn45 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Name");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn46 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("BillingAddress");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn47 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Age");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn48 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FavoriteProduct");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn49 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Self");
        Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand10 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Product", -1);
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn50 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ProductId");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn51 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Name");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn52 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UnitPrice");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn53 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Type");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn54 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OtherProperty");
        Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn55 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Self");
        this.textBox1 = new System.Windows.Forms.TextBox();
        this.label1 = new System.Windows.Forms.Label();
        this.btnObjectsAlive = new System.Windows.Forms.Button();
        this.cbAction = new System.Windows.Forms.ComboBox();
        this.btnGC = new System.Windows.Forms.Button();
        this.btnChanges = new System.Windows.Forms.Button();
        this.chkAutoCreateObjects = new System.Windows.Forms.CheckBox();
        this.chkNotifyChangesChildLists = new System.Windows.Forms.CheckBox();
        this.splitContainer1 = new System.Windows.Forms.SplitContainer();
        this.chkNotifyPropertyChanges = new System.Windows.Forms.CheckBox();
        this.ChkConsiderChildsOnlyInCurrent = new System.Windows.Forms.CheckBox();
        this.label2 = new System.Windows.Forms.Label();
        this.ultraGrid1 = new Infragistics.Win.UltraWinGrid.UltraGrid();
        this.ordersBindingSource = new UI.ObjectBindingSource.ObjectBindingSource(this.components);
        this.orderlinesBindingSource = new UI.ObjectBindingSource.ObjectBindingSource(this.components);
        this.detailOrderLinesBindingSource = new UI.ObjectBindingSource.ObjectBindingSource(this.components);
        this.chkNotifyPropertyChanges2 = new System.Windows.Forms.CheckBox();
        this.ChkConsiderChildsOnlyInCurrent2 = new System.Windows.Forms.CheckBox();
        this.label3 = new System.Windows.Forms.Label();
        this.chkAutoCreateObjects2 = new System.Windows.Forms.CheckBox();
        this.ultraGrid2 = new Infragistics.Win.UltraWinGrid.UltraGrid();
        this.ordersBindingSource_2 = new UI.ObjectBindingSource.ObjectBindingSource(this.components);
        this.chkNotifyChangesChildLists2 = new System.Windows.Forms.CheckBox();
        this.chkConsiderOnlyDetails = new System.Windows.Forms.CheckBox();
        this.btnShowOBS = new System.Windows.Forms.Button();
        this.chkConsiderNonNested_Product = new System.Windows.Forms.CheckBox();
        this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
        this.uddCustomer = new Infragistics.Win.UltraWinGrid.UltraDropDown();
        this.customersBindingSource = new UI.ObjectBindingSource.ObjectBindingSource(this.components);
        this.uddProduct = new Infragistics.Win.UltraWinGrid.UltraDropDown();
        this.productsBindingSource = new UI.ObjectBindingSource.ObjectBindingSource(this.components);
        this.splitContainer1.Panel1.SuspendLayout();
        this.splitContainer1.Panel2.SuspendLayout();
        this.splitContainer1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.ordersBindingSource)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.orderlinesBindingSource)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.detailOrderLinesBindingSource)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.ultraGrid2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.ordersBindingSource_2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.uddCustomer)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.customersBindingSource)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.uddProduct)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.productsBindingSource)).BeginInit();
        this.SuspendLayout();
        // 
        // textBox1
        // 
        this.textBox1.Location = new System.Drawing.Point(114, 12);
        this.textBox1.Name = "textBox1";
        this.textBox1.Size = new System.Drawing.Size(31, 20);
        this.textBox1.TabIndex = 7;
        this.textBox1.Validated += new System.EventHandler(this.textBox1_Validated);
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(16, 15);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(97, 13);
        this.label1.TabIndex = 8;
        this.label1.Text = "Max. Debug Level:";
        // 
        // btnObjectsAlive
        // 
        this.btnObjectsAlive.Location = new System.Drawing.Point(153, 10);
        this.btnObjectsAlive.Name = "btnObjectsAlive";
        this.btnObjectsAlive.Size = new System.Drawing.Size(108, 23);
        this.btnObjectsAlive.TabIndex = 20;
        this.btnObjectsAlive.Text = "Show Objects Alive";
        this.btnObjectsAlive.UseVisualStyleBackColor = true;
        this.btnObjectsAlive.Click += new System.EventHandler(this.btnObjectsAlive_Click);
        // 
        // cbAction
        // 
        this.cbAction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            "-----",
            "15: MOD ordersBindingSource.BindableNestedProperties: A",
            "16: MOD ordersBindingSource.BindableNestedProperties: B",
            "17: MOD orderlinesBindingSource.BindableNestedProperties: A",
            "25: MOD orderlinesBindingSource.BindableNestedProperties: B",
            "--------",
            "23: MOD ordersBindingSource.BindableNestedProperties: C",
            "-----------------",
            "26: MOD orderlinesBindingSource.BindableNestedProperties: C",
            "------",
            "21: Orders[0].OrderLines = Orders[0].OrderLines",
            "22: Orders[0].OrderLines = Orders[1].OrderLines",
            "-----------",
            "18: Refresh ultraGrid1.Rows[0].ChildBands[0].Rows",
            "19: Refresh ultraGrid1.Rows[1].ChildBands[0].Rows",
            "-----------------",
            " 9: Remove <Samantha Brown>",
            "10: Remove <LapTop>",
            "11: Delete All",
            "12: BindingSourceS.Dispose"});
        this.cbAction.Location = new System.Drawing.Point(472, 11);
        this.cbAction.Name = "cbAction";
        this.cbAction.Size = new System.Drawing.Size(444, 21);
        this.cbAction.TabIndex = 18;
        // 
        // btnGC
        // 
        this.btnGC.Location = new System.Drawing.Point(381, 9);
        this.btnGC.Name = "btnGC";
        this.btnGC.Size = new System.Drawing.Size(75, 23);
        this.btnGC.TabIndex = 17;
        this.btnGC.Text = "GC.Collect";
        this.btnGC.UseVisualStyleBackColor = true;
        this.btnGC.Click += new System.EventHandler(this.btnGC_Click);
        // 
        // btnChanges
        // 
        this.btnChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.btnChanges.Location = new System.Drawing.Point(925, 9);
        this.btnChanges.Name = "btnChanges";
        this.btnChanges.Size = new System.Drawing.Size(55, 23);
        this.btnChanges.TabIndex = 16;
        this.btnChanges.Text = "Apply";
        this.btnChanges.UseVisualStyleBackColor = true;
        this.btnChanges.Click += new System.EventHandler(this.btnChanges_Click);
        // 
        // chkAutoCreateObjects
        // 
        this.chkAutoCreateObjects.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.chkAutoCreateObjects.AutoSize = true;
        this.chkAutoCreateObjects.Checked = true;
        this.chkAutoCreateObjects.CheckState = System.Windows.Forms.CheckState.Checked;
        this.chkAutoCreateObjects.Location = new System.Drawing.Point(9, 224);
        this.chkAutoCreateObjects.Name = "chkAutoCreateObjects";
        this.chkAutoCreateObjects.Size = new System.Drawing.Size(115, 17);
        this.chkAutoCreateObjects.TabIndex = 21;
        this.chkAutoCreateObjects.Text = "AutoCreateObjects";
        this.chkAutoCreateObjects.UseVisualStyleBackColor = true;
        this.chkAutoCreateObjects.CheckedChanged += new System.EventHandler(this.chkAutoCreateObjects_CheckedChanged);
        // 
        // chkNotifyChangesChildLists
        // 
        this.chkNotifyChangesChildLists.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.chkNotifyChangesChildLists.AutoSize = true;
        this.chkNotifyChangesChildLists.Checked = true;
        this.chkNotifyChangesChildLists.CheckState = System.Windows.Forms.CheckState.Checked;
        this.chkNotifyChangesChildLists.Location = new System.Drawing.Point(155, 224);
        this.chkNotifyChangesChildLists.Name = "chkNotifyChangesChildLists";
        this.chkNotifyChangesChildLists.Size = new System.Drawing.Size(139, 17);
        this.chkNotifyChangesChildLists.TabIndex = 22;
        this.chkNotifyChangesChildLists.Text = "NotifyChangesChildLists";
        this.chkNotifyChangesChildLists.UseVisualStyleBackColor = true;
        this.chkNotifyChangesChildLists.CheckedChanged += new System.EventHandler(this.chkNotifyChangesChildLists_CheckedChanged);
        // 
        // splitContainer1
        // 
        this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.splitContainer1.Location = new System.Drawing.Point(12, 38);
        this.splitContainer1.Name = "splitContainer1";
        this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
        // 
        // splitContainer1.Panel1
        // 
        this.splitContainer1.Panel1.Controls.Add(this.chkNotifyPropertyChanges);
        this.splitContainer1.Panel1.Controls.Add(this.ChkConsiderChildsOnlyInCurrent);
        this.splitContainer1.Panel1.Controls.Add(this.label2);
        this.splitContainer1.Panel1.Controls.Add(this.ultraGrid1);
        this.splitContainer1.Panel1.Controls.Add(this.chkAutoCreateObjects);
        this.splitContainer1.Panel1.Controls.Add(this.chkNotifyChangesChildLists);
        // 
        // splitContainer1.Panel2
        // 
        this.splitContainer1.Panel2.Controls.Add(this.chkNotifyPropertyChanges2);
        this.splitContainer1.Panel2.Controls.Add(this.ChkConsiderChildsOnlyInCurrent2);
        this.splitContainer1.Panel2.Controls.Add(this.label3);
        this.splitContainer1.Panel2.Controls.Add(this.chkAutoCreateObjects2);
        this.splitContainer1.Panel2.Controls.Add(this.ultraGrid2);
        this.splitContainer1.Panel2.Controls.Add(this.chkNotifyChangesChildLists2);
        this.splitContainer1.Size = new System.Drawing.Size(968, 460);
        this.splitContainer1.SplitterDistance = 245;
        this.splitContainer1.TabIndex = 23;
        // 
        // chkNotifyPropertyChanges
        // 
        this.chkNotifyPropertyChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.chkNotifyPropertyChanges.AutoSize = true;
        this.chkNotifyPropertyChanges.Checked = true;
        this.chkNotifyPropertyChanges.CheckState = System.Windows.Forms.CheckState.Checked;
        this.chkNotifyPropertyChanges.Location = new System.Drawing.Point(819, 224);
        this.chkNotifyPropertyChanges.Name = "chkNotifyPropertyChanges";
        this.chkNotifyPropertyChanges.Size = new System.Drawing.Size(134, 17);
        this.chkNotifyPropertyChanges.TabIndex = 32;
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
        this.ChkConsiderChildsOnlyInCurrent.Location = new System.Drawing.Point(310, 224);
        this.ChkConsiderChildsOnlyInCurrent.Name = "ChkConsiderChildsOnlyInCurrent";
        this.ChkConsiderChildsOnlyInCurrent.Size = new System.Drawing.Size(159, 17);
        this.ChkConsiderChildsOnlyInCurrent.TabIndex = 24;
        this.ChkConsiderChildsOnlyInCurrent.Text = "ConsiderChildsOnlyInCurrent";
        this.ChkConsiderChildsOnlyInCurrent.UseVisualStyleBackColor = true;
        this.ChkConsiderChildsOnlyInCurrent.CheckedChanged += new System.EventHandler(this.ChkConsiderChildsOnlyInCurrent_CheckedChanged);
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(6, 7);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(124, 13);
        this.label2.TabIndex = 23;
        this.label2.Text = "DataSource: List<Order>";
        // 
        // ultraGrid1
        // 
        this.ultraGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.ultraGrid1.DataSource = this.ordersBindingSource;
        appearance1.BackColor = System.Drawing.SystemColors.Window;
        appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
        this.ultraGrid1.DisplayLayout.Appearance = appearance1;
        ultraGridColumn25.Header.VisiblePosition = 0;
        ultraGridColumn26.Header.VisiblePosition = 1;
        ultraGridColumn27.Header.VisiblePosition = 2;
        ultraGridColumn28.Header.VisiblePosition = 3;
        ultraGridColumn28.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDown;
        ultraGridColumn29.Header.VisiblePosition = 9;
        ultraGridColumn56.Header.VisiblePosition = 4;
        ultraGridColumn57.Header.VisiblePosition = 5;
        ultraGridColumn58.Header.VisiblePosition = 6;
        ultraGridColumn59.Header.VisiblePosition = 7;
        ultraGridColumn60.Header.VisiblePosition = 8;
        ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn25,
            ultraGridColumn26,
            ultraGridColumn27,
            ultraGridColumn28,
            ultraGridColumn29,
            ultraGridColumn56,
            ultraGridColumn57,
            ultraGridColumn58,
            ultraGridColumn59,
            ultraGridColumn60});
        ultraGridColumn30.Header.VisiblePosition = 0;
        ultraGridColumn30.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDown;
        ultraGridColumn31.Header.VisiblePosition = 1;
        ultraGridColumn32.Header.VisiblePosition = 6;
        ultraGridColumn33.Header.VisiblePosition = 5;
        ultraGridColumn34.Header.VisiblePosition = 2;
        ultraGridColumn35.Header.VisiblePosition = 3;
        ultraGridColumn36.Header.VisiblePosition = 4;
        ultraGridBand2.Columns.AddRange(new object[] {
            ultraGridColumn30,
            ultraGridColumn31,
            ultraGridColumn32,
            ultraGridColumn33,
            ultraGridColumn34,
            ultraGridColumn35,
            ultraGridColumn36});
        ultraGridColumn37.Header.VisiblePosition = 0;
        ultraGridColumn37.Hidden = true;
        ultraGridColumn38.Header.VisiblePosition = 1;
        ultraGridColumn38.Hidden = true;
        ultraGridColumn39.Header.VisiblePosition = 2;
        ultraGridColumn40.Header.VisiblePosition = 3;
        ultraGridColumn41.Header.VisiblePosition = 4;
        ultraGridBand3.Columns.AddRange(new object[] {
            ultraGridColumn37,
            ultraGridColumn38,
            ultraGridColumn39,
            ultraGridColumn40,
            ultraGridColumn41});
        ultraGridColumn42.Header.VisiblePosition = 0;
        ultraGridColumn43.Header.VisiblePosition = 1;
        ultraGridColumn43.Hidden = true;
        ultraGridBand4.Columns.AddRange(new object[] {
            ultraGridColumn42,
            ultraGridColumn43});
        ultraGridBand4.Hidden = true;
        this.ultraGrid1.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
        this.ultraGrid1.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
        this.ultraGrid1.DisplayLayout.BandsSerializer.Add(ultraGridBand3);
        this.ultraGrid1.DisplayLayout.BandsSerializer.Add(ultraGridBand4);
        this.ultraGrid1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
        this.ultraGrid1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
        appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
        appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
        appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
        appearance2.BorderColor = System.Drawing.SystemColors.Window;
        this.ultraGrid1.DisplayLayout.GroupByBox.Appearance = appearance2;
        appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
        this.ultraGrid1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
        this.ultraGrid1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
        appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
        appearance4.BackColor2 = System.Drawing.SystemColors.Control;
        appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
        appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
        this.ultraGrid1.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
        this.ultraGrid1.DisplayLayout.MaxColScrollRegions = 1;
        this.ultraGrid1.DisplayLayout.MaxRowScrollRegions = 1;
        appearance5.BackColor = System.Drawing.SystemColors.Window;
        appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
        this.ultraGrid1.DisplayLayout.Override.ActiveCellAppearance = appearance5;
        appearance6.BackColor = System.Drawing.SystemColors.Highlight;
        appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
        this.ultraGrid1.DisplayLayout.Override.ActiveRowAppearance = appearance6;
        this.ultraGrid1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
        this.ultraGrid1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
        appearance7.BackColor = System.Drawing.SystemColors.Window;
        this.ultraGrid1.DisplayLayout.Override.CardAreaAppearance = appearance7;
        appearance8.BorderColor = System.Drawing.Color.Silver;
        appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
        this.ultraGrid1.DisplayLayout.Override.CellAppearance = appearance8;
        this.ultraGrid1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
        this.ultraGrid1.DisplayLayout.Override.CellPadding = 0;
        appearance9.BackColor = System.Drawing.SystemColors.Control;
        appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
        appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
        appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
        appearance9.BorderColor = System.Drawing.SystemColors.Window;
        this.ultraGrid1.DisplayLayout.Override.GroupByRowAppearance = appearance9;
        appearance10.TextHAlignAsString = "Left";
        this.ultraGrid1.DisplayLayout.Override.HeaderAppearance = appearance10;
        this.ultraGrid1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
        this.ultraGrid1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
        appearance11.BackColor = System.Drawing.SystemColors.Window;
        appearance11.BorderColor = System.Drawing.Color.Silver;
        this.ultraGrid1.DisplayLayout.Override.RowAppearance = appearance11;
        this.ultraGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
        appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
        this.ultraGrid1.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
        this.ultraGrid1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
        this.ultraGrid1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
        this.ultraGrid1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
        this.ultraGrid1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.ultraGrid1.Location = new System.Drawing.Point(0, 23);
        this.ultraGrid1.Name = "ultraGrid1";
        this.ultraGrid1.Size = new System.Drawing.Size(968, 196);
        this.ultraGrid1.TabIndex = 14;
        this.ultraGrid1.Text = "ultraGrid1";
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
        this.ordersBindingSource.NotifyChangesInNestedPropertiesFromChildlists = true;
        this.ordersBindingSource.NotifyPropertyChanges = true;
        this.ordersBindingSource.RelatedObjectBindingSources = new UI.ObjectBindingSource.ObjectBindingSource[] {
        this.orderlinesBindingSource};
        this.ordersBindingSource.ListChangedOnChildList += new UI.ObjectBindingSource.ObjectBindingSource.ListChangedOnChildListEventHandler(this.ordersBindingSource_ListChangedOnChildList);
        this.ordersBindingSource.CreatingObject += new UI.ObjectBindingSource.ObjectBindingSource.CreatingObjectEventHandler(this.ordersBindingSource_CreatingObject);
        this.ordersBindingSource.NestedError += new UI.ObjectBindingSource.ObjectBindingSource.NestedErrorEventHandler(this.ordersBindingSource_NestedError);
        // 
        // orderlinesBindingSource
        // 
        this.orderlinesBindingSource.AllowNew = true;
        this.orderlinesBindingSource.AutoCreateObjects = false;
        this.orderlinesBindingSource.BindableNestedProperties = new string[] {
        "Product.ProductId",
        "Product.UnitPrice",
        "Product.Name"};
        this.orderlinesBindingSource.ChildListsToConsider = new string[] {
        "Details"};
        this.orderlinesBindingSource.ConsiderChildsOnlyInCurrent = true;
        this.orderlinesBindingSource.DataSource = typeof(OrderLine);
        this.orderlinesBindingSource.NonNestedPropertiesToSupervise = null;
        this.orderlinesBindingSource.NotifyChangesInNestedPropertiesFromChildlists = true;
        this.orderlinesBindingSource.NotifyPropertyChanges = true;
        this.orderlinesBindingSource.RelatedObjectBindingSources = new UI.ObjectBindingSource.ObjectBindingSource[] {
        this.detailOrderLinesBindingSource};
        // 
        // detailOrderLinesBindingSource
        // 
        this.detailOrderLinesBindingSource.AllowNew = false;
        this.detailOrderLinesBindingSource.AutoCreateObjects = false;
        this.detailOrderLinesBindingSource.BindableNestedProperties = new string[] {
        "RecomendedBy.CustomerId",
        "RecomendedBy.Name",
        "RecomendedBy.FavoriteProduct.Name"};
        this.detailOrderLinesBindingSource.ChildListsToConsider = null;
        this.detailOrderLinesBindingSource.ConsiderChildsOnlyInCurrent = true;
        this.detailOrderLinesBindingSource.DataSource = typeof(DetailOrderLine);
        this.detailOrderLinesBindingSource.NonNestedPropertiesToSupervise = null;
        this.detailOrderLinesBindingSource.NotifyChangesInNestedPropertiesFromChildlists = false;
        this.detailOrderLinesBindingSource.NotifyPropertyChanges = true;
        this.detailOrderLinesBindingSource.RelatedObjectBindingSources = new UI.ObjectBindingSource.ObjectBindingSource[0];
        // 
        // chkNotifyPropertyChanges2
        // 
        this.chkNotifyPropertyChanges2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.chkNotifyPropertyChanges2.AutoSize = true;
        this.chkNotifyPropertyChanges2.Checked = true;
        this.chkNotifyPropertyChanges2.CheckState = System.Windows.Forms.CheckState.Checked;
        this.chkNotifyPropertyChanges2.Location = new System.Drawing.Point(819, 184);
        this.chkNotifyPropertyChanges2.Name = "chkNotifyPropertyChanges2";
        this.chkNotifyPropertyChanges2.Size = new System.Drawing.Size(134, 17);
        this.chkNotifyPropertyChanges2.TabIndex = 34;
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
        this.ChkConsiderChildsOnlyInCurrent2.Location = new System.Drawing.Point(310, 184);
        this.ChkConsiderChildsOnlyInCurrent2.Name = "ChkConsiderChildsOnlyInCurrent2";
        this.ChkConsiderChildsOnlyInCurrent2.Size = new System.Drawing.Size(159, 17);
        this.ChkConsiderChildsOnlyInCurrent2.TabIndex = 26;
        this.ChkConsiderChildsOnlyInCurrent2.Text = "ConsiderChildsOnlyInCurrent";
        this.ChkConsiderChildsOnlyInCurrent2.UseVisualStyleBackColor = true;
        this.ChkConsiderChildsOnlyInCurrent2.CheckedChanged += new System.EventHandler(this.ChkConsiderChildsOnlyInCurrent2_CheckedChanged);
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(4, 8);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(159, 13);
        this.label3.TabIndex = 25;
        this.label3.Text = "DataSource: BindingList<Order>";
        // 
        // chkAutoCreateObjects2
        // 
        this.chkAutoCreateObjects2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.chkAutoCreateObjects2.AutoSize = true;
        this.chkAutoCreateObjects2.Checked = true;
        this.chkAutoCreateObjects2.CheckState = System.Windows.Forms.CheckState.Checked;
        this.chkAutoCreateObjects2.Location = new System.Drawing.Point(8, 184);
        this.chkAutoCreateObjects2.Name = "chkAutoCreateObjects2";
        this.chkAutoCreateObjects2.Size = new System.Drawing.Size(115, 17);
        this.chkAutoCreateObjects2.TabIndex = 23;
        this.chkAutoCreateObjects2.Text = "AutoCreateObjects";
        this.chkAutoCreateObjects2.UseVisualStyleBackColor = true;
        this.chkAutoCreateObjects2.CheckedChanged += new System.EventHandler(this.chkAutoCreateObjects2_CheckedChanged);
        // 
        // ultraGrid2
        // 
        this.ultraGrid2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.ultraGrid2.DataSource = this.ordersBindingSource_2;
        appearance13.BackColor = System.Drawing.SystemColors.Window;
        appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
        this.ultraGrid2.DisplayLayout.Appearance = appearance13;
        ultraGridColumn1.Header.VisiblePosition = 0;
        ultraGridColumn2.Header.VisiblePosition = 1;
        ultraGridColumn3.Header.VisiblePosition = 2;
        ultraGridColumn4.Header.VisiblePosition = 3;
        ultraGridColumn4.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDown;
        ultraGridColumn5.Header.VisiblePosition = 9;
        ultraGridColumn6.Header.Caption = "Customer Name";
        ultraGridColumn6.Header.VisiblePosition = 4;
        ultraGridColumn7.Header.Caption = "Customer BillingAddress StreetAddress";
        ultraGridColumn7.Header.VisiblePosition = 5;
        ultraGridColumn8.Header.Caption = "Customer BillingAddress City";
        ultraGridColumn8.Header.VisiblePosition = 6;
        ultraGridColumn9.Header.Caption = "DeliveryAddress StreetAddress";
        ultraGridColumn9.Header.VisiblePosition = 7;
        ultraGridColumn10.Header.Caption = "DeliveryAddress City";
        ultraGridColumn10.Header.VisiblePosition = 8;
        ultraGridBand5.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn6,
            ultraGridColumn7,
            ultraGridColumn8,
            ultraGridColumn9,
            ultraGridColumn10});
        ultraGridColumn11.Header.VisiblePosition = 0;
        ultraGridColumn11.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDown;
        ultraGridColumn12.Header.VisiblePosition = 1;
        ultraGridColumn13.Header.VisiblePosition = 6;
        ultraGridColumn14.Header.VisiblePosition = 5;
        ultraGridColumn15.Header.VisiblePosition = 2;
        ultraGridColumn16.Header.VisiblePosition = 3;
        ultraGridColumn17.Header.VisiblePosition = 4;
        ultraGridBand6.Columns.AddRange(new object[] {
            ultraGridColumn11,
            ultraGridColumn12,
            ultraGridColumn13,
            ultraGridColumn14,
            ultraGridColumn15,
            ultraGridColumn16,
            ultraGridColumn17});
        ultraGridColumn18.Header.VisiblePosition = 0;
        ultraGridColumn18.Hidden = true;
        ultraGridColumn19.Header.VisiblePosition = 1;
        ultraGridColumn20.Header.VisiblePosition = 2;
        ultraGridColumn21.Header.VisiblePosition = 3;
        ultraGridColumn22.Header.VisiblePosition = 4;
        ultraGridBand7.Columns.AddRange(new object[] {
            ultraGridColumn18,
            ultraGridColumn19,
            ultraGridColumn20,
            ultraGridColumn21,
            ultraGridColumn22});
        ultraGridColumn23.Header.VisiblePosition = 0;
        ultraGridColumn24.Header.VisiblePosition = 1;
        ultraGridBand8.Columns.AddRange(new object[] {
            ultraGridColumn23,
            ultraGridColumn24});
        ultraGridBand8.Hidden = true;
        this.ultraGrid2.DisplayLayout.BandsSerializer.Add(ultraGridBand5);
        this.ultraGrid2.DisplayLayout.BandsSerializer.Add(ultraGridBand6);
        this.ultraGrid2.DisplayLayout.BandsSerializer.Add(ultraGridBand7);
        this.ultraGrid2.DisplayLayout.BandsSerializer.Add(ultraGridBand8);
        this.ultraGrid2.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
        this.ultraGrid2.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
        appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
        appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
        appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
        appearance14.BorderColor = System.Drawing.SystemColors.Window;
        this.ultraGrid2.DisplayLayout.GroupByBox.Appearance = appearance14;
        appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
        this.ultraGrid2.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
        this.ultraGrid2.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
        appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
        appearance16.BackColor2 = System.Drawing.SystemColors.Control;
        appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
        appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
        this.ultraGrid2.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
        this.ultraGrid2.DisplayLayout.MaxColScrollRegions = 1;
        this.ultraGrid2.DisplayLayout.MaxRowScrollRegions = 1;
        appearance17.BackColor = System.Drawing.SystemColors.Window;
        appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
        this.ultraGrid2.DisplayLayout.Override.ActiveCellAppearance = appearance17;
        appearance18.BackColor = System.Drawing.SystemColors.Highlight;
        appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
        this.ultraGrid2.DisplayLayout.Override.ActiveRowAppearance = appearance18;
        this.ultraGrid2.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
        this.ultraGrid2.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
        appearance19.BackColor = System.Drawing.SystemColors.Window;
        this.ultraGrid2.DisplayLayout.Override.CardAreaAppearance = appearance19;
        appearance20.BorderColor = System.Drawing.Color.Silver;
        appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
        this.ultraGrid2.DisplayLayout.Override.CellAppearance = appearance20;
        this.ultraGrid2.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
        this.ultraGrid2.DisplayLayout.Override.CellPadding = 0;
        appearance21.BackColor = System.Drawing.SystemColors.Control;
        appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
        appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
        appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
        appearance21.BorderColor = System.Drawing.SystemColors.Window;
        this.ultraGrid2.DisplayLayout.Override.GroupByRowAppearance = appearance21;
        appearance22.TextHAlignAsString = "Left";
        this.ultraGrid2.DisplayLayout.Override.HeaderAppearance = appearance22;
        this.ultraGrid2.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
        this.ultraGrid2.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
        appearance23.BackColor = System.Drawing.SystemColors.Window;
        appearance23.BorderColor = System.Drawing.Color.Silver;
        this.ultraGrid2.DisplayLayout.Override.RowAppearance = appearance23;
        this.ultraGrid2.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
        appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
        this.ultraGrid2.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
        this.ultraGrid2.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
        this.ultraGrid2.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
        this.ultraGrid2.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
        this.ultraGrid2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.ultraGrid2.Location = new System.Drawing.Point(0, 24);
        this.ultraGrid2.Name = "ultraGrid2";
        this.ultraGrid2.Size = new System.Drawing.Size(968, 153);
        this.ultraGrid2.TabIndex = 13;
        this.ultraGrid2.Text = "ultraGrid2";
        // 
        // ordersBindingSource_2
        // 
        this.ordersBindingSource_2.AllowNew = true;
        this.ordersBindingSource_2.AutoCreateObjects = true;
        this.ordersBindingSource_2.BindableNestedProperties = new string[] {
        "Customer.Name",
        "Customer.BillingAddress.StreetAddress",
        "Customer.BillingAddress.City",
        "DeliveryAddress.StreetAddress",
        "DeliveryAddress.City"};
        this.ordersBindingSource_2.ChildListsToConsider = null;
        this.ordersBindingSource_2.ConsiderChildsOnlyInCurrent = true;
        this.ordersBindingSource_2.DataSource = typeof(Order);
        this.ordersBindingSource_2.NonNestedPropertiesToSupervise = null;
        this.ordersBindingSource_2.NotifyChangesInNestedPropertiesFromChildlists = true;
        this.ordersBindingSource_2.NotifyPropertyChanges = true;
        this.ordersBindingSource_2.RelatedObjectBindingSources = new UI.ObjectBindingSource.ObjectBindingSource[] {
        this.orderlinesBindingSource};
        this.ordersBindingSource_2.ListChangedOnChildList += new UI.ObjectBindingSource.ObjectBindingSource.ListChangedOnChildListEventHandler(this.ordersBindingSource_2_ListChangedOnChildList);
        this.ordersBindingSource_2.CreatingObject += new UI.ObjectBindingSource.ObjectBindingSource.CreatingObjectEventHandler(this.ordersBindingSource_2_CreatingObject);
        // 
        // chkNotifyChangesChildLists2
        // 
        this.chkNotifyChangesChildLists2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.chkNotifyChangesChildLists2.AutoSize = true;
        this.chkNotifyChangesChildLists2.Checked = true;
        this.chkNotifyChangesChildLists2.CheckState = System.Windows.Forms.CheckState.Checked;
        this.chkNotifyChangesChildLists2.Location = new System.Drawing.Point(150, 184);
        this.chkNotifyChangesChildLists2.Name = "chkNotifyChangesChildLists2";
        this.chkNotifyChangesChildLists2.Size = new System.Drawing.Size(139, 17);
        this.chkNotifyChangesChildLists2.TabIndex = 24;
        this.chkNotifyChangesChildLists2.Text = "NotifyChangesChildLists";
        this.chkNotifyChangesChildLists2.UseVisualStyleBackColor = true;
        this.chkNotifyChangesChildLists2.CheckedChanged += new System.EventHandler(this.chkNotifyChangesChildLists2_CheckedChanged);
        // 
        // chkConsiderOnlyDetails
        // 
        this.chkConsiderOnlyDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.chkConsiderOnlyDetails.AutoSize = true;
        this.chkConsiderOnlyDetails.Checked = true;
        this.chkConsiderOnlyDetails.CheckState = System.Windows.Forms.CheckState.Checked;
        this.chkConsiderOnlyDetails.Location = new System.Drawing.Point(19, 516);
        this.chkConsiderOnlyDetails.Name = "chkConsiderOnlyDetails";
        this.chkConsiderOnlyDetails.Size = new System.Drawing.Size(172, 17);
        this.chkConsiderOnlyDetails.TabIndex = 25;
        this.chkConsiderOnlyDetails.Text = "ConsiderOnly OrderLine.Details";
        this.chkConsiderOnlyDetails.UseVisualStyleBackColor = true;
        this.chkConsiderOnlyDetails.CheckedChanged += new System.EventHandler(this.chkConsiderOnlyDetails_CheckedChanged);
        // 
        // btnShowOBS
        // 
        this.btnShowOBS.Location = new System.Drawing.Point(267, 9);
        this.btnShowOBS.Name = "btnShowOBS";
        this.btnShowOBS.Size = new System.Drawing.Size(108, 23);
        this.btnShowOBS.TabIndex = 24;
        this.btnShowOBS.Text = "Show oBS alive";
        this.btnShowOBS.UseVisualStyleBackColor = true;
        this.btnShowOBS.Click += new System.EventHandler(this.btnShowOBS_Click);
        // 
        // chkConsiderNonNested_Product
        // 
        this.chkConsiderNonNested_Product.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.chkConsiderNonNested_Product.AutoSize = true;
        this.chkConsiderNonNested_Product.Location = new System.Drawing.Point(208, 516);
        this.chkConsiderNonNested_Product.Name = "chkConsiderNonNested_Product";
        this.chkConsiderNonNested_Product.Size = new System.Drawing.Size(167, 17);
        this.chkConsiderNonNested_Product.TabIndex = 27;
        this.chkConsiderNonNested_Product.Text = "Consider non nested \'Product\'";
        this.toolTip1.SetToolTip(this.chkConsiderNonNested_Product, "To see how it works make sure you select action \'26\' (no nested properties in Ord" +
                "er Lines grid)");
        this.chkConsiderNonNested_Product.UseVisualStyleBackColor = true;
        this.chkConsiderNonNested_Product.CheckedChanged += new System.EventHandler(this.chkConsiderNonNested_Product_CheckedChanged);
        // 
        // uddCustomer
        // 
        this.uddCustomer.DataSource = this.customersBindingSource;
        ultraGridColumn44.Header.VisiblePosition = 0;
        ultraGridColumn45.Header.VisiblePosition = 1;
        ultraGridColumn46.Header.VisiblePosition = 2;
        ultraGridColumn47.Header.VisiblePosition = 3;
        ultraGridColumn48.Header.VisiblePosition = 4;
        ultraGridColumn49.Header.VisiblePosition = 5;
        ultraGridBand9.Columns.AddRange(new object[] {
            ultraGridColumn44,
            ultraGridColumn45,
            ultraGridColumn46,
            ultraGridColumn47,
            ultraGridColumn48,
            ultraGridColumn49});
        this.uddCustomer.DisplayLayout.BandsSerializer.Add(ultraGridBand9);
        this.uddCustomer.DisplayMember = "Name";
        this.uddCustomer.Location = new System.Drawing.Point(418, 504);
        this.uddCustomer.Name = "uddCustomer";
        this.uddCustomer.PreferredDropDownSize = new System.Drawing.Size(0, 0);
        this.uddCustomer.Size = new System.Drawing.Size(128, 24);
        this.uddCustomer.TabIndex = 11;
        this.uddCustomer.ValueMember = "Self";
        this.uddCustomer.Visible = false;
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
        // uddProduct
        // 
        this.uddProduct.DataSource = this.productsBindingSource;
        ultraGridColumn50.Header.VisiblePosition = 0;
        ultraGridColumn51.Header.VisiblePosition = 1;
        ultraGridColumn52.Header.VisiblePosition = 2;
        ultraGridColumn53.Header.VisiblePosition = 3;
        ultraGridColumn54.Header.VisiblePosition = 4;
        ultraGridColumn55.Header.VisiblePosition = 5;
        ultraGridBand10.Columns.AddRange(new object[] {
            ultraGridColumn50,
            ultraGridColumn51,
            ultraGridColumn52,
            ultraGridColumn53,
            ultraGridColumn54,
            ultraGridColumn55});
        this.uddProduct.DisplayLayout.BandsSerializer.Add(ultraGridBand10);
        this.uddProduct.DisplayMember = "Name";
        this.uddProduct.Location = new System.Drawing.Point(567, 504);
        this.uddProduct.Name = "uddProduct";
        this.uddProduct.PreferredDropDownSize = new System.Drawing.Size(0, 0);
        this.uddProduct.Size = new System.Drawing.Size(128, 24);
        this.uddProduct.TabIndex = 12;
        this.uddProduct.ValueMember = "Self";
        this.uddProduct.Visible = false;
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
        // MainForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.WhiteSmoke;
        this.ClientSize = new System.Drawing.Size(992, 545);
        this.Controls.Add(this.chkConsiderNonNested_Product);
        this.Controls.Add(this.chkConsiderOnlyDetails);
        this.Controls.Add(this.btnShowOBS);
        this.Controls.Add(this.splitContainer1);
        this.Controls.Add(this.uddCustomer);
        this.Controls.Add(this.btnObjectsAlive);
        this.Controls.Add(this.cbAction);
        this.Controls.Add(this.btnGC);
        this.Controls.Add(this.btnChanges);
        this.Controls.Add(this.uddProduct);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.textBox1);
        this.Name = "MainForm";
        this.Text = "ObjectBindingSource Demo (Nested Property Binding)";
        this.splitContainer1.Panel1.ResumeLayout(false);
        this.splitContainer1.Panel1.PerformLayout();
        this.splitContainer1.Panel2.ResumeLayout(false);
        this.splitContainer1.Panel2.PerformLayout();
        this.splitContainer1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.ordersBindingSource)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.orderlinesBindingSource)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.detailOrderLinesBindingSource)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.ultraGrid2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.ordersBindingSource_2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.uddCustomer)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.customersBindingSource)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.uddProduct)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.productsBindingSource)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion


    private UI.ObjectBindingSource.ObjectBindingSource  ordersBindingSource;
    private UI.ObjectBindingSource.ObjectBindingSource orderlinesBindingSource;
    private UI.ObjectBindingSource.ObjectBindingSource customersBindingSource;
    private UI.ObjectBindingSource.ObjectBindingSource productsBindingSource;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Label label1;
    internal Infragistics.Win.UltraWinGrid.UltraDropDown uddCustomer;
    internal Infragistics.Win.UltraWinGrid.UltraDropDown uddProduct;
    private System.Windows.Forms.Button btnObjectsAlive;
    private System.Windows.Forms.ComboBox cbAction;
    private System.Windows.Forms.Button btnGC;
    private System.Windows.Forms.Button btnChanges;
    private System.Windows.Forms.CheckBox chkAutoCreateObjects;
    private System.Windows.Forms.CheckBox chkNotifyChangesChildLists;
    private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid2;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private UI.ObjectBindingSource.ObjectBindingSource ordersBindingSource_2;
    private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid1;
    private System.Windows.Forms.CheckBox chkAutoCreateObjects2;
    private System.Windows.Forms.CheckBox chkNotifyChangesChildLists2;
    private UI.ObjectBindingSource.ObjectBindingSource detailOrderLinesBindingSource;
    private System.Windows.Forms.Button btnShowOBS;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.CheckBox ChkConsiderChildsOnlyInCurrent;
    private System.Windows.Forms.CheckBox ChkConsiderChildsOnlyInCurrent2;
    private System.Windows.Forms.CheckBox chkConsiderOnlyDetails;
    private System.Windows.Forms.CheckBox chkConsiderNonNested_Product;
    private System.Windows.Forms.CheckBox chkNotifyPropertyChanges;
    private System.Windows.Forms.CheckBox chkNotifyPropertyChanges2;
    private System.Windows.Forms.ToolTip toolTip1;
    

}


