﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EpicOrbit.Server.Data.Synchronisation {
    public class DisposableLockWrapper : IDisposable {

        private readonly ReaderWriterLockSlim readerWriterLock;
        private readonly LockType lockType;

        public DisposableLockWrapper(ReaderWriterLockSlim readerWriterLock, LockType lockType) {
            this.readerWriterLock = readerWriterLock;
            this.lockType = lockType;

            switch (this.lockType) {
                case LockType.Read:
                    this.readerWriterLock.EnterReadLock();
                    break;

                case LockType.UpgradeableRead:
                    this.readerWriterLock.EnterUpgradeableReadLock();
                    break;

                case LockType.Write:
                    this.readerWriterLock.EnterWriteLock();
                    break;
            }
        }

        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                // dispose managed objects
                switch (lockType) {
                    case LockType.Read:
                        readerWriterLock.ExitReadLock();
                        break;

                    case LockType.UpgradeableRead:
                        readerWriterLock.ExitUpgradeableReadLock();
                        break;

                    case LockType.Write:
                        readerWriterLock.ExitWriteLock();
                        break;
                }
            }
        }
    }
}
