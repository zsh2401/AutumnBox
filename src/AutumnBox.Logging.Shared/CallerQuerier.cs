/*

* ==============================================================================
*
* Filename: CallerQuerier
* Description: 
*
* Version: 1.0
* Created: 2020/8/18 10:08:46
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AutumnBox.Logging
{
    public static class CallerQuerier
    {
        public static Result Default { get; } = new Result("Unknow", String.Empty);
        public static Result GetCurrent()
        {
            return Get(2);
        }
        public static Result Get(int startAt)
        {
            Result? result = null;
            var frames = new StackTrace().GetFrames();
            for (int i = startAt; i < frames.Length; i++)
            {
                var current = At(i);
                if (current.TypeName.StartsWith("<>c"))
                    continue;
                else
                {
                    result = current;
                    break;
                }
            }
            return result ?? Default;
            Result At(int index)
            {
                return new Result(
                    frames[index]?.GetMethod()?.DeclaringType?.Name ?? String.Empty,
                    frames[index]?.GetMethod()?.Name ?? String.Empty
                    );
            }
        }
        public sealed class Result : IEquatable<Result?>
        {
            public Result(string typeName, string methodName)
            {
                TypeName = typeName ?? throw new ArgumentNullException(nameof(typeName));
                MethodName = methodName ?? throw new ArgumentNullException(nameof(methodName));
            }

            public string TypeName { get; }
            public string MethodName { get; }

            public override bool Equals(object? obj)
            {
                return Equals(obj as Result);
            }

            public bool Equals(Result? other)
            {
                return other != null &&
                       TypeName == other.TypeName &&
                       MethodName == other.MethodName;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(TypeName, MethodName);
            }

            public static bool operator ==(Result? left, Result? right)
            {
                return EqualityComparer<Result>.Default.Equals(left, right);
            }

            public static bool operator !=(Result? left, Result? right)
            {
                return !(left == right);
            }
        }
    }
}
