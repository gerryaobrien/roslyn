﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using System.Collections.Concurrent;

namespace Microsoft.CodeAnalysis.Simplification
{
    internal class SpecialTypeAnnotation
    {
        public const string Kind = "SpecialType";

        private static readonly ConcurrentDictionary<SpecialType, string> FromSpecialTypes = new ConcurrentDictionary<SpecialType, string>();
        private static readonly ConcurrentDictionary<string, SpecialType> ToSpecialTypes = new ConcurrentDictionary<string, SpecialType>();

        public static SyntaxAnnotation Create(SpecialType specialType)
        {
            return new SyntaxAnnotation(Kind, FromSpecialTypes.GetOrAdd(specialType, CreateFromSpecialTypes));
        }

        public static SpecialType GetSpecialType(SyntaxAnnotation annotation)
        {
            return ToSpecialTypes.GetOrAdd(annotation.Data, CreateToSpecialTypes);
        }

        private static string CreateFromSpecialTypes(SpecialType arg)
        {
            return arg.ToString();
        }

        private static SpecialType CreateToSpecialTypes(string arg)
        {
            return (SpecialType)Enum.Parse(typeof(SpecialType), arg);
        }
    }
}
