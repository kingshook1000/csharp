using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Iterator
{
    public class DynamicList<T>: IEnumerable<T>
    {
        int len;
        T[] arr;
        int capacity;

        public DynamicList() : this(16) { }

        public DynamicList(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentException("Capacity less than 0");
            }
            this.capacity = capacity;
            arr = new T[capacity];

        }



    }
}
