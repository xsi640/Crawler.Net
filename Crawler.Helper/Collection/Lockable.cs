﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Crawler.Net.Collection
{
    public class Lockable
    {
        [NonSerialized]
        private readonly static int SpinCycles = 20;//Properties.Settings.Default.SpinCycles;

        [NonSerialized]
        protected static long _conflicts;

        [NonSerialized]
        protected int _lock;

        /// <summary>
        /// Returns the number of lock conflicts that have occurred
        /// </summary>
        public static long Conflicts
        {
            get { return _conflicts; }
        }

        /// <summary>
        /// Aquire the lock
        /// </summary>
        public void AquireLock()
        {
            // Assume that we will grab the lock - call CompareExchange
            if (Interlocked.CompareExchange(ref _lock, 1, 0) == 1)
            {
                int n = 0;

                // Could not grab the lock - spin/wait until the lock looks obtainable
                while (_lock == 1)
                {
                    if (n++ > SpinCycles)
                    {
#if TrackConflicts
                        Interlocked.Increment(ref _conflicts);
#endif
                        n = 0;
                        Thread.Sleep(0);
                    }
                }

                // Try to grab the lock - call CompareExchange
                while (Interlocked.CompareExchange(ref _lock, 1, 0) == 1)
                {
                    n = 0;

                    // Someone else grabbed the lock.  Continue to spin/wait until the lock looks obtainable
                    while (_lock == 1)
                    {
                        if (n++ > SpinCycles)
                        {
#if TrackConflicts
                            Interlocked.Increment(ref _conflicts);
#endif
                            n = 0;
                            Thread.Sleep(0);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Release the lock
        /// </summary>
        public void ReleaseLock()
        {
            _lock = 0;
        }

        public bool Locked
        {
            get { return (_lock != 0); }
        }

        public bool TryAquireLock()
        {
            if (_lock == 1) return false;
            return (Interlocked.CompareExchange(ref _lock, 1, 0) != 1);
        }
    }
}
