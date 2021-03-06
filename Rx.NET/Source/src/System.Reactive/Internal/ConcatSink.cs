﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information. 

using System.Collections.Generic;

namespace System.Reactive
{
    abstract class ConcatSink<TSource> : TailRecursiveSink<TSource>
    {
        public ConcatSink(IObserver<TSource> observer, IDisposable cancel)
                : base(observer, cancel)
        {
        }

        protected override IEnumerable<IObservable<TSource>> Extract(IObservable<TSource> source)
        {
            var concat = source as IConcatenatable<TSource>;
            if (concat != null)
                return concat.GetSources();

            return null;
        }

        public override void OnCompleted()
        {
            _recurse();
        }
    }
}
