using System.ComponentModel;

namespace SW_File_Helper.ViewModels.Base.VM
{
    public abstract class ValidatableViewModel : ViewModelBase, IDataErrorInfo
    {
        #region Fields
        private bool[] m_ValidArray;
        #endregion

        #region IDataErrorInfo
        public virtual string Error => throw new NotImplementedException();

        public virtual string this[string columnName] => throw new NotImplementedException();
        #endregion

        #region Methods

        protected void InitValidArray(int count)
        { 
            m_ValidArray = new bool[count];
        }

        protected bool ValidateFields(int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                if (!m_ValidArray[i])
                    return false;
            }

            return true;
        }

        protected void ResetValidArray()
        {
            int len = m_ValidArray.Length;

            for (int i = 0; i < len; i++)
            {
                m_ValidArray[i] = false;
            }
        }

        protected void SetValidArrayValue(int pos, bool value)
        {
            m_ValidArray[pos] = value;
        }

        protected int GetValidArrayCount()
        {
            return m_ValidArray.Length;
        }

        protected int GetLastIndexOfValidArray()
        {
            return m_ValidArray.Length - 1;
        }

        #endregion
    }
}
