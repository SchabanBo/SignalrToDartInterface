﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace SignalRToDartInterface {
    public static class TypeExtensions {

        public static string ToDartType(this Type type) {
            if (type.Name == "Int32") {
                return "int";
            }
            if (type.Name == "Void") {
                return "void";
            }
            if (type.Name == "Double") {
                return "double";
            }
            return type.Name;
        }

        public static bool IsDynamic(this Type type) => typeof(IDynamicMetaObjectProvider).IsAssignableFrom(type);

        public static bool IsNonStringEnumerable(this Type type) => type != typeof(string) && type.IsEnumerableType();

        public static bool IsSetType(this Type type) => type.ImplementsGenericInterface(typeof(ISet<>));

        private static IEnumerable<MemberInfo> GetAllMembers(this Type type) =>
            type.GetTypeInheritance().Concat(type.GetInterfaces()).SelectMany(i => i.GetDeclaredMembers());

        public static MemberInfo GetInheritedMember(this Type type, string name) => type.GetAllMembers().FirstOrDefault(mi => mi.Name == name);

        public static MethodInfo GetInheritedMethod(this Type type, string name)
            => type.GetInheritedMember(name) as MethodInfo ?? throw new ArgumentOutOfRangeException(nameof(name), $"Cannot find method {name} of type {type}.");

        public static MemberInfo GetFieldOrProperty(this Type type, string name)
            => type.GetInheritedMember(name) ?? throw new ArgumentOutOfRangeException(nameof(name), $"Cannot find member {name} of type {type}.");

        public static bool IsNullableType(this Type type) => type.IsGenericType(typeof(Nullable<>));

        public static Type GetTypeOfNullable(this Type type) => type.GenericTypeArguments[0];

        public static bool IsCollectionType(this Type type) => type.ImplementsGenericInterface(typeof(ICollection<>));

        public static bool IsEnumerableType(this Type type) => typeof(IEnumerable).IsAssignableFrom(type);

        public static bool IsListType(this Type type) => typeof(IList).IsAssignableFrom(type);

        public static bool IsDictionaryType(this Type type) => type.GetDictionaryType() != null;

        public static bool IsReadOnlyDictionaryType(this Type type) => type.GetReadOnlyDictionaryType() != null;

        public static bool ImplementsGenericInterface(this Type type, Type interfaceType) => type.GetGenericInterface(interfaceType) != null;

        public static bool IsGenericType(this Type type, Type genericType) => type.IsGenericType && type.GetGenericTypeDefinition() == genericType;

        public static Type GetIEnumerableType(this Type type) => type.GetGenericInterface(typeof(IEnumerable<>));

        public static Type GetDictionaryType(this Type type) => type.GetGenericInterface(typeof(IDictionary<,>));

        public static Type GetReadOnlyDictionaryType(this Type type) => type.GetGenericInterface(typeof(IReadOnlyDictionary<,>));

        public static Type GetGenericInterface(this Type type, Type genericInterface) =>
            type.IsGenericType(genericInterface) ? type : type.GetInterfaces().FirstOrDefault(t => t.IsGenericType(genericInterface));

        public static Type GetTypeDefinitionIfGeneric(this Type type) => type.IsGenericType ? type.GetGenericTypeDefinition() : type;

        public static IEnumerable<ConstructorInfo> GetDeclaredConstructors(this Type type) => type.GetTypeInfo().DeclaredConstructors.Where(c => !c.IsStatic);

        public static Type[] GetGenericParameters(this Type type) => type.GetGenericTypeDefinition().GetTypeInfo().GenericTypeParameters;

        public static IEnumerable<MemberInfo> GetDeclaredMembers(this Type type) => type.GetTypeInfo().DeclaredMembers;

        public static IEnumerable<Type> GetTypeInheritance(this Type type) {
            yield return type;

            var baseType = type.BaseType;
            while (baseType != null) {
                yield return baseType;
                baseType = baseType.BaseType;
            }
        }

        public static IEnumerable<MethodInfo> GetDeclaredMethods(this Type type) => type.GetTypeInfo().DeclaredMethods;

        public static MethodInfo GetDeclaredMethod(this Type type, string name) => type.GetRuntimeMethods().SingleOrDefault(mi => mi.Name == name);

        public static ConstructorInfo GetDeclaredConstructor(this Type type, Type[] parameters) => type.GetDeclaredConstructors().MatchParameters(parameters);

        private static TMethod MatchParameters<TMethod>(this IEnumerable<TMethod> methods, Type[] parameters) where TMethod : MethodBase =>
            methods.SingleOrDefault(mi => mi.GetParameters().Select(pi => pi.ParameterType).SequenceEqual(parameters));
    }
}
