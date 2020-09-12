using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicMapper.Compiled {
    class CompiledCategory<TSource> {

        public string Identifier { get; set; }
        public List<CompiledMember<TSource>> Fields { get; set; }

        public CompiledCategory(string identifier, List<CompiledMember<TSource>> fields) {
            Identifier = identifier ?? throw new ArgumentNullException("DynamicMapper.CompiledCategory: identifier cannot be null!");
            Fields = fields ?? throw new ArgumentNullException("DynamicMapper.CompiledCategory: fields cannot be null!");
        }

    }
}
