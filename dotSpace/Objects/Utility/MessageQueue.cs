using dotSpace.BaseClasses;
using dotSpace.Objects.Network;
using System.Collections.Generic;
using System.Threading;

namespace dotSpace.Objects.Utility
{
    public sealed class MessageQueue
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Fields

        private readonly Dictionary<string, MessageBase> messages;
        private readonly ReaderWriterLockSlim rwLock;

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public MessageQueue()
        {
            this.messages = new Dictionary<string, MessageBase>();
            this.rwLock = new ReaderWriterLockSlim();
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        public MessageBase Get(string key)
        {
            MessageBase t = this.WaitForSession(key);
            bool successs = true;
            rwLock.EnterWriteLock();
            successs = messages.Remove(key);
            rwLock.ExitWriteLock();
            return successs ? t : null;
        }

        public void Put(MessageBase message)
        {
            rwLock.EnterWriteLock();
            messages.Add(message.Session, message);
            rwLock.ExitWriteLock();
            this.Awake(messages);
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Private Methods

        private MessageBase WaitForSession(string key)
        {
            MessageBase t;
            while (((t = this.Find(key)) == null))
            {
                this.Wait(messages);
            }
            return t;
        }
        private MessageBase Find(string key)
        {
            rwLock.EnterReadLock();
            MessageBase msg = this.messages.ContainsKey(key) ? this.messages[key] : null;
            rwLock.ExitReadLock();
            return msg;
        }

        private void Wait(object _lock)
        {
            Monitor.Enter(_lock);
            Monitor.Wait(_lock);
            Monitor.Exit(_lock);
        }
        private void Awake(object _lock)
        {
            Monitor.Enter(_lock);
            Monitor.PulseAll(_lock);
            Monitor.Exit(_lock);
        }

        #endregion
    }
}
