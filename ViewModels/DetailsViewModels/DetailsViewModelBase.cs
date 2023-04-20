﻿using GoT_Wiki.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace GoT_Wiki.ViewModels
{
    public abstract class DetailsViewModelBase<TClass> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private TClass _item = default;
        public TClass Item
        {
            get { return _item; }
            set
            {
                if (!EqualityComparer<TClass>.Default.Equals(_item, value))
                {
                    _item = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Item)));
                }
            }
        }

        protected readonly ServiceBase<TClass> service = null;

        public DetailsViewModelBase(ServiceBase<TClass> service)
        {
            this.service = service;
        }

        public async Task Load(TClass elemenet)
        {
            Item = elemenet;
            await OnLoad();
        }

        protected virtual async Task OnLoad() { }

        protected void FirePropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}
