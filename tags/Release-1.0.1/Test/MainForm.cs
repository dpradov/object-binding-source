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
 
    public readonly List<Customer> _customers = new List<Customer>();
    private readonly List<Product> _products = new List<Product>();
    private readonly List<Order> _orders = new List<Order>();

    private WeakReference _ordRef = null;
    private WeakReference _ordRef2 = null;

    public MainForm()
    {
        
        InitializeComponent();

        //DBG.MaxNivelDepuracion = 2;
        DBG.MaxNivelDepuracion = -1;   // No debug information

        textBox1.Text = DBG.MaxNivelDepuracion.ToString();
        
        dataGridView1.AutoGenerateColumns = false;
        dataGridView2.AutoGenerateColumns = false;

        CreateData();
    }

    
    private void CreateData()
    {
        _customers.Add(new Customer(1, "Jane Wilson", new Address("98104", "6657 Sand Pointe Lane", "Seattle", "USA")));
        _customers.Add(new Customer(2, "Bill Smith", new Address("94109", "5725 Glaze Drive", "San Francisco", "USA")));
        _customers.Add(new Customer(3, "Samantha Brown", null));

        _products.Add(new Product(1, "Keyboard", 49.99));
        _products.Add(new Product(2, "Mouse", 10.99));
        _products.Add(new Product(3, "PC", 599.99));
        _products.Add(new Product(4, "Monitor", 299.99));
        _products.Add(new Product(5, "LapTop", 799.99));
        _products.Add(new Product(6, "Harddisc", 89.99));

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
        Product_Type.DataSource = new EnumeratedValueCollection<ProductType>();
        Product_Type.ValueMember = "Value";
        Product_Type.DisplayMember = "Description";
        Product_Type.DataPropertyName = "Product_Type";


        customersBindingSource.DataSource = _customers;
        productsBindingSource.DataSource = _products;            

        _orders.Add(new Order(1, DateTime.Now, _customers[0]));
        _orders.Add(new Order(2, DateTime.Now, _customers[1]));
        _orders.Add(new Order(3, DateTime.Now, _customers[2]));


        _orders[0].OrderLines.Add(new OrderLine(_products[0], 1));
        _orders[0].OrderLines.Add(new OrderLine(_products[1], 3));
        _orders[1].OrderLines.Add(new OrderLine(_products[2], 2));
        _orders[1].OrderLines.Add(new OrderLine(_products[3], 4));

        ordersBindingSource.DataSource = _orders;

        _ordRef = new WeakReference(_customers[0]);
        _ordRef2 = new WeakReference(_customers[0].BillingAddress);
        MessageBox.Show(String.Format("Created WeakReference to the following objects: customers[0]= {0}   customers[0].BillingAddress= {1}", _ordRef.Target, _ordRef2.Target));
    }

    private void DeleteAll_Click(object sender, EventArgs e)
    {
        Console.WriteLine("");
        
        _orders.Clear();
        _customers.Clear();
        _products.Clear();

        // As Orders, _customers and _products don't implement IBindingList, but only IList, deleting these lists will raise
        // an error on the DataGridView binded to the objects BindingSource on trying to refresth their cells, because
        // BindingSource objects are not aware of the other objects deletion and so they don't inform to the consumers
        // (DataGridView in this case)
        // Also, if we make DataSource = null in the BindingSource controls will cause the same error because they will trigger 
        // the method MyBase.ResetBindings(True) and that will raise ListChanged, forcing the DataGridView to refresh their
        // content
        // ==> We must execute .ResetBindings(false) just after the .Clear() previous

        ordersBindingSource.ResetBindings(false);
        orderlinesBindingSource.ResetBindings(false);
        productsBindingSource.ResetBindings(false);
        customersBindingSource.ResetBindings(false);         
        
        orderlinesBindingSource.DataSource = null;
        ordersBindingSource.DataSource = null;
        productsBindingSource.DataSource = null;
        customersBindingSource.DataSource = null;

        MessageBox.Show("Lists (orders, customers, products) have been cleared. Components ObjectBindingSource as assigned to null");

        MessageBox.Show(String.Format("Object customers[0] is alive: {0}",_ordRef.IsAlive));
        MessageBox.Show(String.Format("Object customers[0].BillingAddress is alive: {0}", _ordRef2.IsAlive));


        GC.Collect();
        MessageBox.Show(String.Format("After GC.Collect the object _customers[0] is alive: {0}", _ordRef.IsAlive));
        MessageBox.Show(String.Format("After GC.Collect the object _customers[0].BillingAddress is alive: {0}", _ordRef2.IsAlive));


        orderlinesBindingSource.Dispose();
        ordersBindingSource.Dispose();
        productsBindingSource.Dispose();
        customersBindingSource.Dispose();

        GC.Collect();
        MessageBox.Show(String.Format("After GC.Collect and Dispose of ObjectBindingSource the object _customers[0] is alive: {0}", _ordRef.IsAlive));
        MessageBox.Show(String.Format("After GC.Collect and Dispose of ObjectBindingSource the object _customers[0].BillingAddress is alive: {0}", _ordRef2.IsAlive));
    }

    private void btnDeleteCustomer_Click(object sender, EventArgs e)
    {
        _customers.RemoveAt(0);
        customersBindingSource.ResetBindings(false);

        MessageBox.Show("Removed _customers[0]");

        GC.Collect();
        MessageBox.Show(String.Format("After GC.Collect the object _customers[0] is alive: {0}", _ordRef.IsAlive));
        MessageBox.Show(String.Format("After GC.Collect the object _customers[0].BillingAddress is alive: {0}", _ordRef2.IsAlive));
    }                        


    private void btnChange1_Click(object sender, EventArgs e)
    {
        List<Order> orders = (List<Order>)ordersBindingSource.DataSource;
        orders[2].Customer = orders[1].Customer;
    }

    private void btnChange2_Click(object sender, EventArgs e)
    {
        List<Order> orders = (List<Order>)ordersBindingSource.DataSource;
        //orders[1].Customer.Name = orders[1].Customer.Name + "^";
        //orders[0].Customer.Name = orders[0].Customer.Name + "^";
        orders[0].OrderLines[0].Product = orders[1].OrderLines[1].Product;
        orders[0].OrderLines[0].Quantity = orders[0].OrderLines[0].Quantity + 10;

        _customers[0].Name = _customers[0].Name + "*";
        _products[1].Name = _products[1].Name + "*";
    }

    private void textBox1_Validated(object sender, EventArgs e)
    {
        try
        {
            DBG.MaxNivelDepuracion = Convert.ToInt32(textBox1.Text);
        }
        catch
        { }
    }


    private void ordersBindingSource_CreatingObject(object Sender, Type ObjectType, ref object Obj)
    {    
        if (ObjectType == typeof(Customer))
        {
            Obj = new Customer();
            _customers.Add((Customer)Obj);
            customersBindingSource.ResetBindings(false);
        }
    }

    
}
