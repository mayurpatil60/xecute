using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XP.DataAccess
{
    /// <summary>
    /// Class to set parameter values
    /// </summary>
    public class DbParameter
    {
        #region Declarations
        private string mName = string.Empty;
        private object mValue = null;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a parameter with the name and value specified. 
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="value">Value associated with the parameter</param>
        public DbParameter(string name, object value)
        {
            mName = name;
            mValue = value;
        }

        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the name of the parameter.
        /// </summary>
        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }

        /// <summary>
        /// Gets or sets the value associated with the parameter.
        /// </summary>
        public object Value
        {
            get { return mValue; }
            set { mValue = value; }
        }
        #endregion

    }
}
