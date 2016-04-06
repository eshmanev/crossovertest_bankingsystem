﻿using System;

namespace BankingSystem.DAL
{
    /// <summary>
    ///     Defines a database transaction.
    /// </summary>
    public interface IDatabaseTransaction : IDisposable
    {
        /// <summary>
        ///     Commits the transaction.
        /// </summary>
        void Commit();

        /// <summary>
        ///     Rollbacks the transaction.
        /// </summary>
        void Rollback();
    }
}