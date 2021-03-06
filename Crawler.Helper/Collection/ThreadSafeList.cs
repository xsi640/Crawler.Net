﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Crawler.Net.Collection
{
    public delegate bool Match<TListType>(TListType item);

    /// <summary>
    /// 线程安全的List集合
    /// </summary>
    /// <typeparam name="TListType"></typeparam>
    public class ThreadSafeList<TListType> : Lockable where TListType : class
    {
        [NonSerialized]
        protected List<TListType> _listCached;
        public List<TListType> _list = new List<TListType>();
        protected int _count;
        [NonSerialized]
        public bool _changed;

        public void SyncCount()
        {
            _count = _list.Count;
        }

        public TListType GetLastItemInList()
        {
            TListType item = null;

            if (_count > 0)
            {
                AquireLock();
                {
                    if (_count > 0)
                        item = _list[_count - 1];
                }
                ReleaseLock();
            }

            return item;
        }

        public TListType GetItem(int index)
        {
            TListType item = null;

            AquireLock();
            {
                if (index < _count)
                    item = _list[index];
            }
            ReleaseLock();

            return item;
        }

        public void Add(TListType item)
        {
            AquireLock();
            {
                _list.Add(item);
                _count++;
                _changed = true;
            }
            ReleaseLock();
        }

        public void Add(List<TListType> items)
        {
            AquireLock();
            {
                _list.AddRange(items);
                _count += items.Count;
                _changed = true;
            }
            ReleaseLock();
        }

        public void Add(TListType[] items)
        {
            AquireLock();
            {
                _list.AddRange(items);
                _count += items.Length;
                _changed = true;
            }
            ReleaseLock();
        }

        public bool IsInList(TListType item)
        {
            bool found;

            AquireLock();
            {
                found = _list.Contains(item);
            }
            ReleaseLock();

            return found;
        }

        public TListType[] ArrayOfItems { get { return AllItems.ToArray(); } }
        public int Count { get { return _count; } }

        public List<TListType> AllItems
        {
            get
            {
                List<TListType> list;
                if (_changed || _listCached == null)
                {
                    list = new List<TListType>(_list);
                    AquireLock();
                    {
                        Interlocked.Exchange<List<TListType>>(ref _listCached, list);
                        _changed = false;
                    }
                    ReleaseLock();
                }
                else
                {
                    list = _listCached;
                }

                return list;
            }
        }

        public int RemoveMultiple(Match<TListType> match)
        {
            int removed = 0;

            AquireLock();
            {
                for (int n = _list.Count - 1; n >= 0; n--)
                {
                    TListType item = _list[n];
                    if (match(item))
                    {
                        _list.RemoveAt(n);
                        _count--;
                        _changed = true;
                        removed++;
                    }
                }
            }
            ReleaseLock();

            return removed;
        }

        public int RemoveMultiple(List<TListType> lists)
        {
            int removed = 0;
            if (lists != null && lists.Count > 0)
            {
                AquireLock();
                {
                    for (int i = 0; i < lists.Count; i++)
                    {
                        _list.Remove(lists[i]);
                        _count--;
                        _changed = true;
                        removed++;
                    }
                }
                ReleaseLock();
            }
            return removed;
        }

        public bool Remove(TListType item)
        {
            AquireLock();
            {
                if (_list.Remove(item))
                {
                    _count--;
                    _changed = true;
                }
            }
            ReleaseLock();

            return true;
        }

        public bool RemoveAt(int index)
        {
            AquireLock();
            try
            {
                _list.RemoveAt(index);
                _count--;
                _changed = true;
            }
            finally
            {
                ReleaseLock();
            }

            return true;
        }

        public TListType GetRemoveSingle(Match<TListType> match)
        {
            TListType result = null;

            AquireLock();
            {
                for (int n = _list.Count - 1; n >= 0; n--)
                {
                    TListType item = _list[n];
                    if (match(item))
                    {
                        result = item;
                        _list.RemoveAt(n);
                        _count--;
                        _changed = true;
                        break;
                    }
                }
            }
            ReleaseLock();

            return result;
        }

        public List<TListType> GetRemoveMultiple(Match<TListType> match)
        {
            List<TListType> result = new List<TListType>();

            AquireLock();
            {
                for (int n = _list.Count - 1; n >= 0; n--)
                {
                    TListType item = _list[n];
                    if (match(item))
                    {
                        result.Add(item);
                        _list.RemoveAt(n);
                        _count--;
                        _changed = true;
                    }
                }
            }
            ReleaseLock();

            return result;
        }

        public void Clear()
        {
            AquireLock();
            {
                _list.Clear();
                _count = 0;
                _changed = true;
            }
            ReleaseLock();
        }

        public TListType Find(TListType item)
        {
            int index;

            AquireLock();
            {
                index = _list.IndexOf(item);
            }
            ReleaseLock();

            return (index >= 0) ? item : null;
        }

        public TListType Find(Match<TListType> match)
        {
            TListType item = null;
            AquireLock();
            {
                for (int n = 0; n < _count; n++)
                {
                    if (match(_list[n]))
                    {
                        item = _list[n];
                        break;
                    }
                }
            }
            ReleaseLock();

            return item;
        }

        public List<TListType> FindMultiple(Match<TListType> match)
        {
            List<TListType> items = new List<TListType>();
            AquireLock();
            {
                for (int n = 0; n < _count; n++)
                {
                    if (match(_list[n]))
                    {
                        items.Add(_list[n]);
                    }
                }
            }
            ReleaseLock();

            return items;
        }

        public TListType FindSingle(Match<TListType> match)
        {
            TListType item = null;
            AquireLock();
            {
                for (int n = 0; n < _count; n++)
                {
                    if (match(_list[n]))
                    {
                        item = _list[n];
                        break;
                    }
                }
            }
            ReleaseLock();
            return item;
        }
    }
}
