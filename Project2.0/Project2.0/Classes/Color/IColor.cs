using System;
using System.Collections.Generic;
using System.Text;

namespace IP1.Imaging.ColorNS
{
    public interface IColor
    {
        static IColor White { get; }
        static IColor Black { get; }
        static IColor Red { get; }
        static IColor Green { get; }
        static IColor Blue { get; }
        static IColor Magenta { get; }
        static IColor Yellow { get; }
        static IColor Cyan { get; }
    }
}
