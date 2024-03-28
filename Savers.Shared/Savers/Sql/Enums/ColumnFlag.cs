using System;

namespace Savers.Shared.Savers.Sql.Enums;

[Flags]
public enum ColumnFlag
{
    None = 1,
    PrimaryKey = 2,
    ForeignKey = 4,
    AutoIncrement = 8,
    NotNull = 16,
}