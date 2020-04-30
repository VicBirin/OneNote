using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace OneNote.Sample.Api
{
    public abstract class CompositeElement<T> : Element, ICompositeElement, IEnumerable<T>, IEnumerable where T : IElement
    {
        List<T> items = new List<T>();

        public T this[int index]
        {
            get { return items[index]; }
            set { items.Insert(index, value); }
        }

        public void AddChildElement(T item)
        {
            items.Add(item);
        }

        protected CompositeElement(ElementType elementType)
        {
            ElementType = elementType;
        }

        public List<E> GetChildElements<E>() where E : Element
        {
            return items.Cast<E>().ToList();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}