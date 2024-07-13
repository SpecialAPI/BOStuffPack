using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Tools
{
    public static class ILTools
    {
        public static bool Calls(this Instruction instr, MethodBase mthd) => instr.MatchCallOrCallvirt(mthd);

        public static bool JumpToNext(this ILCursor crs, Func<Instruction, bool> match, int times = 1)
        {
            for(int i = 0; i < times; i++)
            {
                if (!crs.TryGotoNext(MoveType.After, match))
                    return false;
            }

            return true;
        }

        public static bool JumpBeforeNext(this ILCursor crs, Func<Instruction, bool> match, int times = 1)
        {
            for(int i = 0; i < times; i++)
            {
                if (!crs.TryGotoNext(MoveType.Before, match))
                    return false;
            }

            return true;
        }

        public static IEnumerable MatchAfter(this ILCursor crs, Func<Instruction, bool> match)
        {
            var idx = crs.Index;
            crs.Index = 0;

            for (; crs.JumpToNext(match);)
            {
                yield return null;
            }

            crs.Index = idx;
        }

        public static IEnumerable MatchBefore(this ILCursor crs, Func<Instruction, bool> match)
        {
            var idx = crs.Index;
            crs.Index = 0;

            for (; crs.JumpBeforeNext(match); crs.JumpToNext(match))
            {
                yield return null;
            }

            crs.Index = idx;
        }

        public static VariableDefinition DeclareLocal<T>(this ILContext ctx)
        {
            var loc = new VariableDefinition(ctx.Import(typeof(T)));
            ctx.Body.Variables.Add(loc);

            return loc;
        }

        public static VariableDefinition DeclareLocal<T>(this ILCursor curs)
        {
            return curs.Context.DeclareLocal<T>();
        }
    }
}
