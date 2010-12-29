using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace ObjectBindingSourceDemo
{
    public class Order : BusinessBase
    {
        private int _orderNumber;
        private DateTime _orderDate;
        private Customer _customer;
        private List<OrderLine> _orderLines = new List<OrderLine>();
        private Address _deliveryAddress;

        public Order()
        {
        }

        public Order(int orderNumber, DateTime orderDate,Customer customer)
        {
            _orderNumber = orderNumber;
            _orderDate = orderDate;
            _customer = customer;
        }


        public int OrderNumber
        {
            get { return _orderNumber; }
            set
            {
                _orderNumber = value;
                OnPropertyChanged("OrderNumber");
            }
        }


        public Address DeliveryAddress
        {
            get { return _deliveryAddress; }
            set
            {
                _deliveryAddress = value;
                OnPropertyChanged("DeliveryAddress");
            }
        }

        public DateTime OrderDate
        {
            get { return _orderDate; }
            set
            {
                _orderDate = value;
                OnPropertyChanged("OrderDate");
            }
        }


        public Customer Customer
        {
            get { return _customer; }
            set
            {
                _customer = value;
                OnPropertyChanged("Customer");
            }
        }


        public List<OrderLine> OrderLines
        {
            get { return _orderLines; }
        }

        public override string ToString()
        {
            return String.Format("<Order:{0}>", OrderNumber );
        }
        
    }
}
