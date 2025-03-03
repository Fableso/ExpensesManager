using System.Text.Json.Serialization;

using PrimaryPorts.Models;

namespace PrimaryPorts;

[JsonSerializable(typeof(ExpenseData))]
public partial class PrimaryPortsJsonSerializerContext : JsonSerializerContext;
