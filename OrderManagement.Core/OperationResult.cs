using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Core
{
    /// <summary>
    /// Represents a class which defines operations result object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class OperationResult
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets error list
        /// </summary>
        public List<string> Errors { get; set; }

        /// <summary>
        /// Gets or sets whether operation is succeeded
        /// </summary>
        public bool IsSucceed
        {
            get
            {
                return Errors.Count == 0;
            }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes a new instance of OperationResult class
        /// </summary>
        public OperationResult()
        {
            Errors = new List<string>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds an error to error list
        /// </summary>
        /// <param name="error"></param>
        public void AddError(string error)
        {
            Errors.Add(error);
        }

        /// <summary>
        /// Formats erros
        /// </summary>
        /// <returns></returns>
        public string FormatErrors()
        {
            var sb = new StringBuilder();

            foreach (var error in Errors)
            {
                sb.AppendLine(error);
            }

            return sb.ToString();
        }
        #endregion
    }

    public class OperationResult<T> : OperationResult
    {

        #region Public Properties

        /// <summary>
        /// Gets or sets item 
        /// </summary>
        public T Item { get; set; }
        #endregion
    }
}
