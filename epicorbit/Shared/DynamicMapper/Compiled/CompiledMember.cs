using System;

namespace DynamicMapper.Compiled {
    class CompiledMember<TSource> {

        public string Identifier { get; set; }
        public Type Type { get; set; }

        public Func<TSource, object> Get { get; set; }
        public Action<TSource, object> Set { get; set; }

        public CompiledMember(string identifier, Type dataType, Func<TSource, object> valueRetriever, Action<TSource, object> valueSetter) {
            Identifier = identifier ?? throw new ArgumentNullException("DynamicMapper.CompiledMember: identifier cannot be null!");
            Type = dataType ?? throw new ArgumentNullException("DynamicMapper.CompiledMember: dataType cannot be null!");
            Get = valueRetriever;
            Set = valueSetter;
        }

    }
}
