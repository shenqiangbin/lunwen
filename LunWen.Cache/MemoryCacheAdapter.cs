using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using System.Threading;
using LunWen.Infrastructure;

namespace LunWen.Cache
{
    public class MemoryCacheAdapter : ICache
    {
        private MemoryCache _memoryCache;
        private ReaderWriterLockSlim cacheLock;

        public MemoryCacheAdapter(string name)
        {
            _memoryCache = new MemoryCache(name);
            cacheLock = new ReaderWriterLockSlim();
        }

        public T Get<T>(string key) where T : class
        {
            object obj = null;
            try
            {
                cacheLock.EnterReadLock();
                obj = _memoryCache.Get(key);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
            }
            finally
            {
                cacheLock.ExitReadLock();
            }
            return (T)obj;
        }

        public void Remove(string key)
        {
            try
            {
                cacheLock.EnterWriteLock();
                _memoryCache.Remove(key);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }

        }

        public void Store(string key, object value)
        {
            try
            {
                cacheLock.EnterWriteLock();
                _memoryCache.Set(key, value, DateTime.Now.AddSeconds(1800));
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }
        }
    }
}
