#define USE_BLIST

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Infragistics.Win.UltraWinGrid;
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
    private readonly List<Order>               Orders =  new List<Order>();

    private readonly BindingList<Order> BListOrders = new BindingList<Order>();

    private List<WeakReference> _Objects = new List<WeakReference>();

    public MainForm()
    {
#if DEBUG
        DBG.SetLogFile("C:\\Test_ObjectBindingSource.Log", false);
#endif
        DBG.MaxDebugLevel = 1;
        //DBG.MaxNivelDepuracion = -1;   // No debug information
        
        InitializeComponent();

        textBox1.Text = DBG.MaxDebugLevel.ToString();
        
        CreateData();
        BindDataToUI();

#if !USE_BLIST
        chkAutoCreateObjects2.Enabled = false;
        chkNotifyChangesChildLists2.Enabled = false;
        ultraGrid2.Enabled = false;
        ChkConsiderChildsOnlyInCurrent2.Enabled = false;
#endif

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

#if USE_BLIST
        foreach (Order o in Orders)
            BListOrders.Add(o);
#endif


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

    }


    private void BindDataToUI()
    {
        customersBindingSource.DataSource =  Customers;
        productsBindingSource.DataSource =   Products;
        ordersBindingSource.DataSource =     Orders;

#if USE_BLIST
        ordersBindingSource_2.DataSource =  BListOrders;
#endif

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


#if USE_BLIST
                    BListOrders.Clear();
#endif



                    // As Orders don't implement IBindingList but only IList, clearing that list will raise
                    // an error on the DataGridView binded to the objects BindingSource on trying to refresth their cells, because
                    // BindingSource objects are not aware of the other objects deletion and so they don't inform to the consumers
                    // (DataGridView in this case)
                    // Also, if we make DataSource = null in the BindingSource controls will cause the same error because they will trigger 
                    // the method MyBase.ResetBindings(True) and that will raise ListChanged, forcing the DataGridView to refresh their
                    // content
                    //
                    // ==> We must execute .ResetBindings(false) just after the .Clear() previous

                    ordersBindingSource.ResetBindings(false);                  
                    //ordersBindingSource_2.ResetBindings(false);    // Esto no sería necesario

                    orderlinesBindingSource.DataSource = null;
                    ordersBindingSource.DataSource = null;
                    productsBindingSource.DataSource = null;
                    customersBindingSource.DataSource = null;

#if USE_BLIST
                    ordersBindingSource_2.DataSource = null;
#endif

                    detailOrderLinesBindingSource.DataSource = null;
                    break;

                case "12":   // BindingSourceS.Dispose
                    orderlinesBindingSource.Dispose();
                    ordersBindingSource.Dispose();
                    productsBindingSource.Dispose();
                    customersBindingSource.Dispose();
                    detailOrderLinesBindingSource.Dispose();
#if USE_BLIST
                    ordersBindingSource_2.Dispose();
#endif


                    break;

                case "13":  // Add New Order (4: <Jane Wilson>)
                    Order o = new Order(4, DateTime.Now, Customers[0]);
                    o.OrderLines.Add(new OrderLine(Products[1], 4));
                    o.OrderLines.Add(new OrderLine(Products[4], 5));
                    Orders.Add(o);

                    _Objects.Add(new WeakReference(o));

#if USE_BLIST
                    BListOrders.Add(o);
#endif
                    ultraGrid1.Rows.Refresh(RefreshRow.ReloadData, true);  // Pues el origen no es IBindingList. Para ultraGrid2 no es necesario
                    break;

                case "14":  // Remove New Order (4: <Jane Wilson>)
                    Orders.RemoveAt(3);
                    BListOrders.RemoveAt(3);
                    ultraGrid1.Rows.Refresh(RefreshRow.ReloadData, true);  // Pues el origen no es IBindingList. Para ultraGrid2 no es necesario
                    break;

                case "15":  // MOD ordersBindingSource.BindableNestedProperties: A
                    //ultraGrid1.DisplayLayout.NewColumnLoadStyle = NewColumnLoadStyle.Show;   ' This property could be necessary
                    ordersBindingSource.BindableNestedProperties = new string[] {
                            "Customer.Name",
                            "Customer.BillingAddress.StreetAddress",
                            "Customer.BillingAddress.City",
                            "DeliveryAddress.StreetAddress",
                            "DeliveryAddress.City",
                            "Customer.FavoriteProduct.Name"};
                    break;

                case "16":  // MOD ordersBindingSource.BindableNestedProperties: B
                    ordersBindingSource.BindableNestedProperties = new string[] {
                            "Customer.Name",
                            "Customer.BillingAddress.StreetAddress",
                            "Customer.BillingAddress.City",
                            "DeliveryAddress.StreetAddress",
                            "DeliveryAddress.City"};
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

                    break;


                case "18":   // Refresh ultraGrid1.Rows[0].ChildBands[0].Rows
                    ultraGrid1.Rows[0].ChildBands[0].Rows.Refresh(RefreshRow.ReloadData, true);
                    break;
                    

                case "19":   // Refresh ultraGrid1.Rows[1].ChildBands[0].Rows
                    ultraGrid1.Rows[1].ChildBands[0].Rows.Refresh(RefreshRow.ReloadData, true);
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

                case "25":   // MOD orderlinesBindingSource.BindableNestedProperties: B
                    orderlinesBindingSource.BindableNestedProperties = new string[] {
                            "Product.ProductId",
                            "Product.UnitPrice",
                            "Product.Name"};
                    break;

                case "26":   // MOD orderlinesBindingSource.BindableNestedProperties: C
                    orderlinesBindingSource.BindableNestedProperties = null;
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
        string cad= "";
        int i = 0;

        foreach (WeakReference wr in _Objects)
        {
            if (wr.IsAlive)
            {
                cad += "\n" + wr.Target.ToString();
                i += 1;
            }
        }

        MessageBox.Show("Objects alive: " + i.ToString() +"\n --------------\n" + cad);
    }


    private void btnObjectsAlive_Click(object sender, EventArgs e)
    {
        ShowObjectsAlive();
    }

    private void chkNotifyPropertyChanges_CheckedChanged(object sender, EventArgs e)
    {
        ordersBindingSource.NotifyPropertyChanges = chkNotifyPropertyChanges.Checked;
    }

    private void chkNotifyPropertyChanges2_CheckedChanged(object sender, EventArgs e)
    {
        ordersBindingSource_2.NotifyPropertyChanges = chkNotifyPropertyChanges2.Checked;
    }


    private void chkAutoCreateObjects_CheckedChanged(object sender, EventArgs e)
    {
        ordersBindingSource.AutoCreateObjects = chkAutoCreateObjects.Checked;
    }

    private void chkNotifyChangesChildLists_CheckedChanged(object sender, EventArgs e)
    {
        orderlinesBindingSource.NotifyChangesInNestedPropertiesFromChildlists = chkNotifyChangesChildLists.Checked;
        ordersBindingSource.NotifyChangesInNestedPropertiesFromChildlists = chkNotifyChangesChildLists.Checked;
    }

    private void chkAutoCreateObjects2_CheckedChanged(object sender, EventArgs e)
    {
        ordersBindingSource_2.AutoCreateObjects = chkAutoCreateObjects2.Checked;
    }

    private void chkNotifyChangesChildLists2_CheckedChanged(object sender, EventArgs e)
    {
        orderlinesBindingSource.NotifyChangesInNestedPropertiesFromChildlists = chkNotifyChangesChildLists2.Checked;
        ordersBindingSource_2.NotifyChangesInNestedPropertiesFromChildlists = chkNotifyChangesChildLists2.Checked;
    }

    private void ChkConsiderChildsOnlyInCurrent_CheckedChanged(object sender, EventArgs e)
    {
        orderlinesBindingSource.ConsiderChildsOnlyInCurrent = ChkConsiderChildsOnlyInCurrent.Checked;
        ordersBindingSource.ConsiderChildsOnlyInCurrent = ChkConsiderChildsOnlyInCurrent.Checked;
    }

    private void ChkConsiderChildsOnlyInCurrent2_CheckedChanged(object sender, EventArgs e)
    {
        orderlinesBindingSource.ConsiderChildsOnlyInCurrent = ChkConsiderChildsOnlyInCurrent2.Checked;
        ordersBindingSource_2.ConsiderChildsOnlyInCurrent = ChkConsiderChildsOnlyInCurrent2.Checked;
    }

    private void chkConsiderOnlyDetails_CheckedChanged(object sender, EventArgs e)
    {
        if (chkConsiderOnlyDetails.Checked)
            orderlinesBindingSource.ChildListsToConsider = new string[] { "Details" };
        else
            orderlinesBindingSource.ChildListsToConsider = null;
    }

    private void chkConsiderNonNested_Product_CheckedChanged(object sender, EventArgs e)
    {
        if (chkConsiderNonNested_Product.Checked)
            orderlinesBindingSource.NonNestedPropertiesToSupervise = new string[] { "Product" };
        else
            orderlinesBindingSource.NonNestedPropertiesToSupervise = null;
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
        if (ObjectType == typeof(Customer))
        {
            Obj = new Customer();
            Customers.Add((Customer)Obj);
            customersBindingSource.ResetBindings(false);
        }
    }

    private void ordersBindingSource_ListChangedOnChildList(int row, object sender, ListChangedEventArgs e)
    {
        ultraGrid1.Rows[row].ChildBands[0].Rows.Refresh(Infragistics.Win.UltraWinGrid.RefreshRow.RefreshDisplay, true);
    }

    private void ordersBindingSource_2_ListChangedOnChildList(int row, object sender, ListChangedEventArgs e)
    {
        ultraGrid2.Rows[row].ChildBands[0].Rows.Refresh(Infragistics.Win.UltraWinGrid.RefreshRow.RefreshDisplay, true);
    }

    private void btnShowOBS_Click(object sender, EventArgs e)
    {
#if DEBUG
        
        foreach (WeakReference wr in UI.ObjectBindingSource.ObjectBindingSource._Instances)
            if (wr.IsAlive)
            {
                ObjectBindingSource oBS = (ObjectBindingSource)wr.Target;
                oBS.ShowConfiguration(0);
                DBG.Log(0, "",0,false);
            }
        MessageBox.Show("ObjectBindingSources alive: " + ObjectBindingSource.NumberInstancesAlive().ToString());

#endif
     }

    private void ordersBindingSource_NestedError(object Sender, NestedErrorEventArgs e)
    {
        MessageBox.Show(string.Format("NestedError handled: Type:{0} PropPath:{1} Object:{2} Excep:{3}", e.ErrorType, e.PropertyPath, e.Object, e.Exception.Message));
    }

}



    

