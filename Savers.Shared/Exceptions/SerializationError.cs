using System;

namespace Savers.Shared.Exceptions;

public class SerializationError(string message) : Exception(message);