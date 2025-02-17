﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;


namespace CORWL_API.Extension
{
    public static class ApplyUtcDateExtension
    {
        private const string IsUtcAnnotation = "IsUtc";
        public static PropertyBuilder<TProperty> IsUtc<TProperty>(this PropertyBuilder<TProperty> builder, bool isUtc = true) =>
builder.HasAnnotation(IsUtcAnnotation, isUtc);

        private static readonly ValueConverter<DateTime, DateTime> UtcConverter =
          new(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        private static readonly ValueConverter<DateTime?, DateTime?> UtcNullableConverter =
          new(v => v, v => v == null ? v : DateTime.SpecifyKind(v.Value, DateTimeKind.Utc));

        public static bool IsUtc(this IMutableProperty property) =>
          ((bool?)property.FindAnnotation(IsUtcAnnotation)?.Value) ?? true;

        /// <summary>
        /// Make sure this is called after configuring all your entities.
        /// </summary>
        public static void ApplyUtcDateTimeConverter(this ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (!property.IsUtc())
                    {
                        continue;
                    }

                    if (property.ClrType == typeof(DateTime))
                    {
                        property.SetValueConverter(UtcConverter);
                    }

                    if (property.ClrType == typeof(DateTime?))
                    {
                        property.SetValueConverter(UtcNullableConverter);
                    }
                }
            }
        }

    }
}
