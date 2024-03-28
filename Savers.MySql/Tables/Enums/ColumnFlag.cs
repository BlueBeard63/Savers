using System;

namespace Savers.MySql.Tables.Enums;

[Flags]
public enum ColumnFlag
{
    None = 1,
    PrimaryKey = 2,
    ForeignKey = 4,
    AutoIncrement = 8,
    NotNull = 16,
}