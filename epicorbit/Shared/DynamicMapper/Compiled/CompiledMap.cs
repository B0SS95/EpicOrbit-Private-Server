using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicMapper.Compiled {
    static class CompiledMap<TType1, TType2> where TType1 : class where TType2 : class {

        internal static List<(CompiledMember<TType1>, CompiledMember<TType2>)> members;

        static CompiledMap() {
            members = new List<(CompiledMember<TType1>, CompiledMember<TType2>)>();
            foreach (CompiledMember<TType1> type1member in Mapper<TType1>.members.Values) {
                foreach (CompiledMember<TType2> type2member in Mapper<TType2>.members.Values) {
                    if (type1member.Identifier == type2member.Identifier
                        && type1member.Type == type2member.Type) {
                        members.Add((type1member, type2member));
                    }
                }
            }
        }

    }
}
