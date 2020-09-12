using System;
using System.Collections.Generic;
using System.Text;

namespace EpicOrbit.Shared.ViewModels {
    public class EnumerableResultView<T> {

        public EnumerableResultView(int offset, int count, int totalCount, List<T> items) {
            Offset = offset;
            Count = count;
            TotalCount = totalCount;
            Items = items;
        }

        public int Offset { get; set; }
        public int Count { get; set; }
        public int TotalCount { get; set; }
        public List<T> Items { get; set; }

    }
}
