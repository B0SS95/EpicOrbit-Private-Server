using DynamicMapper.Attributes;
using DynamicMapper.Compiled;
using DynamicMapper.Foundations;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace DynamicMapper {
    public static class Mapper<T> where T : class {

        #region Compiler
        internal static bool hasEmptyConstructor = false;
        internal static Dictionary<string, CompiledCategory<T>> categories = new Dictionary<string, CompiledCategory<T>>();
        internal static Dictionary<string, CompiledMember<T>> members = new Dictionary<string, CompiledMember<T>>();

        /// <summary>
        /// Diese Method wird pro Typ ein Mal ausgeführt und "compiled" diesen, 
        /// damit die gemappten Objekte schneller gelesen/beschrieben werden können
        /// </summary>
        static Mapper() {
            hasEmptyConstructor = typeof(T).GetConstructor(Type.EmptyTypes) != null;
            typeof(T).GetMembers().Where(x => x is FieldInfo || x is PropertyInfo)
                .ToList().ForEach(x => Compile(x));
        }

        private static void Compile(MemberInfo member) {
            CompiledMember<T> compiledMember = null;
            try {

                compiledMember = new CompiledMember<T>(member.Name, ValueType(member), CompiledRetriever(member), CompiledSetter(member));
                if (!members.ContainsKey(compiledMember.Identifier)) {
                    members.Add(compiledMember.Identifier, compiledMember);
                } else {
                    throw new Exception($"DynamicMapper.Mapper: failed to add member [{compiledMember.Identifier}]!");
                }

            } catch {
                throw new InvalidOperationException($"DynamicMapper.Mapper: cannot access member [{member.Name}]!");
            }

            if (compiledMember != null) {
                foreach (Category category in member.GetCustomAttributes<Category>()) {
                    ExportCategory(compiledMember, category);
                }
            }
        }

        public static Type ValueType(MemberInfo member) {
            switch (member) {
                case FieldInfo field:
                    return field.FieldType;
                case PropertyInfo property:
                    return property.PropertyType;
            }
            return null;
        }

        private static Func<T, object> CompiledRetriever(MemberInfo member) {
            try {
                switch (member) {
                    case FieldInfo field:
                        return DelegateFactory.Get<T>(field);
                    case PropertyInfo property:
                        return DelegateFactory.Get<T>(property);
                }
            } catch { }
            return null;
        }

        private static Action<T, object> CompiledSetter(MemberInfo member) {
            try {
                switch (member) {
                    case FieldInfo field:
                        return DelegateFactory.Set<T>(field);
                    case PropertyInfo property:
                        return DelegateFactory.Set<T>(property);
                }
            } catch { }
            return null;
        }

        private static void ExportCategory(CompiledMember<T> member, Category category) {
            if (category == null) {
                return;
            }

            if (!categories.TryGetValue(category.Identifier, out CompiledCategory<T> compiledCategory)) {
                if (categories.ContainsKey(category.Identifier)) {
                    categories.Add(category.Identifier, compiledCategory = new CompiledCategory<T>(category.Identifier, new List<CompiledMember<T>>()));
                } else {
                    throw new Exception($"DynamicMapper.Mapper: failed to create category [{category.Identifier}]");
                }
            }

            if (compiledCategory.Fields.Where(x => x.Identifier == member.Identifier).Count() > 0) {
                throw new Exception($"DynamicMapper.Mapper: duplicate [{member.Identifier}] found in category [{category.Identifier}]!");
            }

            compiledCategory.Fields.Add(member);
        }
        #endregion

        #region Dynamic Export
        #region Category
        public static object ExportCategory(T instance, string identifier) {
            if (!categories.TryGetValue(identifier, out CompiledCategory<T> category)) {
                return null;
            }

            IDictionary<string, object> obj = new ExpandoObject();

            foreach (CompiledMember<T> member in category.Fields) {
                if (member.Get != null) {
                    obj.Add(member.Identifier, member.Get(instance));
                }
            }

            return obj;
        }

        public static object ExportCategoryIntersect(T instance, string identifier, HashSet<string> members) {
            if (members == null || members.Count <= 0) {
                return new ExpandoObject();
            }

            if (!categories.TryGetValue(identifier, out CompiledCategory<T> category)) {
                return null;
            }

            IDictionary<string, object> obj = new ExpandoObject();

            foreach (CompiledMember<T> member in category.Fields) {
                if (!members.Contains(member.Identifier)) {
                    continue;
                }

                if (member.Get != null) {
                    obj.Add(member.Identifier, member.Get(instance));
                }
            }

            return obj;
        }

        public static object ExportCategoryExclude(T instance, string identifier, HashSet<string> members) {
            if (members == null || members.Count <= 0) {
                return ExportCategory(instance, identifier);
            }

            if (!categories.TryGetValue(identifier, out CompiledCategory<T> category)) {
                return null;
            }

            IDictionary<string, object> obj = new ExpandoObject();

            foreach (CompiledMember<T> member in category.Fields) {
                if (members.Contains(member.Identifier)) {
                    continue;
                }

                if (member.Get != null) {
                    obj.Add(member.Identifier, member.Get(instance));
                }
            }

            return obj;
        }
        #endregion
        #region Global
        public static object Export(T instance) {
            IDictionary<string, object> obj = new ExpandoObject();

            foreach (CompiledMember<T> member in Mapper<T>.members.Values) {
                if (member.Get != null) {
                    obj.Add(member.Identifier, member.Get(instance));
                }
            }

            return obj;
        }

        public static object ExportIntersect(T instance, HashSet<string> members) {
            if (members == null || members.Count <= 0) {
                return new ExpandoObject();
            }

            IDictionary<string, object> obj = new ExpandoObject();

            foreach (CompiledMember<T> member in Mapper<T>.members.Values) {
                if (!members.Contains(member.Identifier)) {
                    continue;
                }

                if (member.Get != null) {
                    obj.Add(member.Identifier, member.Get(instance));
                }
            }

            return obj;
        }

        public static object ExportExclude(T instance, HashSet<string> members) {
            if (members == null || members.Count <= 0) {
                return Export(instance);
            }

            IDictionary<string, object> obj = new ExpandoObject();

            foreach (CompiledMember<T> member in Mapper<T>.members.Values) {
                if (members.Contains(member.Identifier)) {
                    continue;
                }

                if (member.Get != null) {
                    obj.Add(member.Identifier, member.Get(instance));
                }
            }

            return obj;
        }
        #endregion
        #endregion

        #region Typesafe Mapping
        public static TDestination Map<TDestination>(T source) where TDestination : class {
            if (!Mapper<TDestination>.hasEmptyConstructor) {
                return null;
            }

            TDestination destination = Activator.CreateInstance<TDestination>();

            return Map(source, destination);
        }

        public static TDestination Map<TDestination>(T source, TDestination destination) where TDestination : class {
            foreach ((CompiledMember<T>, CompiledMember<TDestination>) pair in CompiledMap<T, TDestination>.members) {
                if (pair.Item2.Set != null && pair.Item1.Get != null) {
                    pair.Item2.Set(destination, pair.Item1.Get(source));
                }
            }
            return destination;
        }



        public static TDestination MapIntersect<TDestination>(T source, HashSet<string> members) where TDestination : class {
            if (!Mapper<TDestination>.hasEmptyConstructor) {
                return null;
            }

            TDestination destination = Activator.CreateInstance<TDestination>();

            return MapIntersect(source, destination, members);
        }

        public static TDestination MapIntersect<TDestination>(T source, TDestination destination, HashSet<string> members) where TDestination : class {
            if (members == null || members.Count <= 0) {
                return destination;
            }

            foreach ((CompiledMember<T>, CompiledMember<TDestination>) pair in CompiledMap<T, TDestination>.members) {
                if (!members.Contains(pair.Item1.Identifier)) {
                    continue;
                }

                if (pair.Item2.Set != null && pair.Item1.Get != null) {
                    pair.Item2.Set(destination, pair.Item1.Get(source));
                }
            }
            return destination;
        }

        public static TDestination MapExclude<TDestination>(T source, HashSet<string> members) where TDestination : class {
            if (!Mapper<TDestination>.hasEmptyConstructor) {
                return null;
            }

            TDestination destination = Activator.CreateInstance<TDestination>();

            return MapExclude(source, destination, members);
        }

        public static TDestination MapExclude<TDestination>(T source, TDestination destination, HashSet<string> members) where TDestination : class {
            if (members == null || members.Count <= 0) {
                return Map(source, destination);
            }

            foreach ((CompiledMember<T>, CompiledMember<TDestination>) pair in CompiledMap<T, TDestination>.members) {
                if (members.Contains(pair.Item1.Identifier)) {
                    continue;
                }

                if (pair.Item2.Set != null && pair.Item1.Get != null) {
                    pair.Item2.Set(destination, pair.Item1.Get(source));
                }
            }
            return destination;
        }


        #endregion

    }
}
