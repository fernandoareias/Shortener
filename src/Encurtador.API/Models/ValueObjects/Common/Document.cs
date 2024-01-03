using System;
namespace Encurtador.API.Models.ValueObjects.Common
{
    public abstract class Document : ValueObject
    {
        protected Document()
        {

        }

        protected Document(string number)
        {
            Number = number;
        }

        public string Number{
            get;
            private set;
        }

        public override string ToString()
        {
            return Number.ToString();
        }

        public virtual string GetNumberWithMask()
        {
            throw new NotImplementedException();
        }
    }
}

