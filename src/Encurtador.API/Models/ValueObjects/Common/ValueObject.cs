using System;
namespace Encurtador.API.Models.ValueObjects.Common
{
    public abstract class ValueObject
    {


        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}

