﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using meramedia.Linq.Core.Node;

namespace meramedia.Linq.Core
{
    /// <summary>
    /// The umbracoDataContext which handles the interaction with an <see cref="UmbracoDataProvider"/>
    /// </summary>
    public abstract class UmbracoDataContext : IDisposable
    {
        /// <summary>
        /// Gets the data provider.
        /// </summary>
        /// <value>The data provider.</value>
        private static readonly Lazy<UmbracoDataProvider> _dataProvider = new Lazy<UmbracoDataProvider>(() => new NodeDataProvider());
        public UmbracoDataProvider DataProvider
        {
            get { return _dataProvider.Value; }
        }

        /// <summary>
        /// Useless ctors needed because of generated DataContext file created by umbraco.
        /// </summary>
        protected UmbracoDataContext()
        { }


        /// <summary>
        /// Loads the tree of umbraco items.
        /// </summary>
        /// <typeparam name="TDocTypeBase">The type of the DocTypeBase.</typeparam>
        /// <returns>Collection of umbraco items</returns>
        /// <exception cref="System.ObjectDisposedException">If the DataContext has been disposed of</exception>        
        protected Tree<TDocTypeBase> LoadTree<TDocTypeBase>() where TDocTypeBase : DocTypeBase, new()
        {
            CheckDisposed();
            return DataProvider.LoadTree<TDocTypeBase>();
        }
       

        #region IDisposable Members

        private bool _disposed;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                if (DataProvider != null)
                {
                    DataProvider.Dispose(true);
                }

                _disposed = true;
            }
        }

        private void CheckDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(null);
            }
        }

        #endregion
    }
}
