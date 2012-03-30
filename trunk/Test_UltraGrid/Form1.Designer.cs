namespace Test
{
    partial class Form1
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
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OrderNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DeliveryAddress");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OrderDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Customer", -1, "uddCustomer", 0, Infragistics.Win.UltraWinGrid.SortIndicator.Ascending, false);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OrderLines");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("OrderLines", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Product", -1, "uddProduct");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Quantity");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Details");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OtherDetails");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Product_ProductId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Product_UnitPrice");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Product_Name");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Details_Capacity");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OtherDetails_Capacity");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand3 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Details", 1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RecomendedBy");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NotShownProperty");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RecomendedBy_CustomerId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RecomendedBy_Name");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RecomendedBy_FavoriteProduct_Name");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand4 = new Infragistics.Win.UltraWinGrid.UltraGridBand("OtherDetails", 1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RecomendedBy");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NotShownProperty");
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ultraGrid1 = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ordersBindingSource = new UI.ObjectBindingSource.ObjectBindingSource(this.components);
            this.orderlinesBindingSource = new UI.ObjectBindingSource.ObjectBindingSource(this.components);
            this.detailOrderLinesBindingSource = new UI.ObjectBindingSource.ObjectBindingSource(this.components);
            this.productsBindingSource = new UI.ObjectBindingSource.ObjectBindingSource(this.components);
            this.customersBindingSource = new UI.ObjectBindingSource.ObjectBindingSource(this.components);
            this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ordersBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderlinesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detailOrderLinesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customersBindingSource)).BeginInit();
            this.SuspendLayout();
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
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Width = 77;
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn4.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDown;
            ultraGridColumn5.Header.VisiblePosition = 4;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5});
            ultraGridColumn6.Header.VisiblePosition = 0;
            ultraGridColumn6.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDown;
            ultraGridColumn6.Width = 119;
            ultraGridColumn7.Header.VisiblePosition = 1;
            ultraGridColumn8.Header.VisiblePosition = 8;
            ultraGridColumn9.Header.VisiblePosition = 7;
            ultraGridColumn10.Header.VisiblePosition = 2;
            ultraGridColumn11.Header.VisiblePosition = 3;
            ultraGridColumn12.Header.VisiblePosition = 4;
            ultraGridColumn13.Header.VisiblePosition = 5;
            ultraGridColumn14.Header.VisiblePosition = 6;
            ultraGridBand2.Columns.AddRange(new object[] {
            ultraGridColumn6,
            ultraGridColumn7,
            ultraGridColumn8,
            ultraGridColumn9,
            ultraGridColumn10,
            ultraGridColumn11,
            ultraGridColumn12,
            ultraGridColumn13,
            ultraGridColumn14});
            ultraGridColumn15.Header.VisiblePosition = 0;
            ultraGridColumn15.Hidden = true;
            ultraGridColumn16.Header.VisiblePosition = 1;
            ultraGridColumn16.Hidden = true;
            ultraGridColumn17.Header.VisiblePosition = 2;
            ultraGridColumn18.Header.VisiblePosition = 3;
            ultraGridColumn19.Header.VisiblePosition = 4;
            ultraGridBand3.Columns.AddRange(new object[] {
            ultraGridColumn15,
            ultraGridColumn16,
            ultraGridColumn17,
            ultraGridColumn18,
            ultraGridColumn19});
            ultraGridColumn20.Header.VisiblePosition = 0;
            ultraGridColumn21.Header.VisiblePosition = 1;
            ultraGridColumn21.Hidden = true;
            ultraGridBand4.Columns.AddRange(new object[] {
            ultraGridColumn20,
            ultraGridColumn21});
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
            this.ultraGrid1.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;
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
            this.ultraGrid1.Location = new System.Drawing.Point(21, 70);
            this.ultraGrid1.Name = "ultraGrid1";
            this.ultraGrid1.Size = new System.Drawing.Size(833, 311);
            this.ultraGrid1.TabIndex = 15;
            this.ultraGrid1.Text = "ultraGrid1";
            // 
            // ordersBindingSource
            // 
            this.ordersBindingSource.AllowNew = true;
            this.ordersBindingSource.AutoCreateObjects = true;
            this.ordersBindingSource.BindableNestedProperties = new string[0];
            this.ordersBindingSource.ChildListsToConsider = null;
            this.ordersBindingSource.ConsiderChildsOnlyInCurrent = true;
            this.ordersBindingSource.DataSource = typeof(Order);
            this.ordersBindingSource.NonNestedPropertiesToSupervise = null;
            this.ordersBindingSource.NotifyChangesInNestedPropertiesFromChildlists = false;
            this.ordersBindingSource.NotifyPropertyChanges = false;
            this.ordersBindingSource.RelatedObjectBindingSources = new UI.ObjectBindingSource.ObjectBindingSource[] {
        this.orderlinesBindingSource};
            // 
            // orderlinesBindingSource
            // 
            this.orderlinesBindingSource.AllowNew = true;
            this.orderlinesBindingSource.AutoCreateObjects = false;
            this.orderlinesBindingSource.BindableNestedProperties = new string[] {
        "Product.ProductId",
        "Product.UnitPrice",
        "Product.Name",
        "Details.Capacity",
        "OtherDetails.Capacity"};
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
            // ultraLabel1
            // 
            this.ultraLabel1.Location = new System.Drawing.Point(21, 12);
            this.ultraLabel1.Name = "ultraLabel1";
            this.ultraLabel1.Size = new System.Drawing.Size(833, 35);
            this.ultraLabel1.TabIndex = 17;
            this.ultraLabel1.Text = resources.GetString("ultraLabel1.Text");
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(877, 432);
            this.Controls.Add(this.ultraLabel1);
            this.Controls.Add(this.ultraGrid1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ordersBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderlinesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detailOrderLinesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customersBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UI.ObjectBindingSource.ObjectBindingSource orderlinesBindingSource;
        private UI.ObjectBindingSource.ObjectBindingSource detailOrderLinesBindingSource;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid1;
        private UI.ObjectBindingSource.ObjectBindingSource ordersBindingSource;
        private UI.ObjectBindingSource.ObjectBindingSource productsBindingSource;
        private UI.ObjectBindingSource.ObjectBindingSource customersBindingSource;
        private Infragistics.Win.Misc.UltraLabel ultraLabel1;
    }
}