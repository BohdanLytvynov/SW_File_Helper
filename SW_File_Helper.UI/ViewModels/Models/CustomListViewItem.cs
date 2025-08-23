using SW_File_Helper.ViewModels.Base.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW_File_Helper.ViewModels.Models
{
    public class CustomListViewItem : ValidatableViewModel, IEquatable<CustomListViewItem>
    {
        #region Fields
        private int m_id;
        #endregion

        #region Properties
        public int Id { get=>m_id; set=>Set(ref m_id, value); }
        #endregion

        #region Ctor
        public CustomListViewItem() : this(-1)
        {
            
        }

        public CustomListViewItem(int id)
        {
            m_id = id;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{Id})";
        }

        public bool Equals(CustomListViewItem? other)
        {
            if(other == null) throw new ArgumentNullException("other");

            return this.m_id == other.m_id;
        }
        #endregion
    }
}
