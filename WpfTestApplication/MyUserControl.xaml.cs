using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace WpfTestApplication
{
    /// <summary>
    /// Interaction logic for MyUserControl.xaml
    /// </summary>
    public partial class MyUserControl : UserControl,INotifyPropertyChanged
    {
        public MyUserControl()
        {
            InitializeComponent();
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChange(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }



        public string ControlText
        {
            get { return (string)GetValue(ControlTextProperty); }
            set { SetValue(ControlTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Caption.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ControlTextProperty =
            DependencyProperty.Register("ControlText", typeof(string), typeof(MyUserControl), new PropertyMetadata(string.Empty, OnCaptionPropertyChanged));

        private static void OnCaptionPropertyChanged(DependencyObject dependencyObject,DependencyPropertyChangedEventArgs e)
        {
            MyUserControl myControl = dependencyObject as MyUserControl;
            myControl.RaisePropertyChange("ControlText");
            myControl.OnCaptionPropertyChanged();
        }
        
        private void OnCaptionPropertyChanged()
        {
        //    txbCaption.Text=ControlText;
        }




        public string ForeColour
        {
            get { return (string)GetValue(ForeColourProperty); }
            set { SetValue(ForeColourProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ForeColour.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ForeColourProperty =
            DependencyProperty.Register("ForeColour", typeof(string), typeof(MyUserControl), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.None, PropertyChangeCallback, OnCoerceCurrentTimeProperty), OnValidateCurrentTimeProperty);

        private static void PropertyChangeCallback(DependencyObject obj,DependencyPropertyChangedEventArgs e)
        {
            MyUserControl myControl = obj as MyUserControl;
            myControl.RaisePropertyChange("ForeColour");
            myControl.OnColorPropertyChange();
        }

        private void OnColorPropertyChange()
        {
            txbCaption.Foreground = new SolidColorBrush((Color)TypeDescriptor.GetConverter(typeof(Color)).ConvertFromString(this.ForeColour));
        }

        private static object OnCoerceCurrentTimeProperty(DependencyObject sender, object data)
        {
            if (data.ToString().Length > 3)
                data = data.ToString().Substring(0, 3);
            return data;
        }

        private static bool OnValidateCurrentTimeProperty(object data)
        {
            if (data.ToString() == "Red")
                return true;
            else return false;
        }


        #region ReadOnly DP


        public bool IsMouseOver
        {
            get { return (bool)GetValue(IsMouseOverProperty); }
            set { SetValue(IsMouseOverProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsMouseOver.  This enables animation, styling, binding, etc...
        public static readonly DependencyPropertyKey IsMouseOverPropertyKey =
            DependencyProperty.RegisterReadOnly("IsMouseOver", typeof(bool), typeof(MyUserControl),new PropertyMetadata(false));

        public static readonly DependencyProperty IsMouseOverProperty = IsMouseOverPropertyKey.DependencyProperty;
        #endregion
    }
}
