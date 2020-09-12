using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using EpicOrbit.Server.Data.Synchronisation;

namespace EpicOrbit.Server.Data.Extensions {
    public static class ReaderWriterLockSlimExtension {

        public static IDisposable ObtainReadLock(this ReaderWriterLockSlim readerWriterLock) {
            return new DisposableLockWrapper(readerWriterLock, LockType.Read);
        }

        public static IDisposable ObtainUpgradeableReadLock(this ReaderWriterLockSlim readerWriterLock) {
            return new DisposableLockWrapper(readerWriterLock, LockType.UpgradeableRead);
        }

        public static IDisposable ObtainWriteLock(this ReaderWriterLockSlim readerWriterLock) {
            return new DisposableLockWrapper(readerWriterLock, LockType.Write);
        }
    }
}
