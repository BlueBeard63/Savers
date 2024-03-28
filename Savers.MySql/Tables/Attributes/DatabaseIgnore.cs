using System;
using Savers.Shared.Savers.Sql.Interfaces;

namespace Savers.MySql.Tables.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class DatabaseIgnore : Attribute, IDatabaseIgnore;