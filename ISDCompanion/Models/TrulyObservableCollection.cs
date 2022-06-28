using ISDCompanion.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;

namespace ISDCompanion.Models
{
    //Source = https://stackoverflow.com/questions/1427471/observablecollection-not-noticing-when-item-in-it-changes-even-with-inotifyprop
    public sealed class TrulyObservableCollection<T> : ObservableCollection<T>
        where T : INotifyPropertyChanged, IExpandable
    {

        private int _height = 0;
        private void RecalcHeight()
        {
            if (_height == 0)
            {
                foreach (var item in this)
                {
                    _height += item.getHeight();
                }
            }
        }
        public int Height
        {
            get
            {
                RecalcHeight();
                return _height;
            }
        }

        //public TrulyObservableCollection()
        //{
        //    CollectionChanged += FullObservableCollectionCollectionChanged;
        //}

        //public TrulyObservableCollection(IEnumerable<T> pItems) : this()
        //{
        //    foreach (var item in pItems)
        //    {
        //        this.Add(item);
        //    }
        //}

        //private void FullObservableCollectionCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    if (e.NewItems != null)
        //    {
        //        foreach (Object item in e.NewItems)
        //        {
        //            ((INotifyPropertyChanged)item).PropertyChanged += ItemPropertyChanged;
        //        }
        //    }
        //    if (e.OldItems != null)
        //    {
        //        foreach (Object item in e.OldItems)
        //        {
        //            ((INotifyPropertyChanged)item).PropertyChanged -= ItemPropertyChanged;
        //        }
        //    }
        //}

        //private void ItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    try
        //    {


        //        NotifyCollectionChangedEventArgs args =
        //            new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, sender, sender, IndexOf((T)sender));
        //        OnCollectionChanged(args);
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
    }
}
