﻿using Mickey.Core.Common;
using Mickey.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace Mickey.EntityFramework
{
    internal static class DbContextHelper
    {
        public static IEnumerable<EntityTypeInfo> GetEntityTypeInfos(Type dbContextType)
        {
            return
                from property in dbContextType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                where
                    (ReflectionHelper.IsAssignableToGenericType(property.PropertyType, typeof(IDbSet<>)) ||
                     ReflectionHelper.IsAssignableToGenericType(property.PropertyType, typeof(DbSet<>))) &&
                    ReflectionHelper.IsAssignableToGenericType(property.PropertyType.GenericTypeArguments[0], typeof(IEntity<>))
                select new EntityTypeInfo(property.PropertyType.GenericTypeArguments[0], property.DeclaringType);
        }
    }
}
