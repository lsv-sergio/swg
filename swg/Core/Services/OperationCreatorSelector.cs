using swg.Attributes.Core.Attributes;
using swg.Core.Creators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace swg.Core.Services {
    public class OperationCreatorSelector {
        public IEnumerable<IOperationCreator> Select(Assembly assembly) {
            if (assembly == null) {
                throw new ArgumentNullException("assembly");
            }
            return assembly.GetTypes()
                .Where(type => type.GetCustomAttributes(typeof(OperationAttribute), false) != null && type.BaseType == typeof(OperationCreator))
                .Select(type => Activator.CreateInstance(type) as IOperationCreator);
        }
    }

}