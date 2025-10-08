using SW_File_Helper.Interfaces;
using SW_File_Helper.ViewModels.Base.VM;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW_File_Helper.ViewModels.Models
{
    public abstract class CustomListViewItem : ValidatableViewModel, IEquatable<CustomListViewItem>
    {
        #region Fields
        private int m_number;

        private bool m_IsValid;

        private bool m_IsEnabled;
        #endregion

        #region Properties

        public int Number { get=>m_number; set=>Set(ref m_number, value); }

        public bool IsEnabled
        { get => m_IsEnabled; set => Set(ref m_IsEnabled, value); }

        public bool IsValid 
        { get => m_IsValid; protected set => Set(ref m_IsValid, value); }
        #endregion

        #region Ctor
        public CustomListViewItem() : this(-1)
        {

        }

        public CustomListViewItem(int number)
        {
            m_number = number;
            m_IsValid = false;
            m_IsEnabled = true;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{Number})";
        }

        public bool Equals(CustomListViewItem? other)
        {
            if(other == null) throw new ArgumentNullException("other");

            return this.m_number == other.m_number;
        }

        #endregion
    }
}
