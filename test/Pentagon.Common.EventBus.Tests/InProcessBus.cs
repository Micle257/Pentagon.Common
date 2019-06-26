// -----------------------------------------------------------------------
//  <copyright file="InProcessBus.cs">
//   Copyright (c) Smartdata s. r. o. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.EventBus
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Threading.Tasks.Dataflow;
    using Threading;
    using Xunit;
    using Xunit.Abstractions;

    public class InProcessBusTests
    {
        readonly ITestOutputHelper _helper;

        public InProcessBusTests(ITestOutputHelper helper)
        {
            _helper = helper;
        }

        [Fact]
        public void Test()
        {
            var stop = new Stopwatch();

            IEventBus bus = new InProcessBus(true);

            bus.Subscribe<MsgBase>(( async (i) =>
                                {
                                    await Task.Delay(i.Number * 100);
                                    _helper.WriteLine($"Base: {i.Number} ({stop.Elapsed})");
                                }), c=>c.IncludeDerivedTypes = false);

            bus.Subscribe<Msg>((async (i) =>
                                    {
                                        await Task.Delay(i.Number * 100);
                                        _helper.WriteLine($"Der: {i.Number} - {i.Text} ({stop.Elapsed})");
                                    }));

            stop.Start();

           // bus.Send(1);
            bus.Send(new MsgBase {Number = 5});
            bus.Send(new MsgBase { Number = 2 });
            bus.Send(new Msg { Number = 3, Text = "lol" });
            bus.Send(new Msg { Number = 2, Text = "lol4" });
            bus.Send(new Msg2 { Number = 2, Text = "lol4" });
            bus.Send(new Msg2 { Number = 2, Text = "lol4" });
            bus.Send(new Msg2 { Number = 2, Text = "lol4" });

            Thread.Sleep(7000);

            stop.Stop();
        }
    }

    class MsgBase
    {
        public int Number { get; set; }
    }

    class Msg : MsgBase
    {
        public string Text { get; set; }
    }

    class Msg2 : Msg
    {
        public decimal Lol { get; }
    }
}