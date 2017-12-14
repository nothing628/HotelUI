using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel.App.Provider
{
    public abstract class ServiceProvider
    {
        public abstract void Register();
        public abstract void Boot();
        public abstract string Provide();
    }
}
