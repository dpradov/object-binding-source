using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UI.ObjectBindingSource;


//# Modifications from Daniel Prado Velasco <dpradov@gmail.com> to code released 
//# by seesharper (http://www.codeproject.com/Members/seesharper) and licensed
//# under The Code Project Open License (CPOL) (http://www.codeproject.com/info/cpol10.aspx)
//# The original work of seesharper is available in: 
//#    http://www.codeproject.com/KB/cs/ObjectBindingSource.aspx
//# You can also download it from: 
//#    http://code.google.com/p/object-binding-source/downloads/list
//#
//# -----------------
//# URLs:
//#  http://code.google.com/p/object-binding-source/


public partial class MainForm : Form
{        
 
    private readonly BindingList<Customer>  Customers =  new BindingList<Customer>();
    private readonly BindingList<Product>    Products =  new BindingList<Product>();
    private readonly List<Order>               Orders =  new List<Order>();          // Test with IList
    //private readonly BindingList<Order> Orders = new BindingList<Order>();            // Test with IBindingList

    public readonly List<OtherItem> OtherItems = new List<OtherItem>();
    public  readonly BindingList<SimpleClass> SimpleClasses = new BindingList<SimpleClass>();

    private List<WeakReference> _Objects = new List<WeakReference>();



    public MainForm()
    {
        string dbgFile = "<Console.StandardOutput>";
#if DEBUG
        dbgFile = "C:\\Test_ObjectBindingSource.Log";
        DBG.SetLogFile(dbgFile, false);
#endif
        DBG.MaxDebugLevel  = 1;
        //DBG.MaxDebugLevel = -1;   // No debug information
        
        InitializeComponent();
        lblDebugFile.Text = dbgFile;

        textBox1.Text = DBG.MaxDebugLevel.ToString();
        
        dataGridView1.AutoGenerateColumns = true;
        dataGridView2.AutoGenerateColumns = false;

        CreateData();
        BindDataToUI();
    }

    
    private void CreateData()
    {
        // Products
        //-----------------
        Products.Add(new Product(1, "Keyboard", 49.99));
        Products.Add(new Product(2, "Mouse", 10.99));
        Products.Add(new Product(3, "PC", 599.99));
        Products.Add(new Product(4, "Monitor", 299.99));
        Products.Add(new Product(5, "LapTop", 799.99));
        Products.Add(new Product(6, "Harddisc", 89.99));

        // Customers
        //-----------------
        Customers.Add(new Customer(1, "Jane Wilson", new Address("98104", "6657 Sand Pointe Lane", "Seattle", "USA")));
        Customers.Add(new Customer(2, "Bill Smith", new Address("94109", "5725 Glaze Drive", "San Francisco", "USA")));
        Customers.Add(new Customer(3, "Samantha Brown", null));

        Customers[0].FavoriteProduct = Products[0];
        Customers[1].FavoriteProduct = Products[1];


        // Orders
        //---------------------
        Orders.Add(new Order(1, DateTime.Now, Customers[0]));
        Orders.Add(new Order(2, DateTime.Now, Customers[1]));
        Orders.Add(new Order(3, DateTime.Now, Customers[2]));


        //for (int i = 4; i < 10000; i++)
        //    Orders.Add(new Order(i, DateTime.Now, Customers[0]));



        Orders[0].OrderLines.Add(new OrderLine(Products[0], 1));
        Orders[0].OrderLines.Add(new OrderLine(Products[1], 3));
        Orders[1].OrderLines.Add(new OrderLine(Products[2], 2));
        Orders[1].OrderLines.Add(new OrderLine(Products[0], 4));

        Orders[0].OrderLines[0].Details.Add(new DetailOrderLine(Customers[0]));
        Orders[0].OrderLines[0].Details.Add(new DetailOrderLine(Customers[1]));
        Orders[0].OrderLines[1].Details.Add(new DetailOrderLine(Customers[1]));
        Orders[0].OrderLines[1].Details.Add(new DetailOrderLine(Customers[0]));

        //for (int i = 4; i < 9500; i++)
        //{
        //    Orders[i].OrderLines.Add(new OrderLine(Products[0], 5));
        //    Orders[i].OrderLines.Add(new OrderLine(Products[1], 6));
        //}

        // OtherItems
        //---------------------
        OtherItems.Add(new OtherItem(1, "Item1", Products[0]));
        OtherItems.Add(new OtherItem(2, "Item2", Products[1]));
        OtherItems.Add(new OtherItem(3, "Item3", Products[2]));


        // SimpleClasses
        //----------------------------------
        SimpleClasses.Add(new SimpleClass(1, "un"));
        SimpleClasses.Add(new SimpleClass(2, "deux"));



        // Take a weak reference to the objects so that later we can inspect if they are still live after a deletion
        // and invoking GC.Collect. 
        // That would signal that the component is still referencing them (probably it didn't realize the necessary RemoveHandler
        // from NestedPropertyChanged)

        foreach (Customer c in Customers)
        {
            _Objects.Add(new WeakReference(c));
            if (c.BillingAddress != null)
                _Objects.Add(new WeakReference(c.BillingAddress));
        }

        foreach (object o in Products)
            _Objects.Add(new WeakReference(o));

        foreach (Order o in Orders)
        {
            _Objects.Add(new WeakReference(o));
            foreach (OrderLine ol in o.OrderLines)
            {
                _Objects.Add(new WeakReference(ol));
                foreach (object d in ol.Details )
                    _Objects.Add(new WeakReference(d));
            }
        }

        foreach (object o in OtherItems)
            _Objects.Add(new WeakReference(o));

        foreach (object o in SimpleClasses)
            _Objects.Add(new WeakReference(o));

    }


