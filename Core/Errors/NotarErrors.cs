﻿using Core.Entities;
using Core.Primitives;
using Core.ValueObjects;

namespace Core.Errors
{
    public class NotarErrors
    {
        public static Error InvalidName() =>
            Error.Validation($"Name is required and must be less than {Notar.MAX_NAME_LENGTH} characters.");
    }
}
