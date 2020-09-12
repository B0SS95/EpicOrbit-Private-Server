using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicMapper.Attributes {
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public class Category : Attribute {

        public string Identifier { get; set; }

        public Category(string identifier) {
            Identifier = identifier ?? throw new ArgumentNullException("DynamicMapper.Category.Name: identifier cannot be null!");
        }

    }
}