    private void BindDataToUI()
    {
        // This way we can show a combo with the name of the enum values. If we have a TypeConverter
        // associated to the enum type then we can show a complete description included in an attribute.
        // The example included [TypeConverter(typeof(EnumToStringUsingDescription))] allows to see the
        // description, but doesn't make possible to modify the value because it handles (by now) only 
        // a conversion from enum to string, not from string to enum
        //Product_Type.DataSource = Enum.GetValues(typeof(ProductType));
        //Product_Type.DataPropertyName = "Product_Type";
        //Product_Type.Name = "Product Type";

        // With this helper class we can show and modify the enum type associated to the property using
        // a long description included in an attribute
        //Product_Type.DataSource = new EnumeratedValueCollection<ProductType>();
        //Product_Type.ValueMember = "Value";
        //Product_Type.DisplayMember = "Description";
        //Product_Type.DataPropertyName = "Product_Type";


        customersBindingSource.DataSource =  Customers;
        productsBindingSource.DataSource =   Products;
        ordersBindingSource.DataSource =     Orders;
        otherItemsBindingSource.DataSource = OtherItems;
        simpleClassesBindingSource.DataSource = SimpleClasses;
    }

       

    private void textBox1_Validated(object sender, EventArgs e)
    {
        try
        {
            DBG.MaxDebugLevel = Convert.ToInt32(textBox1.Text);
        }
        catch
        { }
    }


    private void btnGC_Click(object sender, EventArgs e)
    {
        GC.Collect();
    }

