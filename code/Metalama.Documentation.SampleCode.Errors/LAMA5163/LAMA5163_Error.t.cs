// Warning LAMA5163 on `City`: `The 'Customer.DeliveryAddress' property cannot be observed: changes to children of non-auto properties declared on the current type cannot be observed unless the property type implements INotifyPropertyChanged. Consider implementing the INotifyPropertyChanged interface in 'Customer', marking 'Customer.DeliveryAddress' with [Constant], or using ConfigureObservability via a fabric.`
using System.ComponentModel;
using Metalama.Patterns.Observability;
namespace Doc.LAMA5163.Error;
[Observable]
public partial class Customer : INotifyPropertyChanged
{
  private bool _shipToBillingAddress = true;
  public bool ShipToBillingAddress
  {
    get
    {
      return _shipToBillingAddress;
    }
    set
    {
      if (_shipToBillingAddress != value)
      {
        _shipToBillingAddress = value;
        OnPropertyChanged("DeliveryAddress");
        OnPropertyChanged("ShipToBillingAddress");
      }
    }
  }
  private Address _billingAddress = new();
  public Address BillingAddress
  {
    get
    {
      return _billingAddress;
    }
    set
    {
      if (!object.ReferenceEquals(value, _billingAddress))
      {
        var oldValue = _billingAddress;
        if (oldValue != null)
        {
          oldValue.PropertyChanged -= _handleBillingAddressPropertyChanged;
        }
        _billingAddress = value;
        OnObservablePropertyChanged("BillingAddress", oldValue, (INotifyPropertyChanged? )value);
        OnPropertyChanged("DeliveryAddress");
        OnPropertyChanged("BillingAddress");
        SubscribeToBillingAddress(value);
      }
    }
  }
  private Address _shippingAddress = new();
  public Address ShippingAddress
  {
    get
    {
      return _shippingAddress;
    }
    set
    {
      if (!object.ReferenceEquals(value, _shippingAddress))
      {
        var oldValue = _shippingAddress;
        if (oldValue != null)
        {
          oldValue.PropertyChanged -= _handleShippingAddressPropertyChanged;
        }
        _shippingAddress = value;
        OnObservablePropertyChanged("ShippingAddress", oldValue, (INotifyPropertyChanged? )value);
        OnPropertyChanged("DeliveryAddress");
        OnPropertyChanged("ShippingAddress");
        SubscribeToShippingAddress(value);
      }
    }
  }
  public Address DeliveryAddress => this.ShipToBillingAddress ? this.BillingAddress : this.ShippingAddress;
  // Warning: DeliveryAddress isn't an auto-property, so changes to its City can't be observed.
  public string DeliveryCity => this.DeliveryAddress.City;
  private PropertyChangedEventHandler? _handleBillingAddressPropertyChanged;
  private PropertyChangedEventHandler? _handleShippingAddressPropertyChanged;
  public Customer()
  {
    SubscribeToBillingAddress(BillingAddress);
    SubscribeToShippingAddress(ShippingAddress);
  }
  [ObservedExpressions("BillingAddress", "ShippingAddress")]
  protected virtual void OnChildPropertyChanged(string parentPropertyPath, string propertyName)
  {
  }
  [ObservedExpressions("BillingAddress", "ShippingAddress")]
  protected virtual void OnObservablePropertyChanged(string propertyPath, INotifyPropertyChanged? oldValue, INotifyPropertyChanged? newValue)
  {
  }
  protected virtual void OnPropertyChanged(string propertyName)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
  private void SubscribeToBillingAddress(Address value)
  {
    if (value != null)
    {
      _handleBillingAddressPropertyChanged ??= HandlePropertyChanged;
      value.PropertyChanged += _handleBillingAddressPropertyChanged;
    }
    void HandlePropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
      {
        var propertyName = e.PropertyName;
        switch (propertyName)
        {
          default:
            OnChildPropertyChanged("BillingAddress", propertyName);
            break;
        }
      }
    }
  }
  private void SubscribeToShippingAddress(Address value)
  {
    if (value != null)
    {
      _handleShippingAddressPropertyChanged ??= HandlePropertyChanged;
      value.PropertyChanged += _handleShippingAddressPropertyChanged;
    }
    void HandlePropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
      {
        var propertyName = e.PropertyName;
        switch (propertyName)
        {
          default:
            OnChildPropertyChanged("ShippingAddress", propertyName);
            break;
        }
      }
    }
  }
  public event PropertyChangedEventHandler? PropertyChanged;
}