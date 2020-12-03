using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Iterator
{
    public class Person
    {
        public string name;
        public Person(string name)
        {
            this.name = name;
        }
    }

    class People: IEnumerable
    {
        private Person[] _people;

        public People(Person[] people)
        {
            _people = new Person[people.Length];
            for (int i = 0;i < people.Length; i++)
            {
                _people[i] = people[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new PeopleEnumerator(_people);
        }
    }

    public class PeopleEnumerator : IEnumerator
    {
        private Person[] _people;
        private int position = -1;
        public PeopleEnumerator(Person[] people)
        {
            this._people = people;
        }

        public object Current
        { 
            get
            {
                try 
                {
                    return this._people[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public bool MoveNext()
        {
            this.position++;
            return this.position < this._people.Length;
        }

        public void Reset()
        {
            this.position = -1;
        }
    }
}