    private void btnChanges_Click(object sender, EventArgs e)
    {
        string cad;

        try
        {
            String Action = "";
            if (cbAction.SelectedIndex >= 0)
                Action = cbAction.Text.Substring(0, 2);

            switch (Action)
            {
                case " 0":   // MOD "Keyboard"
                    cad = ChangeCase(Products[0].Name);
                    if (cad.EndsWith("*"))
                        cad = cad.Substring(0, cad.Length - 1);
                    else
                        cad = cad + "*";

                    // NOTE!!: A modification that only changes the case of the letters will be ignored by comboBox controls. Is its normal behaviour.
                    // Thats why "*" is added or removed.

                    Products[0].Name = cad;
                    break;

                case " 1":   // MOD "Jane Wilson" & "Bill Smith" 
                    cad = ChangeCase(Customers[0].Name);
                    Customers[0].Name = cad;
                    cad = ChangeCase(Customers[1].Name);
                    Customers[1].Name = cad;
                    break;

                case " 2":   // MOD "Keyboard" & "Mose"
                    cad = ChangeCase(Products[0].Name);
                    Products[0].Name = cad;
                    cad = ChangeCase(Products[1].Name);
                    Products[1].Name = cad;
                    break;

                case " 3":   // INC OrderDate: Order-1 & Order-2
                    Orders[0].OrderDate = Orders[0].OrderDate.AddDays(1);
                    Orders[1].OrderDate = Orders[1].OrderDate.AddDays(1);
                    break;

                case " 4":   // Order-1.OrderLines[1].Product <- Order-1.OrderLines[0].Product
                    Orders[0].OrderLines[1].Product = Orders[0].OrderLines[0].Product;
                    break;

                case " 5":   // INC OrderLines[0].Quantity:  Order-1 & Order-2
                    Orders[0].OrderLines[0].Quantity += 1;
                    Orders[1].OrderLines[0].Quantity += 1;
                    break;

                case " 6":   // INC Customer.Age  <Jane Wilson> & <Bill Smith>
                    Customers[0].Age += 1;
                    Customers[1].Age += 1;
                    break;

                case " 7":   // INC Product.UnitPrice: <Keyboard> & <Mouse>
                    Products[0].UnitPrice += 10;
                    Products[1].UnitPrice += 10;
                    break;

                case " 8":   // MOD <Keyboard>.ChangeMultiple (Property=Nothing)
                    Products[0].ChangeMultiple();
                    break;

                case " 9":   // Remove <Samantha Brown>
                    Customers.RemoveAt(2);
                    break;

                case "10":   // Remove <LapTop>
                    Products.RemoveAt(4);
                    break;

                case "11": // Delete All
                    Orders.Clear();
                    Customers.Clear();
                    Products.Clear();
                    OtherItems.Clear();
                    SimpleClasses.Clear(); 

                    // As Orders and OtherItems don't implement IBindingList but only IList, deleting these lists will raise
                    // an error on the DataGridView binded to the objects BindingSource on trying to refresth their cells, because
                    // BindingSource objects are not aware of the other objects deletion and so they don't inform to the consumers
                    // (DataGridView in this case)
                    // Also, if we make DataSource = null in the BindingSource controls will cause the same error because they will trigger 
                    // the method MyBase.ResetBindings(True) and that will raise ListChanged, forcing the DataGridView to refresh their
                    // content
                    // ==> We must execute .ResetBindings(false) just after the .Clear() previous

                    ordersBindingSource.ResetBindings(false);
                    otherItemsBindingSource.ResetBindings(false);

                    dataGridView1.AutoGenerateColumns = false;

                    orderlinesBindingSource.DataSource = null;
                    ordersBindingSource.DataSource = null;
                    productsBindingSource.DataSource = null;
                    customersBindingSource.DataSource = null;
                    otherItemsBindingSource.DataSource = null;
                    simpleClassesBindingSource.DataSource = null;
                    break;

                case "12":   // BindingSourceS.Dispose
                    orderlinesBindingSource.Dispose();
                    ordersBindingSource.Dispose();
                    productsBindingSource.Dispose();
                    customersBindingSource.Dispose();
                    otherItemsBindingSource.Dispose();
                    simpleClassesBindingSource.Dispose();
                    break;

                case "13":  // Add New Order (4: <Jane Wilson>)
                    Order o = new Order(4, DateTime.Now, Customers[0]);
                    o.OrderLines.Add(new OrderLine(Products[1], 4));
                    o.OrderLines.Add(new OrderLine(Products[4], 5));
                    Orders.Add(o);

                    _Objects.Add(new WeakReference(o));
                    ordersBindingSource.ResetBindings(false);     // Pues el origen no es IBindingList
                    break;

                case "14":  // Remove New Order (4: <Jane Wilson>)
                    Orders.RemoveAt(3);
                    ordersBindingSource.ResetBindings(false);     // Pues el origen no es IBindingList
                    break;

                case "24":  // INSERT New Order (4: <Jane Wilson>) at position 1
                    o = new Order(4, DateTime.Now, Customers[0]);
                    o.OrderLines.Add(new OrderLine(Products[1], 4));
                    o.OrderLines.Add(new OrderLine(Products[4], 5));
                    Orders.Insert(1, o);

                    _Objects.Add(new WeakReference(o));
                    ordersBindingSource.ResetBindings(false);     // Pues el origen no es IBindingList
                    break;

                case "15":  // MOD ordersBindingSource.BindableNestedProperties: A
                    ordersBindingSource.BindableNestedProperties = new string[] {
                            "Customer.Name",
                            "Customer.BillingAddress.StreetAddress",
                            "Customer.BillingAddress.City",
                            "DeliveryAddress.StreetAddress",
                            "DeliveryAddress.City",
                            "Customer.FavoriteProduct.Name"};

                    //DataGridViewColumn c = new DataGridViewTextBoxColumn();
                    //c.DataPropertyName = "Customer_FavoriteProduct_Name";
                    //c.HeaderText = "Customer.FavoriteProduct.Name";
                    //c.Name = "customerFavoriteProductNameDataGridViewTextBoxColumn";
                    //dataGridView1.Columns.Add(c);
                    break;

                case "16":  // MOD ordersBindingSource.BindableNestedProperties: B
                    ordersBindingSource.BindableNestedProperties = new string[] {
                            "Customer.Name",
                            "Customer.BillingAddress.StreetAddress",
                            "Customer.BillingAddress.City",
                            "DeliveryAddress.StreetAddress",
                            "DeliveryAddress.City"};

                    //c = dataGridView1.Columns[dataGridView1.Columns.Count-1];
                    //if (c.DataPropertyName == "Customer_FavoriteProduct_Name")
                    //    dataGridView1.Columns.RemoveAt(dataGridView1.Columns.Count - 1);
                    break;

                case "23":  // MOD ordersBindingSource.BindableNestedProperties: C
                    ordersBindingSource.BindableNestedProperties = new string[] {
                            "Customer.Name",
                            "Customer.BillingAddress.StreetAddress",
                            "Customer.BillingAddress.City",
                            "DeliveryAddress.StreetAddress",
                            "DeliveryAddress.City",
                            "Customer.FavoriteProduct.NonExistentProperty"};
                    break;

                case "17":   // MOD orderlinesBindingSource.BindableNestedProperties: A
                    orderlinesBindingSource.BindableNestedProperties = new string[] {
                            "Product.ProductId",
                            "Product.UnitPrice",
                            "Product.Name",
                            "Product.Type"};

                    // I have set: dataGridView2.AutoGenerateColumns = false;

                    DataGridViewComboBoxColumn productType = new DataGridViewComboBoxColumn();
                    productType.DataSource = new EnumeratedValueCollection<ProductType>();
                    productType.ValueMember = "Value";
                    productType.DisplayMember = "Description";
                    productType.DataPropertyName = "Product_Type";
                    productType.HeaderText = "Product.Type";
                    dataGridView2.Columns.Add(productType);
                    break;

                case "20":  // MOD Orders[0].OrderLines[0].Details[0].NotShownProperty
                    Orders[0].OrderLines[0].Details[0].NotShownProperty += 10;
                    break;

                case "21":   // Orders[0].OrderLines = Orders[0].OrderLines;
                    Orders[0].OrderLines = Orders[0].OrderLines;
                    break;

                case "22":   // Orders[0].OrderLines = Orders[1].OrderLines;
                    Orders[0].OrderLines = Orders[1].OrderLines;
                    break;

                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }


    }

    private string ChangeCase(string cad)
    {
        if (cad == cad.ToUpper())
            cad = cad.ToLower();
        else
            cad = cad.ToUpper();

        return cad;
    }

    private void ShowObjectsAlive()
    {
        string cad = "";
        int i = 0;

        foreach (WeakReference wr in _Objects)
        {
            if (wr.IsAlive)
            {
                cad += "\n" + wr.Target.ToString();
                i += 1;
            }
        }

        MessageBox.Show("Objects alive: " + i.ToString() + "\n --------------\n" + cad);
    }


    private void btnObjectsAlive_Click(object sender, EventArgs e)
    {
        ShowObjectsAlive();
    }

    private void chkAutoCreateObjects_CheckedChanged(object sender, EventArgs e)
    {
        ordersBindingSource.AutoCreateObjects = chkAutoCreateObjects.Checked;
    }

    private void chkNotifyChangesChildLists_CheckedChanged(object sender, EventArgs e)
    {
        ordersBindingSource.NotifyChangesInNestedPropertiesFromChildlists = chkNotifyChangesChildLists.Checked;
    }

    private void chkAutoCreateObjects2_CheckedChanged(object sender, EventArgs e)
    {
        orderlinesBindingSource.AutoCreateObjects = chkAutoCreateObjects2.Checked;
    }

    private void chkNotifyChangesChildLists2_CheckedChanged(object sender, EventArgs e)
    {
        orderlinesBindingSource.NotifyChangesInNestedPropertiesFromChildlists = chkNotifyChangesChildLists2.Checked;
    }

    private void ChkConsiderChildsOnlyInCurrent_CheckedChanged(object sender, EventArgs e)
    {
        ordersBindingSource.ConsiderChildsOnlyInCurrent = ChkConsiderChildsOnlyInCurrent.Checked;
    }

    private void ChkConsiderChildsOnlyInCurrent2_CheckedChanged(object sender, EventArgs e)
    {
        orderlinesBindingSource.ConsiderChildsOnlyInCurrent = ChkConsiderChildsOnlyInCurrent2.Checked;
    }

    private void chkConsiderOnlyDetails_CheckedChanged(object sender, EventArgs e)
    {
        if (chkConsiderOnlyDetails.Checked)
            orderlinesBindingSource.ChildListsToConsider = new string[] { "Details" };
        else
            orderlinesBindingSource.ChildListsToConsider = null;
    }


    private void ordersBindingSource_CreatingObject(object Sender, Type ObjectType, ref object Obj)
    {
        if (ObjectType == typeof(Customer))
        {
            Obj = new Customer();
            Customers.Add((Customer)Obj);
            customersBindingSource.ResetBindings(false);
        }
    }

    private void ordersBindingSource_2_CreatingObject(object Sender, Type ObjectType, ref object Obj)
    {
        if (ObjectType == typeof(Product))
        {
            Obj = new Product();
            Products.Add((Product)Obj);
            productsBindingSource.ResetBindings(false);
        }
    }

    private void ordersBindingSource_ListChangedOnChildList(int row, object sender, ListChangedEventArgs e)
    {
    }

    private void ordersBindingSource_2_ListChangedOnChildList(int row, object sender, ListChangedEventArgs e)
    {
    }

    private void btnShowOBS_Click(object sender, EventArgs e)
    {
#if DEBUG

        foreach (WeakReference wr in UI.ObjectBindingSource.ObjectBindingSource._Instances)
            if (wr.IsAlive)
            {
                ObjectBindingSource oBS = (ObjectBindingSource)wr.Target;
                oBS.ShowConfiguration(0);
                DBG.Log(0, "", 0, false);
            }
        MessageBox.Show("ObjectBindingSources alive: " + ObjectBindingSource.NumberInstancesAlive().ToString());

#endif

    }

    private void btnHookedObjects_Click(object sender, EventArgs e)
    {
#if DEBUG
        this.productsBindingSource.Show_HookedObjects();
        this.customersBindingSource.Show_HookedObjects();
        this.ordersBindingSource.Show_HookedObjects();
        this.orderlinesBindingSource.Show_HookedObjects();
#endif
    }

    private void ordersBindingSource_NestedError(object Sender, NestedErrorEventArgs e)
    {
        MessageBox.Show(string.Format("NestedError handled: Type:{0} PropPath:{1} Object:{2} Excep:{3}", e.ErrorType, e.PropertyPath, e.Object, e.Exception.Message));
    }

}
